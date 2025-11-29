using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyRecordController(IFacultyRecordCommand facultyRecord) : BaseController

    {
        [Authorize]
        [HttpPost("Add Faculty Information")]
        public async Task<ActionResult<FacultyRecordDto>> AddFacultyDetailsAsync([FromBody] FacultyRecordDto facultydata)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            facultydata.FacultyId = userId.Value;
            var response = await facultyRecord.AddFacultyInformationAsync(facultydata);
            return Ok(response);
        }
        [Authorize]
        [HttpPatch("Update Faculty Information")]
        public async Task<ActionResult<FacultyRecordDto>> UpdateFacultyDetailsAsync( [FromBody] FacultyRecordDto facultydata)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            facultydata.FacultyId = userId.Value;
            var response = await facultyRecord.UpdateFacultyInformationAsync(facultydata);
            return Ok(response);
        }
        [Authorize]
        [HttpGet("Display Faculty Information")]
        public async Task<ActionResult<FacultyRecordDto>> DisplayFacultyDetailsAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await facultyRecord.DisplayFacultyDetailsAsync(userId.Value);
            return Ok(response);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        [HttpGet("Faculty PhotoID")]
        public async Task<ActionResult<FacultyRecordDto>> FacultyPhotoIDAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await facultyRecord.FacultyPhotoIDAsync(userId.Value);
            return Ok(response);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<string>> ForgotPassword([FromBody] ForgotPasswordDto email)
        {
            if (string.IsNullOrWhiteSpace(email.Email))
                return BadRequest("Email is required.");
            var request = await facultyRecord.ForgotPassword(email.Email);
            if (request is null)
            {
                return BadRequest("Something went wrong");
            }
            return Ok(request);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPatch("NewPassword")]
        public async Task<ActionResult> NewPassword([FromBody] NewPasswordDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.EmailAddress) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Email or Password is required.");
            var request = await facultyRecord.NewPassword(dto.Password, dto.EmailAddress);
            if (!request)
                return BadRequest("Email not found.");

            return Ok(new { message = "Password updated successfully." });
        }
    }
}
