using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.External
{
    public interface IHandlingDepartmentApi
    {
        Task<List<CourseDto>?> DisplayDITAsync();
        Task<List<CourseDto>?> DisplayDCSAsync();
        Task<List<CourseDto>?> DisplayDGTTAsync();
        Task<List<CourseDto>?> DisplayDBMAsync();
        Task<List<CourseDto>?> DisplayCCJEAsync();

    }
}
