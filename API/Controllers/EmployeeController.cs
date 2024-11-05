using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class EmployeeController(DataContext context) : BaseApiController
    {
        [Authorize]
        [HttpPost("registerEmployee")]
        public async Task<ActionResult<EmployeeDto>> RegisterEmployee(EmployeeDto employeeDto)
        {
            if( await CheckIfEmployeeExists(employeeDto.EmpName)) return BadRequest("Employee already registered");

            var address = new Address
            {
                Address1 = employeeDto.Address1,
                Address2 = employeeDto.Address2,
                Address3 = employeeDto.Address3,
                City = employeeDto.City,
                State = employeeDto.State,
                Country = employeeDto.Country,
                IsActive = true,
                CreatedBy = employeeDto.CreatedBy,
                CreatedDate = System.DateTime.Now,
                IsDeleted = false
            };
            context.Address.Add(address);
            await context.SaveChangesAsync();

            var employee = new Employee
            {
                EmpName = employeeDto.EmpName,
                Email = employeeDto.Email,
                Mobile = employeeDto.Mobile,
                Landline = employeeDto.Landline,
                PhotoPath = employeeDto.PhotoPath,
                AddressId = address.Id,
                IsActive = true,
                CreatedBy = employeeDto.CreatedBy,
                CreatedDate = System.DateTime.Now,
                IsDeleted = false
            };
            context.Employees.Add(employee);
            await context.SaveChangesAsync();
            return employeeDto;
        }

        private async Task<bool> CheckIfEmployeeExists(string empName)
        {
            return await context.Employees.AnyAsync(c => c.EmpName.ToLower() == empName.ToLower());
        }
    }
}