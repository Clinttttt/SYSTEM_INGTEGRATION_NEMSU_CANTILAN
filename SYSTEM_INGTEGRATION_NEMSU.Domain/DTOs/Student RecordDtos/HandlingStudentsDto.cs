using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs
{
   public class HandlingStudentsDto
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string CourseTitle { get; set; } = string.Empty;
        public DateTime DateEnrolled { get; set; }
        public string? StudentSchoolId { get; set; }
        public string? Email { get; set; }
        public StudentCourseStatus? studentCourseStatus { get; set; }
    
        public string? Coursedepartment { get; set; }
        public string? ProfileColor { get; set; }
    }
}
