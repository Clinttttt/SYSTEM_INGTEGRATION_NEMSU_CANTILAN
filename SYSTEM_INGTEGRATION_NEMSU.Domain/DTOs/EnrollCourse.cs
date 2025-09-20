using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs
{
   public class EnrollCourse
    {
        public Guid EnrollmentID { get; set; }
        public string? StudentID { get; set; }
        public string? CourseID { get; set; }
        public DateTime DateEnrolled { get; set; }
        public string? CourseCode { get; set; }
        public string? Title { get; set; }
        public int Unit { get; set; }
    }
}
