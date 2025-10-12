using Azure.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.CommandHandlers;
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
                Cost = request.Cost,
                CourseDescriptiion = request.CourseDescriptiion,
                Department = request.Department,
                Room = request.Room,
                Schedule = request.Schedule,
                SchoolYear = request.SchoolYear,
                Semester = request.Semester,
                Category = await context.category.FindAsync(request.CategoryId)
                ?? throw new Exception("Category not found")
            };
            if (request.LearningObjectives is not null && request.LearningObjectives.Any())
            {
                foreach (var obj in request.LearningObjectives)
                {

                    course.LearningObjectives.Add(new LearningObjectives
                    {
                        description = obj.description,
                    });
                }
            }
            context.course.Add(course);
            await context.SaveChangesAsync();
            var filter = course.Adapt<CourseDto>();
            return filter;
        }

        public async Task<IEnumerable<CourseDto>> DisplayCourseAsync(Guid adminid)
        {
            var retrieve = await context.course.Where(s => s.AdminId == adminid).ToListAsync();
            return retrieve.Adapt<List<CourseDto>>();
        }
        public async Task<Course?> UpdateCourseAsync(UpdateCourseDto course)
        {
            var request = await context.course.FirstOrDefaultAsync(s => s.AdminId == course.Id);
            if (request is null)
            {
                return null;
            }
            request.CourseCode = course.CourseCode;
            request.Unit = course.Unit;
            request.Title = course.Title;
            request.Cost = course.Cost;
            context.Update(request);
            await context.SaveChangesAsync();
            return request.Adapt<Course>();
        }
        public async Task<CourseDto?> GetCourseAsync(Guid AdminId, Guid CourseId)
        {
            var request = await context.users.FirstOrDefaultAsync(s => s.Id == AdminId);
            if (request is null) return null;
            var GetCourse = await context.course.AsNoTracking()
                .Include(s => s.Category)
                .Include(s => s.LearningObjectives)
                .FirstOrDefaultAsync(s => s.AdminId == request.Id && s.Id == CourseId);
            if (GetCourse is null) return null;
            return GetCourse.Adapt<CourseDto>();
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

    }
}


