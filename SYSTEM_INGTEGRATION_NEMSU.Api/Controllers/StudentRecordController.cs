using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Security.Claims;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDto;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentRecordController(IStudentRecordCommand studentRecordCommand, IUserRespository user) : ControllerBase
    {
        [Authorize]
        [HttpPost("Add Personal Details")]
        public async Task<ActionResult<PersonalInformation>> AddPersonalDetailsAsync([FromBody] PersonalInformationDto personalInformation)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var filter = personalInformation.Adapt<PersonalInformation>();
            filter.StudentId = UserId;
            var response = await studentRecordCommand.AddPersonalDetailsAsync(filter);
            return Ok(filter);
        }
        [Authorize]
        [HttpPatch("Update Personal Details")]
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
        [HttpGet("Display Personal Details")]
        public async Task<ActionResult<PersonalInformationDto>> DispalyPersonalDetailsAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var response = await studentRecordCommand.DisplayPersonalInformationAsync(UserId);
            return Ok(response);
        }
        [Authorize]
        [HttpPost("Add Academic Details")]
        public async Task<ActionResult<PersonalInformation>> AddAcademicDetailsAsync([FromBody] AcademicInformationDto academicInformation)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var filter = academicInformation.Adapt<AcademicInformation>();
            filter.StudentId = UserId;
            var response = await studentRecordCommand.AddAcademicInformationAsync(filter);
            return Ok(filter);
        }
        [Authorize]
        [HttpPatch("Update Academic Details")]
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
        [HttpGet("Display Academic Details")]

        public async Task<ActionResult<AcademicInformationDto>> DispalyAcademicDetailsAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var response = await studentRecordCommand.DisplayAcademicInformation(UserId);
            return Ok(response);
        }
        [Authorize]
        [HttpPost("Add Contact Details")]
        public async Task<ActionResult<PersonalInformation>> AddContactDetailsAsync([FromBody] ContactInformationDto contactInformation)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var filter = contactInformation.Adapt<ContactInformation>();
            filter.StudentId = UserId;
            var response = await studentRecordCommand.AddContactInformationAsync(filter);
            return Ok(filter);
        }
        [Authorize]
        [HttpPatch("Update Contact Details")]
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
        [HttpGet("Display Contact Details")]
        public async Task<ActionResult<PersonalInformationDto>> DispalyContactDetailsAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var response = await studentRecordCommand.DisplayContactInformationAsync(UserId);
            return Ok(response);
        }
        [Authorize]
        [HttpPost("Assign StudentID")]
        public async Task<ActionResult<SchoolIdDto>?> StudentSchoolIdAsync(string SchoolId)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var StudentId = await user.UserInfo(UserId);
            if (StudentId is null) return BadRequest("User Cannot Find");
            var request = await studentRecordCommand.StudentSchoolIdAsync(StudentId.Id, SchoolId);
            return Ok(request);
        }
        [Authorize]
        [HttpPatch("UpdateAll Details")]
        public async Task<ActionResult> UpdateAllDetailsAsync(StudentUpdateInformationDto udpate)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var StudentId = await user.UserInfo(UserId);
            if (StudentId is null) return BadRequest("User Cannot Find");
            udpate.StudentId = StudentId.Id;
            var request = await studentRecordCommand.UpdateAllDetailsAsync(udpate);
            if (request is null) return BadRequest("Something Went Wrong");
            return Ok(request);
        }
        [Authorize]
        [HttpPatch("Student SaveInformationAsync")]
        public async Task<ActionResult<bool>> StudentSaveInformationAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if(FindUser is null) { return BadRequest("Login First"); }
            var StudentId = Guid.Parse(FindUser.Value);
            var request = await studentRecordCommand.StudentSaveInformationAsync(StudentId);
            return Ok(request);
        }
    }
}
