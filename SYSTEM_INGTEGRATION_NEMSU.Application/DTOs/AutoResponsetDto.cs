using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.DTOs
{
  public class AutoResponsetDto
    {
        public string? Message { get; set; }
        public AnnouncementType? Type { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid StudentId { get; set; }  
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
    }
}
