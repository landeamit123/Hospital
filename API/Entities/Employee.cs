using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public required string EmpName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Landline { get; set; } = string.Empty;
        public int? AddressId { get; set; }
        public Address? Address { get; set; } 
        public string PhotoPath { get; set; } = string.Empty;
        public string Shift { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifieddBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string ReasonOfLeaving { get; set; }  = string.Empty;
    }
}