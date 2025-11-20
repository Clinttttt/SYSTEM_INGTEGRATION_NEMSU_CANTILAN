using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandlingMessageController(IUserRespository respository, IRespondCommand respondCommand) : ControllerBase
    {
      
        [Authorize]
        [HttpPost("Auto Response")]
        public async Task<ActionResult<AutoResponsetDto>> AutoResponse(string CourseCode)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return Unauthorized("Login First");

            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if (user is null) return BadRequest("User not found or Student ID missing");

            var AdminId = user.Id;
            var response = await respondCommand.AutoResponseAsync(AdminId, CourseCode);
            if (response is null) { return BadRequest("Something went wrong"); }
            return Ok(response);
        }
       
        [Authorize]
        [HttpPost("Announcement")]
        public async Task<ActionResult<AnnouncementDto>> Announcement(CreateAnnouncementDto announcement)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return Unauthorized("Login First");

            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if (user is null) return BadRequest("User not found or Student ID missing");

            var AdminId = user.Id;
            announcement.AdminId = AdminId;
            var response = await respondCommand.AddAnnouncementAsync(announcement);
            return Ok(response);
        }
       
        [Authorize]
        [HttpGet("Display Announcement")]
        public async Task<ActionResult<AnnouncementDto>> DisplayAnnouncementAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return Unauthorized("Login First");

            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if (user is null) return BadRequest("User not found or Student ID missing");
            var response = await respondCommand.DisplayAnnouncementAsync(user.Id);
            return Ok(response);
        }
      
        [Authorize]
        [HttpDelete("Delete Announcement/{AnnouncementId}")]
        public async Task<ActionResult<bool>> DeleteAnnouncementAsync(Guid AnnouncementId)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return Unauthorized("Login First");

            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if (user is null) return BadRequest("User not found or Student ID missing");
            var response = await respondCommand.DeleteAnnouncementAsync(user.Id, AnnouncementId);
            return Ok(response);
        }
       
        [Authorize]
        [HttpPatch("Edit Announcement")]
        public async Task<ActionResult<AnnouncementDto>> EditAnnouncementAsync(EditAnnouncementDto editAnnouncement)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return Unauthorized("Login First");

            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if (user is null) return BadRequest("User not found or Student ID missing");
            editAnnouncement.AdminId = user.Id;
            var response = await respondCommand.EditAnnouncementAsync(editAnnouncement);
            return Ok(response);
        }
      
        [Authorize]
        [HttpGet("Provision Announcement")]
        public async Task<ActionResult<AnnouncementDto>> ProvisionAnnouncementAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return Unauthorized("Login First");

            var GetUserId = Guid.Parse(FindUser.Value);
            var response = await respondCommand.ProvisionAnnouncementAsync(GetUserId);
            return Ok(response);

        }
    }
}
