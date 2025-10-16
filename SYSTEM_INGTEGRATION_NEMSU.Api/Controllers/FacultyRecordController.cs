using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyRecordController(IFacultyRecordCommand facultyRecord, IUserRespository user) : ControllerBase
    {
        [Authorize]
        [HttpPost("Add Faculty Information")]
        public async Task<ActionResult<FacultyRecordDto>> AddFacultyDetailsAsync(FacultyRecordDto facultydata)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var FacultyId = await user.UserInfo(UserId);
            if (FacultyId is null) return BadRequest("User Cannot Find");
            facultydata.FacultyId = FacultyId.Id;
            var response = await facultyRecord.AddFacultyInformationAsync(facultydata);
            return Ok(response);
        }
        [Authorize]
        [HttpPatch("Update Faculty Information")]
        public async Task<ActionResult<FacultyRecordDto>> UpdateFacultyDetailsAsync(FacultyRecordDto facultydata)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var FacultyId = await user.UserInfo(UserId);
            if (FacultyId is null) return BadRequest("User Cannot Find");
            facultydata.FacultyId = FacultyId.Id;
            var response = await facultyRecord.UpdateFacultyInformationAsync(facultydata);
            return Ok(response);
        }
        [Authorize]
        [HttpGet("Display Faculty Information")]
        public async Task<ActionResult<FacultyRecordDto>> DisplayFacultyDetailsAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var FacultyId = await user.UserInfo(UserId);
            if (FacultyId is null) return BadRequest("User Cannot Find");
            var response = await facultyRecord.DisplayFacultyDetailsAsync(FacultyId.Id);
            return Ok(response);
        }
    }
}
