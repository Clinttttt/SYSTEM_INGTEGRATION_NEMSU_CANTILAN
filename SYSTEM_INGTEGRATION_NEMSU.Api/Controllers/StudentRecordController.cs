using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDto;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentRecordController(IStudentRecordCommand studentRecordCommand) : ControllerBase
    {
        [Authorize]
        [HttpPost("AddPersonalDetails")]
        public async Task<ActionResult<PersonalInformation>> AddPersonalDetailsAsync([FromBody] PersonalInformationDto personalInformation)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var filter = personalInformation.Adapt<PersonalInformation>();
            filter.StudentId = UserId;
            var response = await studentRecordCommand.AddPersonalDetailsAsync(filter);
            return Ok(response);
        }
        [Authorize]
        [HttpPatch("UpdatePersonalDetails")]
        public async Task<ActionResult<PersonalInformation>> UpdatePersonalDetailsAsync( PersonalInformationDto personalInformationDto)
        {
            var Finduser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (Finduser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(Finduser.Value);
            personalInformationDto.StudentId = UserId;
            var response = await studentRecordCommand.UpdatePersonalInformationAsync(personalInformationDto);
            return Ok(response);
        }
        [Authorize]
        [HttpGet("DisplayPersonalDetails")]
        public async Task<ActionResult<PersonalInformationDto>> DispalyPersonalDetailsAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var response = await studentRecordCommand.DisplayPersonalInformationAsync(UserId);
            return Ok(response);
        }
        [Authorize]
        [HttpPost("AddAcademicDetails")]
        public async Task<ActionResult<PersonalInformation>> AddAcademicDetailsAsync([FromBody] AcademicInformationDto academicInformation)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var filter = academicInformation.Adapt<AcademicInformation>();
            filter.StudentId = UserId;
            var response = await studentRecordCommand.AddAcademicInformationAsync(filter);
            return Ok(response);
        }
        [Authorize]
        [HttpPatch("UpdateAcademicDetails")]
        public async Task<ActionResult<PersonalInformation>> UpdateAcademicDetailsAsync(AcademicInformationDto academicInformation)
        {
            var Finduser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (Finduser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(Finduser.Value);
            academicInformation.StudentId = UserId;
            var response = await studentRecordCommand.UpdateAcademicInformationAsync(academicInformation);
            return Ok(response);
        }
        [Authorize]
        [HttpGet("DisplayAcademicDetails")]
        public async Task<ActionResult<PersonalInformationDto>> DispalyAcademicDetailsAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var response = await studentRecordCommand.DisplayAcademicInformation(UserId);
            return Ok(response);
        }
        [Authorize]
        [HttpPost("AddContactDetails")]
        public async Task<ActionResult<PersonalInformation>> AddContactDetailsAsync([FromBody] ContactInformationDto contactInformation)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var filter = contactInformation.Adapt<ContactInformation>();
            filter.StudentId = UserId;
            var response = await studentRecordCommand.AddContactInformationAsync(filter);
            return Ok(response);
        }
        [Authorize]
        [HttpPatch("UpdateContactDetails")]
        public async Task<ActionResult<PersonalInformation>> UpdateContactDetailsAsync(ContactInformationDto contactInformationDto)
        {
            var Finduser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (Finduser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(Finduser.Value);
            contactInformationDto.StudentId = UserId;
            var response = await studentRecordCommand.UpdateContactInformationAsync(contactInformationDto);
            return Ok(response);
        }
        [Authorize]
        [HttpGet("DisplayContactDetails")]
        public async Task<ActionResult<PersonalInformationDto>> DispalyContactDetailsAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var response = await studentRecordCommand.DisplayContactInformationAsync(UserId);
            return Ok(response);
        }
    }
}
