using System;
using System.Collections.Generic;
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

      
        public Guid CourseId { get; set; }
        [JsonIgnore] 
        public Course Course { get; set; } = null!;
       
        public Category? Category { get; set; }
        public DateTime DateEnrolled { get; set; }
        public EnrollmentStatus EnrollmentStatus { get; set; }
    }

    public enum EnrollmentStatus
    {
        Provisioned,
        Enrolled,     
        Completed,   
        Dropped,       
    }


}
