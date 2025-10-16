using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.Interface
{
    public interface IHandlingStudents
    {
        Task<List<HandlingStudentsDto>> DisplayStudentsAsync(Guid AdminId);
        Task<List<HandlingStudentsDto>> DisplayStudentByCoursesAsync(Guid AdminId, string CourseCode);
    }
}
