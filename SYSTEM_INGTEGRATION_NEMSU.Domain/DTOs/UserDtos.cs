using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs
{
    public class UserDtos
    {

        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string? StudentId { get; set; }
        public string? Course { get; set; }
        public int YearLevel { get; set; }


    }
}
