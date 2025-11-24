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
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentHandlingController(IPaymentServices enrollmenthandling, IEnrollmentServices enrollmentservices) : BaseController
    {
        [Authorize]
        [HttpPost("Enroll Course")]
        public async Task<ActionResult<PaymentDetailsDto>> EnrollCourse([FromBody] PaymentDetailsDto paymentdetails)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            paymentdetails.StudentId = userId.Value;
            if (string.IsNullOrWhiteSpace(paymentdetails.AccountNumber) || paymentdetails.cost <= 0)
                return BadRequest("Invalid account number or cost.");
            var response = await enrollmenthandling.InvoiceAsync(paymentdetails);
            if (response == null)
                return Conflict("Already enrolled.");
            return Ok(response);
        }

        [Authorize]
        [HttpPost("ProvisionEnrollCourse")]
        public async Task<ActionResult<ProvisionDto>?> ProvisionAsync([FromQuery] string courseCode)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var StudentId = userId.Value;
            var response = await enrollmenthandling.ProvisionAsync(StudentId, courseCode);
            if (response == null)
                return Conflict("Already enrolled.");
            return Ok(response);
        }

        [Authorize]
        [HttpPost("Pay ProvisionAsync")]
        public async Task<ActionResult<bool>?> PayProvisionAsync([FromBody] PaymentDetailsDto paymentDetails)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            paymentDetails.StudentId = userId.Value;
            var response = await enrollmenthandling.PayProvisionAsync(paymentDetails);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("Display AllCourseDetails")]
        public async Task<ActionResult<CourseDto>> DisplayCourse()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var StudentId = userId.Value;
            var response = await enrollmentservices.DisplayCourseAsync(StudentId);
            if (response is null) { return BadRequest("Nothing to Display"); }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("Get Course")]
        public async Task<ActionResult<EnrolledCourseViewDto>> GetCourse([FromQuery] Guid CourseId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var StudentId = userId.Value;
            var response = await enrollmentservices.GetCourse(CourseId, StudentId);
            if (response is null) return BadRequest("Something went wrong ");
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("UnEnroll Course/{CourseId}")]
        public async Task<IActionResult> UnenrollCourse([FromRoute] string CourseId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var StudentId = userId.Value;
            var response = await enrollmentservices.UnEnrollCourseAsync(StudentId, CourseId);
            if (response is false) return BadRequest("Something went wrong");
            return Ok(response);
        }

        [Authorize]
        [HttpPatch("Inactive Course")]
        public async Task<ActionResult<bool>> InactiveCourse([FromQuery] Guid CourseId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await enrollmentservices.InactiveCourseAsync(userId.Value, CourseId);
            if (request is false) { return BadRequest("Something went wrong"); }
            return Ok(request);

        }

        [Authorize]
        [HttpPatch("Active Course")]
        public async Task<ActionResult<bool>> ActiveCourse([FromQuery] Guid CourseId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await enrollmentservices.ActiveCourseAsync(userId.Value, CourseId);
            if (request is false) { return BadRequest("Something went wrong"); }
            return Ok(request);

        }


        [Authorize]
        [HttpGet("Display PreviewCourse")]
        public async Task<ActionResult<CourseDto>> PreviewCourseAsync([FromQuery] Guid CourseId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await enrollmentservices.PreviewCourseAsync(userId.Value, CourseId);
            return Ok(request);
        }
        [Authorize]
        [HttpPost("Add Payment")]
        public async Task<ActionResult<PaymentDetailsDto>> AddPaymentAsync([FromBody] PaymentDetailsDto payment)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            payment.StudentId = userId.Value;         
            var request = await enrollmentservices.AddPaymentAsync(payment);
            return Ok(request);
        }

        [Authorize]
        [HttpGet("Display Payment")]
        public async Task<ActionResult<PaymentDetailsDto>> DisplayPaymentAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await enrollmentservices.DisplayPaymentAsync(userId.Value);
            return Ok(request);
        }

        [Authorize]
        [HttpDelete("Delete PaymentDetails")]
        public async Task<ActionResult<bool>> DeletePaymentAsync([FromQuery] Guid PaymentId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await enrollmentservices.DeletePaymentAsync(userId.Value, PaymentId);
            return Ok(request);
        }



        [Authorize]
        [HttpGet("Display AllAnnouncement")]
        public async Task<ActionResult<AnnouncementDto>> DisplayAllAnnouncementAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await enrollmentservices.DisplayAllAnnouncementAsync(userId.Value);
            return Ok(request);
        }

        [Authorize]
        [HttpGet("Display CourseAnnouncement")]
        public async Task<ActionResult<AnnouncementDto>> DisplayCourseAnnouncementAsync([FromQuery] Guid CourseId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await enrollmentservices.DisplayAnnouncementAsync(CourseId, userId.Value);
            return Ok(request);
        }

        [Authorize]
        [HttpGet("Display AnnouncementByType")]
        public async Task<ActionResult<AnnouncementDto>> DisplayAnnouncementByType([FromQuery] InformationType type)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await enrollmentservices.DisplayAnnouncementByType(userId.Value, type);
            return Ok(request);
        }

        [Authorize]
        [HttpGet("Display DisplayAllTypeAnnouncementAsync")]
        public async Task<ActionResult<AnnouncementDto>> DisplayAllTypeAnnouncementAsync([FromQuery] Guid CourseId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await enrollmentservices.DisplayAllTypeAnnouncementAsync(CourseId, userId.Value);
            return Ok(request);
        }

        [Authorize]
        [HttpGet("Display AllEnrolledCourse")]
        public async Task<ActionResult<EnrollCourseDto>> DisplayAllCourseEnrolledAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await enrollmentservices.DisplayAllCourseEnrolledAsync(userId.Value);
            return Ok(request);
        }

        [Authorize]
        [HttpPost("Generate StudentId")]
        public async Task<ActionResult<SchoolIdDto>> GenerateStudentId()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await enrollmentservices.GenerateStudentId(userId.Value);
            if (request is null) { return BadRequest("null"); }
            return Ok(request);
        }

        [Authorize]
        [HttpGet("Course TrackerAsync")]
        public async Task<ActionResult<CourseTrack>> CourseTrackerAsync([FromQuery] Guid CourseId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await enrollmentservices.CourseTrackerAsync(userId.Value, CourseId);
            return Ok(request);
        }

        [Authorize]
        [HttpPost("Direct EnrollAsync")]
        public async Task<IActionResult> DirectEnrollAsync([FromQuery] Guid CourseId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await enrollmentservices.DirectEnrollAsync(userId.Value, CourseId);
            if (request is false) return BadRequest("Something went wrong");
            return Ok(request);
        }
    }
}
