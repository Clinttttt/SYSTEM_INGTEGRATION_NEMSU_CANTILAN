using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
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
        public async Task<ActionResult<PaymentDetailsDto>> EnrollCourse(PaymentDetailsDto paymentdetails)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return Unauthorized("Login First");

            var GetUserId = Guid.Parse(FindUser.Value);


            paymentdetails.StudentId = GetUserId;
            var response = await enrollmenthandling.InvoiceAsync(paymentdetails);
            if (response is null) { return BadRequest("Something went wrong"); }
            return Ok(response);
        }
        [Authorize]
        [HttpPost("ProvisionEnrollCourse")]
        public async Task<ActionResult<ProvisionDto>?> ProvisionAsync(string courseCode)
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
        [HttpGet("Display AllCourseDetails")]
        public async Task<ActionResult<CourseDto>> DisplayCourse()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return Unauthorized("Login First");

            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if (user is null) return BadRequest("User not found or Student ID missing");

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
            if (FindUser is null) return Unauthorized("Login First");

            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if (user is null || string.IsNullOrEmpty(CourseId)) return BadRequest("User not found or Student ID missing");

            var StudentId = user.Id;
            var response = await enrollmentservices.UnEnrollCourseAsync(StudentId, CourseId);
            if (response is false) return BadRequest("Something went wrong");
            return Ok(response);
        }
        [Authorize]
        [HttpPatch("Inactive Course")]
        public async Task<ActionResult<bool>> InactiveCourse(Guid CourseId)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) { return BadRequest("User not found"); }
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
        [HttpGet("Display PreviewCourse")]
        public async Task<ActionResult<CourseDto>> PreviewCourseAsync(Guid CourseId)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) { return BadRequest("User not found"); }
            var UserId = Guid.Parse(FindUser.Value);
            var request = await enrollmentservices.PreviewCourseAsync(UserId, CourseId);
            return Ok(request);
        }
        [Authorize]
        [HttpPost("Add Payment")]
        public async Task<ActionResult<PaymentDetailsDto>> AddPaymentAsync(PaymentDetailsDto payment)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) { return BadRequest("User not found"); }
            var UserId = Guid.Parse(FindUser.Value);
            payment.StudentId = UserId;
            if (UserId != payment.StudentId)
            {
                return BadRequest("User Not found");
            }
            var request = await enrollmentservices.AddPaymentAsync(payment);
            return Ok(request);
        }
        [Authorize]
        [HttpGet("Display Payment")]
        public async Task<ActionResult<PaymentDetailsDto>> DisplayPaymentAsync(Guid StudentId)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) { return BadRequest("User not found"); }
            var UserId = Guid.Parse(FindUser.Value);
            var request = await enrollmentservices.DisplayPaymentAsync(UserId, StudentId);
            return Ok(request);
        }

        [Authorize]
        [HttpGet("Display AllAnnouncement")]
        public async Task<ActionResult<AnnouncementDto>> DisplayAllAnnouncementAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("User not found");
            var UserId = Guid.Parse(FindUser.Value);
            var request = await enrollmentservices.DisplayAllAnnouncementAsync(UserId);
            return Ok(request);
        }
        [Authorize]
        [HttpGet("Display CourseAnnouncement")]
        public async Task<ActionResult<AnnouncementDto>> DisplayCourseAnnouncementAsync(Guid CourseId)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("User not found");
            var UserId = Guid.Parse(FindUser.Value);
            var request = await enrollmentservices.DisplayAnnouncementAsync(CourseId, UserId);
            return Ok(request);
        }
        [Authorize]
        [HttpGet("Display AnnouncementByType")]
        public async Task<ActionResult<AnnouncementDto>> DisplayAnnouncementByType(InformationType type)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("User not found");
            var UserId = Guid.Parse(FindUser.Value);
            var request = await enrollmentservices.DisplayAnnouncementByType(UserId, type);
            return Ok(request);
        }
        [Authorize]
        [HttpGet("Display AllEnrolledCourse")]
        public async Task<ActionResult<EnrollCourseDto>> DisplayAllCourseEnrolledAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("User not found");
            var UserId = Guid.Parse(FindUser.Value);
            var request = await enrollmentservices.DisplayAllCourseEnrolledAsync(UserId);
            return Ok(request);
        }
    }
}
