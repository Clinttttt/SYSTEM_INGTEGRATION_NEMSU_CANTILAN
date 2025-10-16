using Microsoft.EntityFrameworkCore;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;
using Mapster;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories
{
    public class EnrollmentServices(ApplicationDbContext context) : IEnrollmentServices
    {
        public async Task<EnrollCourseDto?> EnrollCourseAsync(Guid studentId, string courseCode, EnrollmentStatus status = EnrollmentStatus.Provisioned)
        {

            var course = await context.course
                .FirstOrDefaultAsync(c => c.CourseCode == courseCode);
            if (course is null) return null;


            var student = await context.users
                .FirstOrDefaultAsync(u => u.Id == studentId && u.Role == UserRole.Student);
            if (student is null) return null;


            bool alreadyEnrolled = await context.enrollcourse
                .AnyAsync(e => e.StudentId == studentId && e.CourseId == course.Id);
            if (alreadyEnrolled) return null;


            var enrollment = new EnrollmentCourse
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                CourseId = course.Id,
                DateEnrolled = DateTime.UtcNow,
                EnrollmentStatus = status,
                
            };
            course.TotalEnrolled += 1;
            course.AvailableSlots = course.MaxCapacity - course.TotalEnrolled;
            context.enrollcourse.Add(enrollment);
            await context.SaveChangesAsync();


            var dto = enrollment.Adapt<EnrollCourseDto>();
            dto.CourseCode = course.CourseCode;
            dto.Title = course.Title;
            dto.Unit = course.Unit;
            dto.Category = course.Category;
            return dto;
        }


        public async Task<IEnumerable<EnrollCourseDto>> DisplayCourseAsync(Guid studentId)
        {
            var enrollments = await context.enrollcourse
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId)
                .ToListAsync();

            return enrollments.Adapt<List<EnrollCourseDto>>();
        }
        public async Task<CourseDto?> GetCourse(Guid CourseId, Guid StudentId)
        {
            var request = await context.enrollcourse.FirstOrDefaultAsync(s => s.CourseId == CourseId && s.StudentId == StudentId);
            if (request is null) return null;
            var retrieve = await context.course.AsNoTracking()
                .Include(s => s.Category).
                FirstOrDefaultAsync(s => s.Id == request.CourseId);
            if (retrieve is null)
                return null;
            return retrieve.Adapt<CourseDto>();
        }
        public async Task<bool> UnEnrollCourseAsync(Guid studentId, string courseCode)
        {
            var enrollment = await context.enrollcourse
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.Course.CourseCode == courseCode);
            if (enrollment is null) return false;
                  
            enrollment.Course.TotalEnrolled -= 1;
            enrollment.Course.AvailableSlots = enrollment.Course.MaxCapacity - enrollment.Course.TotalEnrolled;

            context.enrollcourse.Remove(enrollment);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
