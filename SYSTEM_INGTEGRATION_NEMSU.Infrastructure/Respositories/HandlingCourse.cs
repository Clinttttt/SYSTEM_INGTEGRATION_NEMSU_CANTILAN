using Azure.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories
{
    public class HandlingCourse(ApplicationDbContext context) : IHandlingCourse
    {
        public async Task<CourseDto?> AddCourseAsync(Course request)
        {

            var course = new Course
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                CourseCode = request.CourseCode,
                Unit = request.Unit,
                AdminId = request.AdminId,
                FacultyPersonalsId = request.AdminId,
                Cost = request.Cost,
                CourseDescriptiion = request.CourseDescriptiion,
                Department = request.Department,
                Room = request.Room,
                Schedule = request.Schedule,
                SchoolYear = request.SchoolYear,
                Semester = request.Semester,
                CourseStatus = CourseStatus.Active,
                Category = await context.category.FindAsync(request.CategoryId)
                ?? throw new Exception("Category not found"),
                MaxCapacity = request.MaxCapacity,
            };
         
            context.course.Add(course);
            await context.SaveChangesAsync();
            var filter = course.Adapt<CourseDto>();
            return filter;
        }

        public async Task<QuickStatsDto?> DisplayStatsAsync(Guid AdminId, string CourseCode)
        {
            var requestcourse = await context.course.FirstOrDefaultAsync(s => s.AdminId == AdminId && s.CourseCode == CourseCode);
            if (requestcourse is null) return null;
            var stats = new QuickStatsDto
            {
                MaxCapacity = requestcourse.MaxCapacity,
                TotalEnrolled = requestcourse.TotalEnrolled,
                AvailableSlots = requestcourse.AvailableSlots,
            };
            return stats;

        }
        public async Task<IEnumerable<CourseDto>> DisplayCourseAsync(Guid adminid)
        {
            var retrieve = await context.course
                .Include(s=> s.Category)
                .Where(s => s.AdminId == adminid && s.CourseStatus == CourseStatus.Active).ToListAsync();

            return retrieve.Adapt<List<CourseDto>>();
        }

        public async Task<CourseDto?> UpdateCourseAsync(UpdateCourseDto course)
        {
            var request = await context.course.FirstOrDefaultAsync(s => s.AdminId == course.AdminId && s.Id == course.Id);
            if (request is null){ return null; }
            request.CourseCode = course.CourseCode;
            request.Unit = course.Unit;
            request.Title = course.Title;
            request.Cost = course.Cost;
            request.Category = await context.category.FindAsync(course.CategoryId)
                   ?? throw new Exception("Category not found");
            request.Department = course.Department;
            request.CourseDescriptiion = course.CourseDescription;
            request.Semester = course.Semester;
            request.SchoolYear = course.SchoolYear;
            request.Schedule = course.Schedule;
            request.Room = course.Room;
            request.CourseStatus = course.CourseStatus;
            request.MaxCapacity = course.MaxCapacity;
            context.Update(request);
            await context.SaveChangesAsync();
            return request.Adapt<CourseDto>();
        }
        public async Task<CourseDto?> GetCourseAsync(Guid AdminId, Guid CourseId)
        {
            var request = await context.users.FirstOrDefaultAsync(s => s.Id == AdminId);
            if (request is null) return null;

            var GetCourse = await context.course
                .Include(s => s.Category)          
                .FirstOrDefaultAsync(s => s.AdminId == request.Id && s.Id == CourseId);
            if (GetCourse is null) return null;
            var liststudent = await context.enrollcourse.Where(s => s.CourseId == CourseId).ToListAsync();
            var r = GetCourse.Adapt<CourseDto>();
           
            r.Id = GetCourse.Id;
            r.CategoryId = GetCourse.Id;
            r.CourseStatus = GetCourse.CourseStatus;
            return r;
        }
        public async Task<bool> DeleteCourseAsycnc(Guid AdminId, Guid course)
        {
            var request = await context.course.FirstOrDefaultAsync(s => s.Id == course && s.AdminId == AdminId);
            if (request is null)
            {
                return false;
            }
            context.course.Remove(request);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ArchivedCourseAsync(Guid AdminId, string CourseCode)
        {
            var request = await context.course.FirstOrDefaultAsync(s => s.AdminId == AdminId && s.CourseCode == CourseCode);
            if (request is null) return false;
            request.CourseStatus = CourseStatus.Archived;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ActiveCourseAsync(Guid AdminId, string CourseCode)
        {
            var request = await context.course.FirstOrDefaultAsync(s => s.AdminId == AdminId && s.CourseCode == CourseCode);
            if (request is null) return false;
            request.CourseStatus = CourseStatus.Active;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<CourseDto>> DisplayArchiveCourseAsync(Guid adminid)
        {
            var retrieve = await context.course.Where(s => s.AdminId == adminid && s.CourseStatus == CourseStatus.Archived).ToListAsync();

            return retrieve.Adapt<List<CourseDto>>();
        }



    }
}


