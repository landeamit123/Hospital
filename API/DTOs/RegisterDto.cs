using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(100)]
        public required string UserName { get; set; }
        [Required]
        [MaxLength(20)]
        public required string Password { get; set; }
        public int RoleId { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(20)]
        public string MiddleName { get; set; } = string.Empty;
        [MaxLength(20)]
        public string LastName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Landline { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}