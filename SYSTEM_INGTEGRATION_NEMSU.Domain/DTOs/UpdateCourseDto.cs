using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs
{
    public class UpdateCourseDto
    {
        public string? CourseCode { get; set; }
        public string? Title { get; set; }
        public Guid? CategoryId { get; set; }
        public CourseDepartment Department { get; set; }
        public int Unit { get; set; }
        public double Cost { get; set; }
        public string? CourseDescription { get; set; }
        public CourseSemester Semester { get; set; }
        public string? SchoolYear { get; set; }
        public string? Schedule { get; set; }
        public string? Room { get; set; }
        public Guid Id { get; set; }
        public Guid AdminId { get; set; }

        public int MaxCapacity { get; set; }
        
        public CourseStatus CourseStatus { get; set; }

    }
}
