using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.External
{
   public interface IHandlingStudentsApi
    {
        Task<List<HandlingStudentsDto>?> DisplayStudentsAsync();
        Task<List<HandlingStudentsDto>?> DisplayStudentByCoursesAsync(string CourseCode);
        Task<HandlingAllStudentsDetailsDto?> DisplayAllStudentsDetailsAsync(Guid StudentId);
        Task<List<HandlingStudentsDto>?> DisplayStudentByDepartmentAsync(CourseDepartment department);
        Task<SummaryStatisticsDto?> SummaryStatisticsAsync();
        Task<List<DepartmentStatsDto>?> DepartmentStatsAsync();
        Task<StudentsByYearLevelResponse?> StudentByYearLevelAsync(
           CourseProgram choice,
           YearLevelChoice yearLevel,
           int pageNumber = 1,
           int pageSize = 10,
           string searchQuery = "");
        Task<FacultyPhotoId?> StudentPhotoIDAsync(Guid StudentId);
    }
}
