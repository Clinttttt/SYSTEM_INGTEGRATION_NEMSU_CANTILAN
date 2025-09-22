using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.DTOs
{
    public class OutlookCourseDto
    {
        public string? StudentId { get; set; }
        public string? FacilitatorName { get; set; }
        public string? FacilitatorId { get; set; }
        public string? Message { get; set; }
        public DateTime DateCreated { get; set; }
        public string? CourseCode { get; set; }
        public string? Title { get; set; }
        public int Unit { get; set; }
    }
}
