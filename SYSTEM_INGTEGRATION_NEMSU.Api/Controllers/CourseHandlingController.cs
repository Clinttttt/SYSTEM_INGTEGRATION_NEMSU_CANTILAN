using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class CourseHandlingController(IHandlingCourse handlingCourse, IUserRespository respository) : ControllerBase
    {

        [Authorize]
        [HttpGet("Display Course")]
        public async Task<ActionResult<CourseDto>> DisplayAllCourse()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var response = await handlingCourse.DisplayCourseAsync(UserId);
            if (response is null) return BadRequest("nothing to dispaly");
            return Ok(response);
        }
        [Authorize]
        [HttpGet("GetCourse Admin/{CourseId}")]
        public async Task<ActionResult<CourseDto>> GetCourseAsync(Guid CourseId)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var AdminId = await respository.UserInfo(UserId);
            if (AdminId is null) return BadRequest("User not Found");
            var StudentId = AdminId.Id;
            var response = await handlingCourse.GetCourseAsync(StudentId, CourseId);
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
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null)
            {
                return BadRequest("Login First");
            }
            var UserId = Guid.Parse(FindUser.Value);
            var filter = course.Adapt<Course>();
            filter.AdminId = UserId;
            var response = await handlingCourse.AddCourseAsync(filter);
            return Ok(response);
        }


        [Authorize]
        [HttpPatch("Update Course")]
        public async Task<ActionResult<CourseDto>> UpdateCourse(UpdateCourseDto course)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            var userid = Guid.Parse(FindUser!.Value);
            var user = await respository.UserInfo(userid);
            if (user is null) return BadRequest("user not found");
            course.AdminId = user.Id;
            var response = await handlingCourse.UpdateCourseAsync(course);

            if (response is null) { return BadRequest("Something went wrong"); }
            return Ok(response);
        }
        [Authorize]
        [HttpDelete("Delete Course/{course}")]
        public async Task<IActionResult> DeleteCourse(Guid course)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null)
            {
                return Unauthorized("Login First");
            }
            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if (user is null)
            {
                return BadRequest("User not found ");
            }
            var AdminId = user.Id;
            var response = await handlingCourse.DeleteCourseAsycnc(AdminId, course);
            if (response is false) { return BadRequest("Nothing to Delete"); }
            return Ok(response);
        }
        [Authorize]
        [HttpGet("Quick Stats")]
        public async Task<ActionResult<QuickStatsDto>> DisplayStatsAsync(string CourseCode)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");

            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if (user is null) { return BadRequest("User not found "); }
            var response = await handlingCourse.DisplayStatsAsync(user.Id, CourseCode);
            if (response is null) return BadRequest("Nothing to display");
            return Ok(response);
        }
        [Authorize]
        [HttpPatch("Archive Course")]
        public async Task<ActionResult<bool>> ArchivedCourseAsync(string CourseCode)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");

            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if (user is null) { return BadRequest("User not found "); }
            var response = await handlingCourse.ArchivedCourseAsync(user.Id, CourseCode);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("Display ArchiveCourse")]
        public async Task<ActionResult<CourseDto>> DisplayAllArchiveCourse()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var response = await handlingCourse.DisplayArchiveCourseAsync(UserId);
            if (response is null) return BadRequest("nothing to dispaly");
            return Ok(response);
        }
    }
}
