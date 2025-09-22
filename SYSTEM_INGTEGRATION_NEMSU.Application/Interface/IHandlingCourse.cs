using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.Interface
{
   public interface IHandlingCourse
    {
        Task<CourseDto?> AddCourseAsync(Course request);
        Task<IEnumerable<CourseDto>> DisplayCourseAsync(Guid adminid);
        Task<Course?> UpdateCourseAsync(UpdateCourseDto course);
        Task<bool> DeleteCourseAsycnc(Guid AdminId,Guid course);
    }
}
