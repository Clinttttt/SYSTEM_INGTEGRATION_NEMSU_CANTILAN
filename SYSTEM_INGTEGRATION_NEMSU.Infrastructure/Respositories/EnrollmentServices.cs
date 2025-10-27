using Microsoft.EntityFrameworkCore;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;
using Mapster;
using SYSTEM_INGTEGRATION_NEMSU.Client.Helper;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Text.Json.Serialization;

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

            if (course.AvailableSlots <= 0) { return null; }

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
                StudentCourseStatus = StudentCourseStatus.Active,
                ProfileColor = RandomColor.Generate(),

            };
            course.TotalEnrolled += 1;
            context.enrollcourse.Add(enrollment);
            await context.SaveChangesAsync();


            var dto = new EnrollCourseDto
            {
                CourseCode = course.CourseCode,
                Title = course.Title,
                Unit = course.Unit,

                StudentId = studentId,
                CourseId = enrollment.CourseId,
                DateEnrolled = enrollment.DateEnrolled,
                EnrollmentStatus = status,

            };


            return dto;
        }

        public async Task<IEnumerable<CourseDto>?> DisplayCourseAsync(Guid StudentId)
        {
            var FindUser = await context.users.FindAsync(StudentId);
            if (FindUser is null)
                return Enumerable.Empty<CourseDto>();

            var course = await context.course
                .Include(s => s.Category)
                .ToListAsync();

            return course.Adapt<List<CourseDto>>();

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
        public async Task<CourseDto?> PreviewCourseAsync(Guid StudentId, Guid CourseId)
        {
            var request = await context.users.FindAsync(StudentId);
            if (request is null)
            {
                return null;
            }
            var course = await context.course.FirstOrDefaultAsync(s => s.Id == CourseId);
            return course.Adapt<CourseDto>();
        }
        public async Task<bool> UnEnrollCourseAsync(Guid studentId, string courseCode)
        {
            var enrollment = await context.enrollcourse
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.Course.CourseCode == courseCode);
            if (enrollment is null) return false;

            enrollment.Course.TotalEnrolled -= 1;


            context.enrollcourse.Remove(enrollment);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InactiveCourseAsync(Guid StudentId, Guid CourseId)
        {
            var request = await context.enrollcourse.FirstOrDefaultAsync(s => s.StudentId == StudentId && s.CourseId == CourseId);
            if (request is null) { return false; }
            request.StudentCourseStatus = StudentCourseStatus.Inactive;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IctiveCourseAsync(Guid StudentId, Guid CourseId)
        {
            var request = await context.enrollcourse.FirstOrDefaultAsync(s => s.StudentId == StudentId && s.CourseId == CourseId);
            if (request is null) { return false; }
            request.StudentCourseStatus = StudentCourseStatus.Active;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<List<AnnouncementDto>> DisplayAnnounceMentAsync(Guid StudentId, string CourseCode)
        {
            var request = await context.announcements
                .Include(s => s.course)
                .ThenInclude(s => s.Enrollments)
                .Where(s => s.course.CourseCode == CourseCode && s.course.Enrollments.Any(s => s.StudentId == StudentId))
                .Select(s => new AnnouncementDto
                {
                    Title = s.Title,
                    Message = s.Message,
                    CourseName = s.course.Title,
                    InformationType = s.InformationType,
                    DateCreated = s.DateCreated,
                    CourseCode = s.course.CourseCode,

                })
                .ToListAsync();
            return request;
        }
        public async Task<PaymentDetailsDto?> AddPaymentAsync(PaymentDetailsDto payment)
        {
            var request = await context.users.FindAsync(payment.StudentId);
            if (request is null)
            {
                return null;
            }
            var Add = new PaymentDetails
            {
                StudentId = payment.StudentId,
                AccountNumber = payment.AccountNumber ?? "N/A",
                paymentMethod = payment.paymentMethod,
                PurchaseDate = DateTime.UtcNow,
                CourseCode = payment.CourseCode,
                Cost = payment.cost,
            };
            context.paymentDetails.Add(Add);
            await context.SaveChangesAsync();
            var filter = new PaymentDetailsDto
            {
                StudentId = payment.StudentId,
                AccountNumber = payment.AccountNumber ?? "N/A",
                paymentMethod = payment.paymentMethod,
                PurchaseDate = Add.PurchaseDate,
                CourseCode = payment.CourseCode,
                cost = payment.cost,
            };
            return filter;
        }
        public async Task<List<PaymentDetailsDto>?> DisplayPaymentAsync(Guid StudentVerification, Guid StudentId)
        {
            var User = await context.users.FindAsync(StudentVerification);
            if (User is null)
            {
                return null;
            }
            var request = await context.paymentDetails.Where(s => s.StudentId == StudentId).ToListAsync();
            return request.Adapt<List<PaymentDetailsDto>>();
        }
        public async Task<List<AnnouncementDto>?> DisplayAllAnnouncementAsync(Guid CourseId,Guid StudentId)
        {
            var FindUser = await context.users.FindAsync(StudentId);
            if (FindUser is null) return null;
            var CourseAdmin = await context.course.FindAsync(CourseId);
            if (CourseAdmin is null) return null;
            var request = await context.announcements
                .AsNoTracking()
                .Where(s => s.Type == AnnouncementType.instructor && s.AdminId == CourseAdmin.AdminId).ToListAsync();
            return request.Adapt<List<AnnouncementDto>>();
        }
        public async Task<List<AnnouncementDto>?> DisplayAnnouncementAsync(Guid CourseId, Guid StudentId)
        {
            var FindUser = await context.users.FindAsync(StudentId);
            if (FindUser is null) return null;
            var CourseAdmin = await context.course.FindAsync(CourseId);
            if (CourseAdmin is null) return null;
            var request = await context.announcements
                .AsNoTracking()
                .Where(s => s.CourseId == CourseId && s.AdminId == CourseAdmin.AdminId && s.Type == AnnouncementType.instructor)
                .ToListAsync();
            return request.Adapt<List<AnnouncementDto>>();
        }
      





    }
}
