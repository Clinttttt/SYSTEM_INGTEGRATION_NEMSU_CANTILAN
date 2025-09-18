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
   public class EnrollmentServices(ApplicationDbContext context) : IEnrollmentServices
    {
     public async Task<EnrollCourse?> EnrollCourseAsync(string StudentID,Guid CourseId)
        {
            var request = await context.course.FindAsync(CourseId);
            if(request is null)
            {
                return null;
            }
            var finduser = await context.users.FirstOrDefaultAsync(s => s.StudentId == StudentID.ToString());
            if(finduser is null)
            {
                return null;
            }
            var enrollment = new EnrollmentCourse()
            {
                Id = Guid.NewGuid(),
                StudentID = StudentID,
                CourseID = CourseId,
                DateEnrolled = DateTime.UtcNow

            };
            if (await context.enrollcourse.AnyAsync(s => s.StudentID == StudentID))
            {
                return null;
            }
            context.enrollcourse.Add(enrollment);
            await context.SaveChangesAsync();
            var courseDto = new EnrollCourse()
            {
                EnrollmentID = enrollment.Id,
                StudentID = enrollment.StudentID,
                CourseID = enrollment.CourseID,
                DateEnrolled = enrollment.DateEnrolled,
                CourseCode = request.CourseCode,
                Title = request.Title,
                Unit = request.Unit,
                
            };                    
            return courseDto;
        }
        public async Task<IEnumerable<EnrollmentCourse>?> DisplayCourseAsync(int StudentID)
        {
            var request =  await context.enrollcourse.Where(s => s.StudentID == StudentID.ToString()).ToListAsync();
            if(request is null)
            {
                return null;
            }
            return request;
         
        }
    }
}
