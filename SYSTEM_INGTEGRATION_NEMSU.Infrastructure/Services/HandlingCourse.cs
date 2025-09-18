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

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services
{
    public class HandlingCourse(ApplicationDbContext context) : IHandlingCourse
    {
      public async Task<CourseDto> AddCourseAsync( Course request)
        {

            context.course.Add(request);
            await context.SaveChangesAsync();
            return request.Adapt<CourseDto>();          
        }
        public async Task<IEnumerable<CourseDto>> DisplayCourseAsync(Guid adminid)
        {
            var retrieve = await context.course.Where(s => s.AdminId == adminid).ToListAsync();
            return retrieve.Adapt<List<CourseDto>>();
        }
        public async Task<Course?> UpdateCourseAsync(UpdateCourseDto course)
        {
            var request = await context.course.FindAsync(course.Id);
            if(request is null)
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
        public async Task<bool> DeleteCourseAsycnc( Guid course)
        {
            var request = await context.course.FindAsync(course);
            if(request is null)
            {
                return false;
            }
            context.course.Remove(request);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
