using System;
using System.Collections.Generic;
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
        public ICollection<EnrollmentCourse> Enrollments { get; set; } = new List<EnrollmentCourse>();

        public Guid? CategoryId { get; set; }   
       public Category? Category { get; set; } 
    }
    public enum CourseDepartment
    {
        BSHM,
        DIT,
        DGTT,
        DBM,
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
        First_Semester,
        Second_Semester
    }
 

}
