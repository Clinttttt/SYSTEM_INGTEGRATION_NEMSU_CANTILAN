using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandlingStudentsController(IHandlingStudents handlingStudents, IUserRespository user) : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        [HttpGet("Display Students")]
        public async Task<ActionResult<HandlingStudentsDto>> DisplayStudents()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
          
    
            var request = await handlingStudents.DisplayStudentsAsync(UserId);
            return Ok(request);
        }
       
        [Authorize]
        [HttpGet("Display Students ByCourses")]
        public async Task<ActionResult<HandlingStudentsDto>> DisplayStudentsByCourses(string CourseCode)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var AdminId = await user.UserInfo(UserId);
            if (AdminId is null) return BadRequest("User Cannot Find");
            var request = await handlingStudents.DisplayStudentByCoursesAsync(AdminId.Id,CourseCode);
            return Ok(request);
        }
      
        [Authorize]
        [HttpGet("GetAll StudentDetails")]
        public async Task<ActionResult<HandlingAllStudentsDetailsDto>> DisplayAllStudentsDetails(Guid StudentId)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");

            var UserId = Guid.Parse(FindUser.Value);
            var AdminId = await user.UserInfo(UserId);
            if (AdminId is null) return BadRequest("User not found");
            var request = await handlingStudents.DisplayAllStudentsDetailsAsync(AdminId.Id, StudentId);      
            return Ok(request);
        }

      
        [Authorize]
        [HttpGet("Display Students ByDepartment")]
        public async Task<ActionResult<HandlingStudentsDto>> DisplayStudentByDepartmentAsync(CourseDepartment department)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var AdminId = await user.UserInfo(UserId);
            if (AdminId is null) return BadRequest("User Cannot Find");
            var request = await handlingStudents.DisplayStudentByDepartmentAsync(AdminId.Id, department);
            return Ok(request);
        }
    
        [Authorize]
        [HttpGet("Summary Statistics")]
        public async Task<ActionResult<SummaryStatisticsDto>> SummaryStatisticsAsync()
        {
            var Finduser = User.FindFirst(ClaimTypes.NameIdentifier);
            if(Finduser is null) { return BadRequest("Login First"); }
            var UserId = Guid.Parse(Finduser.Value);
            var AdminId = await user.UserInfo(UserId);
            if(AdminId is null) { return BadRequest("User Cannot Find"); }
            var request = await handlingStudents.SummaryStatisticsAsync(AdminId.Id);
            return Ok(request);
        }
        
        [Authorize]
        [HttpGet("Department Statistics")]
        public async Task<ActionResult<DepartmentStatsDto>> DepartmentStatsAsync()
        {
            var Finduser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (Finduser is null) { return BadRequest("Login First"); }
            var UserId = Guid.Parse(Finduser.Value);
            var AdminId = await user.UserInfo(UserId);
            if (AdminId is null) { return BadRequest("User Cannot Find"); }
            var request = await handlingStudents.DepartmentStatsAsync(AdminId.Id);
            return Ok(request);
        }
        [Authorize]
        [HttpGet("Display StudentByYearLevel")]
        public async Task<ActionResult<object>> StudentByYearLevelAsync( CourseProgram choice, YearLevelChoice yearLevel, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string searchQuery = "")
        {
            var Finduser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (Finduser is null) { return BadRequest("Login First"); }

            var UserId = Guid.Parse(Finduser.Value);

            var result = await handlingStudents.StudentByYearLevelAsync(
                UserId,
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
    }
}
