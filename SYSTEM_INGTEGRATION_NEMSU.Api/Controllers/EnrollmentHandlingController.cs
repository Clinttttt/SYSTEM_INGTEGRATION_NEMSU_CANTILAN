using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentHandlingController(IPaymentServices enrollmenthandling, IEnrollmentServices enrollmentservices, IUserRespository respository) : ControllerBase
    {
        [Authorize]
        [HttpPost("Enroll Course")]
        public async Task<ActionResult<Invoice>> EnrollCourse(string CourseId, double Payment)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null)  return Unauthorized("Login First");
            
            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if (user is null) return BadRequest("User not found or Student ID missing");
            
            var StudentId = user.Id;
            var response = await enrollmenthandling.InvoiceAsync(StudentId, CourseId, Payment);
            if (response is null) { return BadRequest("Something went wrong"); }
            return Ok(response);
        }
        [Authorize]
        [HttpPost("Provision EnrollCourse")]
        public async Task<ActionResult<Invoice>?> ProvisionAsync( string courseCode)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return Unauthorized("Login First");

            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if (user is null) return BadRequest("User not found or Student ID missing");

            var StudentId = user.Id;
            var response = await enrollmenthandling.ProvisionAsync(StudentId, courseCode);
            if (response is null) { return BadRequest("Something went wrong"); }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("Display Course")]
        public async Task<ActionResult<EnrollmentCourse>> DisplayCourse()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if(FindUser is null) return Unauthorized("Login First");
            
            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if(user is null) return BadRequest("User not found or Student ID missing");
            
            var StudentId = user.Id;
            var response = await enrollmentservices.DisplayCourseAsync(StudentId);
            if (response is null) { return BadRequest("Nothing to Display"); }
            return Ok(response);
        }
        [Authorize]
        [HttpGet("Get Course")]
        public async Task<ActionResult<CourseDto>?> GetCourse(Guid CourseId)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null)
            {
                return BadRequest("Login First");
            }
            var UserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(UserId);
            if (user is null) return BadRequest("User not found or Student ID missing");
            var StudentId = user.Id;
            var response = await enrollmentservices.GetCourse(CourseId, StudentId);
            return Ok(response);
        }
        [Authorize]
        [HttpDelete("UnEnroll Course/{CourseId}")]
        public async Task<IActionResult> UnenrollCourse(string CourseId)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if(FindUser is null) return Unauthorized("Login First");
            
            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if(user is null || string.IsNullOrEmpty(CourseId)) return BadRequest("User not found or Student ID missing");
            
            var StudentId = user.Id;
            var response = await enrollmentservices.UnEnrollCourseAsync(StudentId,CourseId);
            if (response is false) return BadRequest("Something went wrong");      
            return Ok(response);
        }
        [Authorize]
        [HttpPatch("Inactive Course")]
        public async Task<ActionResult<bool>> InactiveCourse(Guid CourseId)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if(FindUser is null) { return BadRequest("User not found"); }
            var UserId = Guid.Parse(FindUser.Value);
            var request = await enrollmentservices.InactiveCourseAsync(UserId, CourseId);
            return Ok(request);

        }
        [Authorize]
        [HttpPatch("Active Course")]
        public async Task<ActionResult<bool>> ActiveCourse(Guid CourseId)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) { return BadRequest("User not found"); }
            var UserId = Guid.Parse(FindUser.Value);
            var request = await enrollmentservices.InactiveCourseAsync(UserId, CourseId);
            return Ok(request);

        }
        [Authorize]
        [HttpGet("Display Announcement")]
        public async  Task<ActionResult<AnnouncementDto>> DisplayAnnounceMentAsync(string CourseCode)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) { return BadRequest("User not found"); }
            var UserId = Guid.Parse(FindUser.Value);
            var request = await enrollmentservices.DisplayAnnounceMentAsync(UserId, CourseCode);
            return Ok(request);
        }
    }
}
