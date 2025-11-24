using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Claims;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseHandlingController(IHandlingCourse handlingCourse) : BaseController
    {

        [Authorize]
        [HttpGet("Display Course")]
        public async Task<ActionResult> DisplayAllCourse()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await handlingCourse.DisplayCourseAsync(userId.Value);
            if (response is null) return BadRequest("nothing to dispaly");
            return Ok(response);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        [HttpGet("Display CourseByDepartment")]
        public async Task <ActionResult<IEnumerable<CourseDto>>> DisplayCourseByDepartmentAsync([FromQuery] CourseDepartment department)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await handlingCourse.DisplayCourseByDepartmentAsync(userId.Value,department);
            if (response is null) return BadRequest("nothing to dispaly");
            return Ok(response);
        }
        
        [Authorize]
        [HttpGet("GetCourse Admin/{CourseId}")]
        public async Task<ActionResult<CourseDto>> GetCourseAsync([FromRoute] Guid CourseId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await handlingCourse.GetCourseAsync(userId.Value, CourseId);
            if (response is null)
            {
                return BadRequest("Nothings To Display");
            }
            return Ok(response);
        }
        [Authorize]
        [HttpPost("Add Course")]
        public async Task<ActionResult<CourseDto>> AddCourse([FromBody] CreateCourseDto course)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var filter = course.Adapt<Course>();
            filter.AdminId = userId.Value;
            var response = await handlingCourse.AddCourseAsync(filter);
            return Ok(response);
        }


        [Authorize]
        [HttpPatch("Update Course")]
        public async Task<ActionResult<CourseDto>> UpdateCourse([FromBody] UpdateCourseDto course)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            course.AdminId = userId.Value;
            var response = await handlingCourse.UpdateCourseAsync(course);
            if (response is null) { return BadRequest("Something went wrong"); }
            return Ok(response);
        }
        [Authorize]
        [HttpDelete("Delete Course/{course}")]
        public async Task<IActionResult> DeleteCourse(Guid course)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var AdminId = userId.Value;
            var response = await handlingCourse.DeleteCourseAsycnc(AdminId, course);
            if (response is false) { return BadRequest("Nothing to Delete"); }
            return Ok(response);
        }
        [Authorize]
        [HttpGet("Quick Stats")]
        public async Task<ActionResult<QuickStatsDto>> DisplayStatsAsync([FromQuery] string CourseCode)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await handlingCourse.DisplayStatsAsync(userId.Value, CourseCode);
            if (response is null) return BadRequest("Nothing to display");
            return Ok(response);
        }
        [Authorize]
        [HttpPatch("Archive Course")]
        public async Task<ActionResult<bool>> ArchivedCourseAsync([FromQuery] string CourseCode)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await handlingCourse.ArchivedCourseAsync(userId.Value, CourseCode);
            return Ok(response);
        }
        [Authorize]
        [HttpPatch("Active CourseAsync")]
        public async Task<ActionResult<bool>> ActiveCourseAsync([FromQuery] string CourseCode)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await handlingCourse.ActiveCourseAsync(userId.Value, CourseCode);
            return Ok(response);
        }

       
        [Authorize]
        [HttpGet("Display ArchiveCourse")]
        public async Task<ActionResult<CourseDto>> DisplayAllArchiveCourse()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await handlingCourse.DisplayArchiveCourseAsync(userId.Value);
            if (response is null) return BadRequest("nothing to dispaly");
            return Ok(response);
        }
    }
}
