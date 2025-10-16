using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.Interface
{
    public interface IHandlingDepartment
    {
        Task<List<CourseDto>?> DisplayDITAsync(Guid AdminId);
        Task<List<CourseDto>?> DisplayDCSAsync(Guid AdminId);
        Task<List<CourseDto>?> DisplayDGTTAsync(Guid AdminId);
        Task<List<CourseDto>?> DisplayDBMAsync(Guid AdminId);
        Task<List<CourseDto>?> DisplayCCJEAsync(Guid AdminId);
      
    }
}
