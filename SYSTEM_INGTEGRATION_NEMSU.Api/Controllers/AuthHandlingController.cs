using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthHandlingController(IAuthServices authservice) : ControllerBase
    {
      
        [HttpPost("Register")]
        public async Task<ActionResult<User>> RegisterAsync( [FromBody] UserDtos request)
        {
            var response = await authservice.RegisterAsync(request);
            if (response is null)
            {
                return BadRequest("Account Already Exists");
            }
            return Ok(request);
        }
      
        [HttpPost("Login")]
        public async Task<ActionResult<User>> LoginAsync([FromBody] LoginDto request)
        {
            var response = await authservice.LoginAsync(request);
            if (response is null)
            {
                return BadRequest("Login Failed");
            }
            return Ok(response);
        }
     
        [HttpPost("Refreshtoken")]
        public async Task<ActionResult<TokenResponseDto>> RefreshTokenAsync([FromBody] RefreshTokenDto request)
        {
            var user = await authservice.RefreshTokenAsync(request);
            if (user is null || user.RefreshToken is null || request.RefreshToken is null)
            {
                return BadRequest("Unauthorized User");
            }
            return Ok(user);
        }
       
        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            var find = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (find is null || !Guid.TryParse(find.Value, out var Results))
            {
                return BadRequest("Something Went Wrong");
            }
            var response = await authservice.LogoutAsync(Results);
            if (!response)
            {
                return BadRequest("Logout Failed");
            }
            return Ok(new { Message = " Login Successfully" });
        }
    }
}
