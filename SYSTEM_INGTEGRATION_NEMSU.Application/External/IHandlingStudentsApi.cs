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
   public interface IHandlingStudentsApi
    {
        Task<List<HandlingStudentsDto>?> DisplayStudentsAsync();
        Task<List<HandlingStudentsDto>?> DisplayStudentByCoursesAsync(string CourseCode);
        Task<HandlingAllStudentsDetailsDto?> DisplayAllStudentsDetailsAsync(Guid StudentId);
        Task<List<HandlingStudentsDto>?> DisplayStudentByDepartmentAsync(CourseDepartment department);
    }
}
