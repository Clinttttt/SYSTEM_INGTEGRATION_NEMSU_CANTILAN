using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Faculty_Record;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public Guid AdminId { get; set; }
        public double Cost { get; set; }
        public string? CourseCode { get; set; }
        public string? Title { get; set; }
        public int Unit { get; set; }
        public CourseDepartment Department { get; set; }
        public string? CourseDescriptiion { get; set; }
       
   
        public CourseSemester Semester { get; set; }
        public string? SchoolYear { get; set; }
        public string? Schedule { get; set; }
        public string? Room { get; set; }
        public Guid? FacultyPersonalsId { get; set; }
        [JsonIgnore]
        public FacultyPersonalInformation? FacultyPersonals { get; set; } 
     

        public Guid? CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }

        public int MaxCapacity { get; set; }
        public int TotalEnrolled { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int AvailableSlots { get; private set; }
        public CourseStatus CourseStatus { get; set; }
    }
    public enum CourseDepartment
    {
        None,
        [Display(Name = "College of Criminal Justice Education")]
        CCJE,
        [Display(Name = "Department of Industrial Technology")]
        DIT,
        [Display(Name = "Department of General Teacher Training")]
        DGTT,
        [Display(Name = "Department of Business Administration")]
        DBM,
        [Display(Name = "Department Of Computer Studies")]
        DCS,
    }
   
    public enum CourseSemester
    {
        [Display(Name = "1st Semester")]
        First_Semester,
        [Display(Name = "2nd Semester")]
        Second_Semester
    }
    public enum CourseStatus
    {   
        Active,
        Archived,
    };


}
