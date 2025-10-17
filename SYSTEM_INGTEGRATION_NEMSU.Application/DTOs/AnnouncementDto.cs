using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.DTOs
{
  public  class AnnouncementDto
    {
        public string? Title { get; set; }
        public string? CourseName { get; set; }
        public string? Message { get; set; }
        [JsonIgnore]
        public AnnouncementType? Type { get; set; }
        [JsonIgnore]
        public DateTime DateCreated { get; set; }
        [JsonIgnore]
        public Guid? StudentId { get; set; }
        public Guid? CourseId { get; set; }
        public string? CourseCode { get; set; }
        public InformationType InformationType { get; set; }
    
    }
}
