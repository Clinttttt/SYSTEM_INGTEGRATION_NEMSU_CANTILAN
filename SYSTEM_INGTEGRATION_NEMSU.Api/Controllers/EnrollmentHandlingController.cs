using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentHandlingController(IPaymentServices enrollmenthandling, IEnrollmentServices enrollmentservices) : ControllerBase
    {
        [HttpPost("EnrollCourse")]
        public async Task<ActionResult<Invoice>> EnrollCourse( string StudentID, Guid CourseId, double Payment)
        {
            var response = await enrollmenthandling.InvoiceAsync(StudentID, CourseId, Payment);
            if(response is null)
            {
                return BadRequest("Something went wrong");
            }
            return Ok(response);
        }
        [HttpGet("DisplayCourse")]
        public async Task<ActionResult<EnrollmentCourse>> DisplayCourse(int StudentId)
        {
            var response = await enrollmentservices.DisplayCourseAsync(StudentId);
            if(response is null)
            {
                return BadRequest("Nothing to Display");
            }
            return Ok(response);
        }
    }
}
