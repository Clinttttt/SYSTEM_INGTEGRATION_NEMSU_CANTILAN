using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.External
{
    public interface IHandlingApiCourse
    {
        Task<CourseDto?> AddCourse(CreateCourseDto course);
        Task<IEnumerable<CourseDto>?> DisplayAllCourse();
        Task<CourseDto?> UpdateCourse(UpdateCourseDto course);
        Task<bool> DeleteCourse(Guid course);
        Task<CourseDto?> GetCourseAsync(Guid CourseId);
        Task<QuickStatsDto?> DisplayStatsAsync(string CourseCode);
    }
}
