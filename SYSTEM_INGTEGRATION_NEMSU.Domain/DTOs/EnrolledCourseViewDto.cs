using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs
{
   public class EnrolledCourseViewDto
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public string? CourseCode { get; set; }
        public EnrolledCourseStatus enrolledCourseStatus { get; set; }
        public string? CourseName { get; set; }
        public string? Schedule { get; set; }
        public string? Room { get; set; }
        public int Unit { get; set; }
        public string? SchoolYear { get; set; }
        public string? FacultyName { get; set; }
       
        public string? CourseDescription { get; set; }
        public Category? Category { get; set; }



    }
}
