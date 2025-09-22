using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.External
{
   public interface IEnrollmentApiServices
    {
        Task<Invoice?> EnrollCourseAsync(string CourseId, double Payment);
        Task<IEnumerable<EnrollmentCourse>?> DisplayCourseAsync();
        Task<bool> UnenrollCourse(string CourseId);
    }
}
