using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs
{
    public class CourseDto
    {
        public double Cost { get; set; }
        public Guid? AdminId { get; set; }
        public string? CourseCode { get; set; }
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public int Unit { get; set; }
        public CategoryDto? Category { get; set; }
        public CourseDepartment? Department { get; set; }
        [JsonPropertyName("course_description")]
        public string? CourseDescriptiion { get; set; }
        public int MaxCapacity { get; set; }
        public int TotalEnrolled { get; set; }
    
        public string? SchoolYear { get; set; }
        public CourseSemester? Semester { get; set; }
        public string? Schedule { get; set; }
        public string? Room { get; set; }

        [JsonIgnore]
        public Guid CategoryId { get; set; }
        public CourseStatus CourseStatus { get; set; }
        public EnrolledCourseStatus enrolledCourseStatus { get; set; }
    }

public class CategoryDto
{
        [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}
}
