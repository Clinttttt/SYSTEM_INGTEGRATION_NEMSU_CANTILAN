using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs
{
    public class EnrollCourseDto
    {
    
        public Guid StudentId { get; set; }      
        public Guid CourseId { get; set; }    
        public DateTime DateEnrolled { get; set; }

       
        public string? CourseCode { get; set; } 
        public string? Title { get; set; }
        public int Unit { get; set; }
    }

}
