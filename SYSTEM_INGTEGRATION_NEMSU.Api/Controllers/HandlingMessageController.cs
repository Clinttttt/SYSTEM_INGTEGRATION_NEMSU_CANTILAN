using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SYSTEM_INGTEGRATION_NEMSU.Application.CommandHandlers;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandlingMessageController(IHandlingMessage messageresponse, IUserRespository respository) : ControllerBase
    {
        [Authorize]
        [HttpPost("AutoResponse")]
        public async Task<ActionResult<AutoResponsetDto>> AutoResponse(string CourseCode)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if(FindUser is null) return Unauthorized("Login First");
            
            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if(user is null) return BadRequest("User not found or Student ID missing");

            var AdminId = user.Id;         
            var response = await messageresponse.AutoResponseCommand(AdminId, CourseCode);
            if(response is null) { return BadRequest("Something went wrong"); }
            return Ok(response);
        }

        [Authorize]
        [HttpPost("Announcement")]
        public async Task<ActionResult<AnnouncementDto>> Announcement(string CourseCode, string Message)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if(FindUser is null) return Unauthorized("Login First");
            
            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if (user is null) return BadRequest("User not found or Student ID missing");
            
            var Student = user.Id;
            var response = await messageresponse.AnnouncementCommand(Student, Message, CourseCode);
            return Ok(response);
        }
    }
}
