using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.Interface
{
    public interface IHandlingStudents
    {
        Task<List<HandlingStudentsDto>> DisplayStudentsAsync(Guid AdminId);
        Task<List<HandlingStudentsDto>> DisplayStudentByCoursesAsync(Guid AdminId, string CourseCode);
        Task<HandlingAllStudentsDetailsDto?> DisplayAllStudentsDetailsAsync(Guid AdminId, Guid StudentId);
        Task<List<HandlingStudentsDto>> DisplayStudentByDepartmentAsync(Guid AdminId, CourseDepartment department);
        Task<SummaryStatisticsDto> SummaryStatisticsAsync(Guid Admin);
        Task<List<DepartmentStatsDto>> DepartmentStatsAsync(Guid AdminId);
        Task<(List<HandlingStudentsDto> Students, int TotalCount)> StudentByYearLevelAsync(
                   Guid AdminId,
                   CourseProgram choice,
                   YearLevelChoice yearLevel,
                   int pageNumber = 1,
                   int pageSize = 10,
                   string searchQuery = "");


    }
}
