﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories;

namespace SYSTEM_INGTEGRATION_NEMSU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentHandlingController(IPaymentServices enrollmenthandling, IEnrollmentServices enrollmentservices, IUserRespository respository) : ControllerBase
    {
        [Authorize]
        [HttpPost("EnrollCourse")]
        public async Task<ActionResult<Invoice>> EnrollCourse(string CourseId, double Payment)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null)  return Unauthorized("Login First");
            
            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if (user is null) return BadRequest("User not found or Student ID missing");
            
            var StudentId = user.Id;
            var response = await enrollmenthandling.InvoiceAsync(StudentId, CourseId, Payment);
            if (response is null) { return BadRequest("Something went wrong"); }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("DisplayCourse")]
        public async Task<ActionResult<EnrollmentCourse>> DisplayCourse()
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if(FindUser is null) return Unauthorized("Login First");
            
            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if(user is null) return BadRequest("User not found or Student ID missing");
            
            var StudentId = user.Id;
            var response = await enrollmentservices.DisplayCourseAsync(StudentId);
            if (response is null) { return BadRequest("Nothing to Display"); }
            return Ok(response);
        }
        [Authorize]
        [HttpGet("GetCourse")]
        public async Task<ActionResult<CourseDto>?> GetCourse(Guid CourseId)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if (FindUser is null)
            {
                return BadRequest("Login First");
            }
            var UserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(UserId);
            if (user is null) return BadRequest("User not found or Student ID missing");
            var StudentId = user.Id;
            var response = await enrollmentservices.GetCourse(CourseId, StudentId);
            return Ok(response);
        }
        [Authorize]
        [HttpDelete("UnEnrollCourse/{CourseId}")]
        public async Task<IActionResult> UnenrollCourse(string CourseId)
        {
            var FindUser = User.FindFirst(ClaimTypes.NameIdentifier);
            if(FindUser is null) return Unauthorized("Login First");
            
            var GetUserId = Guid.Parse(FindUser.Value);
            var user = await respository.UserInfo(GetUserId);
            if(user is null || string.IsNullOrEmpty(CourseId)) return BadRequest("User not found or Student ID missing");
            
            var StudentId = user.Id;
            var response = await enrollmentservices.UnEnrollCourseAsync(StudentId,CourseId);
            if (response is false) return BadRequest("Something went wrong");      
            return Ok(response);
        }
    }
}
