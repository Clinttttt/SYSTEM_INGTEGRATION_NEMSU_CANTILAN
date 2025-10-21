using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        public List<LearningObjectives> LearningObjectives { get; set; } = new();
        public CourseSemester Semester { get; set; }
        public string? SchoolYear { get; set; }
        public string? Schedule { get; set; }
        public string? Room { get; set; }
        [JsonIgnore]
        public ICollection<EnrollmentCourse> Enrollments { get; set; } = new List<EnrollmentCourse>();
        [JsonIgnore] 
        public ICollection<InstructorAnnouncement> Announcements { get; set; } = new List<InstructorAnnouncement>();
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int MaxCapacity { get; set; }
        public int TotalEnrolled { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int AvailableSlots { get; private set; }




        public CourseStatus CourseStatus { get; set; }
    }
    public enum CourseDepartment
    {
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
    public class LearningObjectives
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? description { get; set; }

        [JsonIgnore]
        public Guid CourseId { get; set; }
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
