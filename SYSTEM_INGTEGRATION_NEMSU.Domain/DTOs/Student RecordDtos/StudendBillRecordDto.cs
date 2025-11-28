using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos
{
   public class StudendBillRecordDtoDto
    {
        public string? StudentId { get; set; }
        public DateTime DateEnrolled { get; set; }
        public string? CourseCode { get; set; }
        public InvoiceStatus? Status { get; set; }
        public double Cost { get; set; }
        public string? FullName { get; set; }
    }
}
