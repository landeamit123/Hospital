using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            try
            {
                if(await UserExists(registerDto.UserName)) return BadRequest("Username is already taken");

            using var hmac = new HMACSHA512();
            var user = new User
            {
                UserName = registerDto.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key,
                FirstName = registerDto.FirstName,
                RoleId = registerDto.RoleId,
                MiddleName = registerDto.MiddleName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Mobile = registerDto.Mobile,
                Landline = registerDto.Landline,
                IsActive = true,
                CreatedBy = registerDto.CreatedBy,
                CreatedDate = registerDto.CreatedDate,
                IsDeleted = false
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return new UserDto
            {
                Username = registerDto.FirstName + " " + registerDto.LastName,
                Token = registerDto.Email
            };
            }
            catch (Exception ex)
            {
                var ab = ex.Message;
                throw;
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await context.Users.FirstOrDefaultAsync( x => 
            x.UserName == loginDto.UserName.ToLower());

            if(user == null) return Unauthorized("Invalid User");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < ComputeHash.Length; i++)
            {
                if(ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            return new UserDto
            {
                Username = user.FirstName + " " + user.LastName,
                Token = tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(u => u.UserName.ToLower() == username.ToLower());
        }
    }
}