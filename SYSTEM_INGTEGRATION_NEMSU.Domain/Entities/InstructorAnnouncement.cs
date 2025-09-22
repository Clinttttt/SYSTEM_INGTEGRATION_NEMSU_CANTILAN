using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities
{
  public class InstructorAnnouncement
    {
        public Guid Id { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? CourseId { get; set; }
        public AnnouncementType? Type { get; set; }
        public string? Message { get; set; }

        public DateTime DateCreated { get; set; }
    }
    public enum AnnouncementType
    {
        instructor,
        System,
    }
}
