using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("DisplayCourse/{Adminid}")]
        public async Task<ActionResult<CourseDto>> DisplayAllCourse(Guid Adminid)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if(FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var response = await handlingCourse.DisplayCourseAsync(Adminid);

            return response is not null && UserId == Adminid ? Ok(response) : BadRequest("Something went wrong");
        }
        [Authorize]
        [HttpGet("GetCourseAdmin")]
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
        [HttpPost("AddCourse")]
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
        [HttpPatch("UpdateCourse")]
        public async Task<ActionResult<CourseDto>> UpdateCourse(UpdateCourseDto course)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            var userid = Guid.Parse(FindUser!.Value);
            var response = await handlingCourse.UpdateCourseAsync(course);
            response!.AdminId = userid;
            if(response is null)
            {
                return BadRequest("Something went wrong");
            }
            return Ok(response);
        }
        [Authorize]
        [HttpDelete("DeleteCourse/{course}")]
        public async Task<IActionResult> DeleteCourse(Guid course)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if(FindUser is null)
            {
                return Unauthorized("Login First");
            }
            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if(user is null)
            {
                return BadRequest("User not found ");
            }
            var AdminId = user.Id;
            var response = await handlingCourse.DeleteCourseAsycnc(AdminId, course);
            if(response is false)
            {
                return BadRequest("Nothing to Delete");
            }
            return Ok(response);
        }
    }
}
