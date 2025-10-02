using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs
{
   public class CreateCourseDto
    {
        public double Cost { get; set; }
        public string? CourseCode { get; set; }
        public string? Title { get; set; }
        public int Unit { get; set; }
        public Guid CategoryId { get; set; }

    }
}
