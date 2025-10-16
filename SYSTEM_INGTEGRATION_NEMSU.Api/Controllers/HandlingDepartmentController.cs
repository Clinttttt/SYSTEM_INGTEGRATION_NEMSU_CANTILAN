using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandlingDepartmentController(IHandlingDepartment handlingDepartment, IUserRespository user) : ControllerBase
    {
        [Authorize]
        [HttpGet("Display DIT")]
        public async Task<ActionResult<CourseDto>> DisplayDITAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var AdminId = await user.UserInfo(UserId);
            if (AdminId is null) return BadRequest("User not found");
            var request = await handlingDepartment.DisplayDITAsync(AdminId.Id);
            return Ok(request);
        }
        [Authorize]
        [HttpGet("Display DCS")]
        public async Task<ActionResult<CourseDto>> DisplayDCSAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var AdminId = await user.UserInfo(UserId);
            if (AdminId is null) return BadRequest("User not found");
            var request = await handlingDepartment.DisplayDITAsync(AdminId.Id);
            return Ok(request);
        }
        [Authorize]
        [HttpGet("Display DGTT")]
        public async Task<ActionResult<CourseDto>> DisplayDGTTAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var AdminId = await user.UserInfo(UserId);
            if (AdminId is null) return BadRequest("User not found");
            var request = await handlingDepartment.DisplayDITAsync(AdminId.Id);
            return Ok(request);
        }
        [Authorize]
        [HttpGet("Display DBM")]
        public async Task<ActionResult<CourseDto>> DisplayDBMAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var AdminId = await user.UserInfo(UserId);
            if (AdminId is null) return BadRequest("User not found");
            var request = await handlingDepartment.DisplayDITAsync(AdminId.Id);
            return Ok(request);
        }
        [Authorize]
        [HttpGet("Display CCJE")]
        public async Task<ActionResult<CourseDto>> DisplayCCJEAsync()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null) return BadRequest("Login First");
            var UserId = Guid.Parse(FindUser.Value);
            var AdminId = await user.UserInfo(UserId);
            if (AdminId is null) return BadRequest("User not found");
            var request = await handlingDepartment.DisplayDITAsync(AdminId.Id);
            return Ok(request);
        }
    }
}
