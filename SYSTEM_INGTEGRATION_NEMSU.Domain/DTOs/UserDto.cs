using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.DTOs
{
 public   class UserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? StudentId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Course { get; set; }
        public int YearLevel { get; set; }
       // public UserOption UserOption { get; set; }
    }
    /* public enum UserOption
      {
          Student,
          Facilitator

      }*/

}
