using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.DTOs
{
    public class RequestAnnouncementDto
    {
        public Guid StudentId { get; set; }
        public string? Message { get; set; }
        public string? CourseCode { get; set; }
    }
}
