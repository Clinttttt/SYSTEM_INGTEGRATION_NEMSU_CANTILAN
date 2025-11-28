using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandlingStudentsController(IHandlingStudents handlingStudents,  IStudentRecordCommand studentRecord) : BaseController
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        [HttpGet("Display Students")]
        public async Task<ActionResult<HandlingStudentsDto>> DisplayStudents()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await handlingStudents.DisplayStudentsAsync(userId.Value);
            return Ok(request);
        }
       
        [Authorize]
        [HttpGet("Display Students ByCourses")]
        public async Task<ActionResult<HandlingStudentsDto>> DisplayStudentsByCourses( [FromQuery] string CourseCode)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await handlingStudents.DisplayStudentByCoursesAsync(userId.Value,CourseCode);
            return Ok(request);
        }
      
        [Authorize]
        [HttpGet("GetAll StudentDetails")]
        public async Task<ActionResult<HandlingAllStudentsDetailsDto>> DisplayAllStudentsDetails( [FromQuery] Guid StudentId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await handlingStudents.DisplayAllStudentsDetailsAsync(userId.Value, StudentId);      
            return Ok(request);
        }

      
        [Authorize]
        [HttpGet("Display Students ByDepartment")]
        public async Task<ActionResult<HandlingStudentsDto>> DisplayStudentByDepartmentAsync( [FromQuery] CourseDepartment department)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await handlingStudents.DisplayStudentByDepartmentAsync(userId.Value, department);
            return Ok(request);
        }
    
        [Authorize]
        [HttpGet("Summary Statistics")]
        public async Task<ActionResult<SummaryStatisticsDto>> SummaryStatisticsAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await handlingStudents.SummaryStatisticsAsync(userId.Value);
            return Ok(request);
        }
        
        [Authorize]
        [HttpGet("Department Statistics")]
        public async Task<ActionResult<DepartmentStatsDto>> DepartmentStatsAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await handlingStudents.DepartmentStatsAsync(userId.Value);
            return Ok(request);
        }
        [Authorize]
        [HttpGet("Display StudentByYearLevel")]
        public async Task<ActionResult<object>> StudentByYearLevelAsync( [FromQuery] CourseProgram choice, YearLevelChoice yearLevel, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string searchQuery = "")
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");

            var result = await handlingStudents.StudentByYearLevelAsync(
                userId.Value,
                choice,
                yearLevel,
                pageNumber,
                pageSize,
                searchQuery);

           
            return Ok(new
            {
                Students = result.Students,
                TotalCount = result.TotalCount
            });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Get StudentId/{StudentId}")]
        public async Task<ActionResult<FacultyPhotoId>> GetStudentPhoto( [FromRoute] Guid StudentId)
        {
            var request = await studentRecord.StudentPhotoIDAsync(StudentId);
            return Ok(request);
        }
        
        [Authorize]
        [HttpGet("Student BillRecord")]
        public async Task<ActionResult<StudendBillRecordDtoDto>> StudentRecordAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await handlingStudents.StudentRecordAsync(userId.Value);
            return Ok(request);
        }

    }
}
