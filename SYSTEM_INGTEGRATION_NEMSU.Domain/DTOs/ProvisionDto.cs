using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs
{
    public class ProvisionDto
    {
        public string? CourseCode { get; set; }
        public InvoiceStatus Status { get; set; }
        public string? Standing { get; set; }
       public DateTime DateEnrolled { get; set; }

    }
}
