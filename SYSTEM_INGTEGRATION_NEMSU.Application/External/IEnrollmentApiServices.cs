using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.External
{
    public interface IEnrollmentApiServices
    {
        Task<Invoice?> EnrollCourseAsync(string CourseId, double Payment);
        Task<IEnumerable<CourseDto>?> DisplayCourseAsync();
        Task<bool> UnenrollCourse(string CourseId);

        Task<CourseDto?> PreviewCourseAsync(Guid CourseId);
    }
}
