using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord
{
    public class CourseTracker
    {
        public Guid Id { get; set; }
        public Guid StudentId {get;set;}
        public Guid CourseId { get; set; }
        public string? CourseName { get; set; }
        public CourseTrack CourseTrack { get; set; } = CourseTrack.not_enrolled;
    }
    public enum CourseTrack
    {   not_enrolled,
        Course_Already_Paid,
        Course_Already_Provisioned,
        Deleted_Paid,
        Deleted_Provisioned
    }
}
