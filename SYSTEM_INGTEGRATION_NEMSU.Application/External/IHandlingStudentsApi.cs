using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.External
{
   public interface IHandlingStudentsApi
    {
        Task<List<HandlingStudentsDto>?> DisplayStudentsAsync();
        Task<List<HandlingStudentsDto>?> DisplayStudentByCoursesAsync(string CourseCode);
    }
}
