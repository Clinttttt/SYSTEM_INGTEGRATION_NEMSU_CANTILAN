using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities
{
  public class InstructorAnnouncement
    {
        public Guid Id { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? CourseId { get; set; }
        public AnnouncementType? Type { get; set; }
        public string? CourseCode { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public InformationType InformationType { get; set; }
        [JsonIgnore] // 👈 prevents recursive serialization
        public Course course { get; set; } = null!;
        public DateTime DateCreated { get; set; }
    }
    public enum AnnouncementType
    {
        instructor,
        System,
    }
    public enum InformationType
    {
        Academic,
        Events,
        Important,
        General
    };
    

}
