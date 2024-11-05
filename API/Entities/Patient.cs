using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public string MiddleName { get; set; } = string.Empty;
        public required string LastName { get; set; }
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BP { get; set; } = string.Empty;
        public string Temperature { get; set; } = string.Empty;
        public string Issue { get; set; } = string.Empty;
        public string Daignostic { get; set; } = string.Empty;
        public string PreviousHistory { get; set; } = string.Empty;
    }
}