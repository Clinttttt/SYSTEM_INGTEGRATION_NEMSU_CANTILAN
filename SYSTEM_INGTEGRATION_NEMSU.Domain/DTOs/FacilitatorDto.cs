using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs
{
  public class FacilitatorDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? FacilitatorId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }      
    }
}
