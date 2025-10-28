using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.DTOs
{
    public class CreateAnnouncementDto
    {
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? CourseCode { get; set; }
        public InformationType InformationType { get; set; }
        public Guid AdminId { get; set; }
    }

    public class EditAnnouncementDto
    {
        public string? Title { get; set; }
        public string? Message { get; set; }

        public InformationType InformationType { get; set; }
        public Guid AdminId { get; set; }
        public Guid AnnouncementId { get; set; }
    }

    public class AnnouncementDto
    {

        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? CourseName { get; set; }
        public Guid CourseId { get; set; }
        public InformationType InformationType { get; set; }
        public AnnouncementType Type { get; set; }
        public DateTime DateCreated { get; set; }
        public string? CourseCode { get; set; }
        public Guid AnnouncementId { get; set; }


    }
}
