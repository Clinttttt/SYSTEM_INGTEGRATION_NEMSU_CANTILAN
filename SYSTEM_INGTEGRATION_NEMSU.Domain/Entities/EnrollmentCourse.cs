using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities
{
    public class EnrollmentCourse
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        [JsonIgnore]   
        public User Student { get; set; } = null!;
        public StudentCourseStatus StudentCourseStatus { get; set; }
        public Guid CourseId { get; set; }
        [JsonIgnore] 
        public Course Course { get; set; } = null!;
        public string? ProfileColor { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
        public DateTime DateEnrolled { get; set; }
        public EnrollmentStatus EnrollmentStatus { get; set; }
        public EnrolledCourseStatus enrolledCourseStatus { get; set; }
    }
    public enum EnrolledCourseStatus
    {
        [Display(Name = "In Progress")]
        Inprogress,
        [Display(Name = "Completed")]
        Completed
    }
    public enum EnrollmentStatus
    {
        Provisioned,
        Enrolled,     
        Completed,   
        Dropped,       
    }
    public enum StudentCourseStatus
    {
        [Display(Name = "Active")]
        Active,
        [Display(Name = "Inactive ")]
        Inactive
    }

}
