using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs
{
    public class EnrollCourseDto
    {
    
        public Guid StudentId { get; set; }      
        public Guid CourseId { get; set; }    
        public string? FacultyFullName { get; set; }
        public DateTime DateEnrolled { get; set; }
        [JsonIgnore]
        public Course? Course { get; set; }
       
        public Category? Category { get; set; }
        public string? CourseCode { get; set; } 
        public string? Title { get; set; }
        public int Unit { get; set; }
        public string? Schedule { get; set; }
        public string? Room { get; set; }
        public EnrollmentStatus EnrollmentStatus { get; set; }
    }

}
