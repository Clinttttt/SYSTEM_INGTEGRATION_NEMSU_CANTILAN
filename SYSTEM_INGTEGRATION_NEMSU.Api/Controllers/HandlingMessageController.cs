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
    public class HandlingMessageController( IRespondCommand respondCommand) : BaseController
    {
      
        [Authorize]
        [HttpPost("Auto Response")]
        public async Task<ActionResult<AutoResponsetDto>> AutoResponse([FromQuery] string CourseCode)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var AdminId = userId.Value;
            var response = await respondCommand.AutoResponseAsync(AdminId, CourseCode);
            if (response is null) { return BadRequest("Something went wrong"); }
            return Ok(response);
        }
       
        [Authorize]
        [HttpPost("Announcement")]
        public async Task<ActionResult<AnnouncementDto>> Announcement( [FromBody] CreateAnnouncementDto announcement)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var AdminId = userId.Value;
            announcement.AdminId = AdminId;
            var response = await respondCommand.AddAnnouncementAsync(announcement);
            return Ok(response);
        }
       
        [Authorize]
        [HttpGet("Display Announcement")]
        public async Task<ActionResult<AnnouncementDto>> DisplayAnnouncementAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await respondCommand.DisplayAnnouncementAsync(userId.Value);
            return Ok(response);
        }
      
        [Authorize]
        [HttpDelete("Delete Announcement/{AnnouncementId}")]
        public async Task<ActionResult<bool>> DeleteAnnouncementAsync( [FromRoute] Guid AnnouncementId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await respondCommand.DeleteAnnouncementAsync(userId.Value, AnnouncementId);
            return Ok(response);
        }
       
        [Authorize]
        [HttpPatch("Edit Announcement")]
        public async Task<ActionResult<AnnouncementDto>> EditAnnouncementAsync( [FromBody] EditAnnouncementDto editAnnouncement)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            editAnnouncement.AdminId = userId.Value;
            var response = await respondCommand.EditAnnouncementAsync(editAnnouncement);
            return Ok(response);
        }
      
        [Authorize]
        [HttpGet("Provision Announcement")]
        public async Task<ActionResult<AnnouncementDto>> ProvisionAnnouncementAsync()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized("Login First");
            var response = await respondCommand.ProvisionAnnouncementAsync(userId.Value);
            return Ok(response);

        }
    }
}
