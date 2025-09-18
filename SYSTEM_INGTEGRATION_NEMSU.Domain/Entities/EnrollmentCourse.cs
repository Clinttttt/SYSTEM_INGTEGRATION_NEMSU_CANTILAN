using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities
{
  public class EnrollmentCourse
    {
       public Guid Id { get; set; }
       public string? StudentID { get; set; }
       public Guid CourseID { get; set; }
       public DateTime DateEnrolled { get; set; }
    }
}
