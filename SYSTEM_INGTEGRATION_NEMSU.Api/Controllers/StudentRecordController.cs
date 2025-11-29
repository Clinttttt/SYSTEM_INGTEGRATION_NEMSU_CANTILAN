using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Security.Claims;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDto;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos.EnrollmentFormDto;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos.NewFolder;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentRecordController(IStudentRecordCommand studentRecordCommand ) : BaseController
    {
     
        [Authorize]
        [HttpPost("Add Personal Details")]
        public async Task<ActionResult<PersonalInformation>> AddPersonalDetailsAsync([FromBody] PersonalInformationDto personalInformation)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var filter = personalInformation.Adapt<PersonalInformation>();
            filter.StudentId = userId.Value;
            var response = await studentRecordCommand.AddPersonalDetailsAsync(filter);         
            return Ok(filter);
        }
       
        [Authorize]
        [HttpPatch("Update Personal Details")]
        public async Task<ActionResult<PersonalInformation>> UpdatePersonalDetailsAsync( [FromBody] PersonalInformationDto personalInformationDto)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            personalInformationDto.StudentId = userId.Value;
            var response = await studentRecordCommand.UpdatePersonalInformationAsync(personalInformationDto);
            return Ok(response);
        }
      
        [Authorize]
        [HttpGet("Display Personal Details")]
        public async Task<ActionResult<PersonalInformationDto>> DispalyPersonalDetailsAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await studentRecordCommand.DisplayPersonalInformationAsync(userId.Value);
            return Ok(response);
        }
     
        [Authorize]
        [HttpPost("Add Academic Details")]
        public async Task<ActionResult<PersonalInformation>> AddAcademicDetailsAsync([FromBody] AcademicInformationDto academicInformation)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var filter = academicInformation.Adapt<AcademicInformation>();
            filter.StudentId = userId.Value;
            var response = await studentRecordCommand.AddAcademicInformationAsync(filter);
            return Ok(filter);
        }
       
        [Authorize]
        [HttpPatch("Update Academic Details")]
        public async Task<ActionResult<PersonalInformation>> UpdateAcademicDetailsAsync([FromBody] AcademicInformationDto academicInformation)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            academicInformation.StudentId = userId.Value;
            var response = await studentRecordCommand.UpdateAcademicInformationAsync(academicInformation);
            return Ok(response);
        }
       
        [Authorize]
        [HttpGet("Display Academic Details")]
      
        public async Task<ActionResult<AcademicInformationDto>> DispalyAcademicDetailsAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await studentRecordCommand.DisplayAcademicInformation(userId.Value);
            return Ok(response);
        }
      
        [Authorize]
        [HttpPost("Add Contact Details")]
        public async Task<ActionResult<PersonalInformation>> AddContactDetailsAsync([FromBody] ContactInformationDto contactInformation)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var filter = contactInformation.Adapt<ContactInformation>();
            filter.StudentId = userId.Value;
            var response = await studentRecordCommand.AddContactInformationAsync(filter);
            if (response is null) return BadRequest("Fill Up All Requirements");
            return Ok(filter);
        }
      
        [Authorize]
        [HttpPatch("Update Contact Details")]
        public async Task<ActionResult<PersonalInformation>> UpdateContactDetailsAsync( [FromBody] ContactInformationDto contactInformationDto)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            contactInformationDto.StudentId = userId.Value;
            var response = await studentRecordCommand.UpdateContactInformationAsync(contactInformationDto);
            return Ok(response);
        }
       
        [Authorize]
        [HttpGet("Display Contact Details")]
        public async Task<ActionResult<PersonalInformationDto>> DispalyContactDetailsAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await studentRecordCommand.DisplayContactInformationAsync(userId.Value);
            return Ok(response);
        }
       
        [Authorize]
        [HttpPost("Assign StudentID")]
        public async Task<ActionResult<SchoolIdDto>?> StudentSchoolIdAsync( [FromBody] StudentMiniInfoDto data)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            data.StudentId = userId.Value;
            var request = await studentRecordCommand.StudentSchoolIdAsync(data);
            return Ok(request);
        }
  
        [Authorize]
        [HttpPatch("UpdateAllDetails")]
        public async Task<ActionResult<ProfileUpdateDto>> UpdateAllDetailsAsync([FromBody] ProfileUpdateDto udpate)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            udpate.StudentId = userId.Value;
            var request = await studentRecordCommand.UpdateAllDetailsAsync(udpate);           
            return Ok(request);
        }
        
        [Authorize]
        [HttpPatch("Update EnrollmentForm")]
        public async Task<ActionResult<EnrollmentFormDto>> UpdateEnrollmentFormAsyn( [FromBody] EnrollmentFormDto udpate)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            udpate.StudentId = userId.Value;
            var request = await studentRecordCommand.UpdateEnrollmentFormAsync(udpate);
            return Ok(request);
        }


        [Authorize]
        [HttpPatch("Student SaveInformationAsync")]
        public async Task<ActionResult<bool>> StudentSaveInformationAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await studentRecordCommand.StudentSaveInformationAsync(userId.Value);
            return Ok(request);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        [HttpGet("Display MiniDetailsMenu")]
        public async Task<ActionResult<MiniDisplayMenuDto>> MiniDisplayMenuAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await studentRecordCommand.MiniDisplayMenuAsync(userId.Value);
            if (request is null) { return BadRequest("error"); }
            return Ok(request);
        } 
      
        [Authorize]
        [HttpGet("Check Information")]
        public async Task<ActionResult<bool>> CheckInformationAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await studentRecordCommand.CheckInformationAsync(userId.Value);
            return Ok(request);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        [HttpGet("Student PhotoID")]
        public async Task<ActionResult> StudentPhotoIDAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var request = await studentRecordCommand.StudentPhotoIDAsync(userId.Value);
            if (request is null) { return BadRequest("Something went wrong"); }
            return Ok(request);
        }
        [HttpPost("Student ForgotPassword")]
        public async Task<ActionResult<string>> ForgotPassword([FromBody] ForgotPasswordDto email)
        {
            if (string.IsNullOrWhiteSpace(email.Email))
                return BadRequest("Email is required");
            var request = await studentRecordCommand.StudentForgotPassword(email.Email);
            if(request is null)
            {
                return BadRequest("Something went wrong");
            }
            return Ok(request);
        }
        [HttpPatch("Student NewPassword")]
        public async Task<ActionResult> NewPassword([FromBody] NewPasswordDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.EmailAddress) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Email or Password is required.");
            var request = await studentRecordCommand.StudentNewPassword(dto.Password, dto.EmailAddress);
            if (!request)
                return BadRequest("Email not found.");

            return Ok(new { message = "Password updated successfully." });
        }
    }
}
