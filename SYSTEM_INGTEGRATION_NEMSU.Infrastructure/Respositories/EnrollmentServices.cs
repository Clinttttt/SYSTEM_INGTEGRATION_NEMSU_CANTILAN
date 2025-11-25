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
using System.Runtime.InteropServices;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Storage.Json;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations;
using Azure.Core;


namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories
{
    public class EnrollmentServices(ApplicationDbContext context) : IEnrollmentServices
    {
        public async Task<bool> DirectEnrollAsync(Guid StudentId, Guid CourseId)
        {
            var enrollment = new EnrollmentCourse
            {
                Id = Guid.NewGuid(),
                StudentId = StudentId,
                CourseId = CourseId,
                DateEnrolled = DateTime.UtcNow,
                EnrollmentStatus = EnrollmentStatus.Enrolled,
                StudentCourseStatus = StudentCourseStatus.Active,
                ProfileColor = RandomColor.Generate(),
                enrolledCourseStatus = EnrolledCourseStatus.Inprogress,
            };
            var coursestatus = await context.courseTrackers.FirstOrDefaultAsync(s => s.StudentId == StudentId && s.CourseId == CourseId);
            if (coursestatus is null) return false;

            coursestatus.CourseTrack = CourseTrack.Course_Already_Paid;

            var provision = await context.announcements.FirstOrDefaultAsync(s => s.StudentId == StudentId && s.CourseId == CourseId && s.Type == AnnouncementType.provision);
            if (provision is not null)
            {
                context.announcements.Remove(provision);
            }
            var course = await context.announcements
                 .Include(s => s.course)
                 .FirstOrDefaultAsync(s => s.CourseId == CourseId);

            var Response = new AutoResponsetDto();
            Response.Message = "Welcome, everyone! It’s great to have you all here. Each new term brings " +
                "fresh opportunities to learn and connect," +
                " and I’m excited to see what we’ll accomplish together. Stay open, " +
                "stay curious, and let’s make this a great start.";
            Response.Type = AnnouncementType.System;
            Response.DateCreated = DateTime.UtcNow;
            Response.CourseCode = course?.course.CourseCode;
            Response.StudentId = StudentId;
            var save = new InstructorAnnouncement
            {
                AdminId = course?.course.AdminId,
                Message = Response.Message,
                DateCreated = Response.DateCreated,
                Type = AnnouncementType.System,
                CourseId = course?.course.Id,
                InformationType = InformationType.System,
                CourseCode = course?.course.CourseCode,
                CourseName = course?.course.Title,
                Title = "Welcome Message",
                StudentId = StudentId,
            };
            var announcemntCheck = await context.announcements.FirstOrDefaultAsync(s => s.CourseId == save.CourseId && s.StudentId == StudentId && s.Type == AnnouncementType.System);
            if (announcemntCheck is null)
            {
                context.announcements.Add(save);
            }
            await context.Database.ExecuteSqlInterpolatedAsync(
        $"UPDATE Course SET TotalEnrolled = TotalEnrolled + 1 WHERE Id = {CourseId}");
            context.enrollcourse.Add(enrollment);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<CourseTrack> CourseTrackerAsync(Guid StudentId, Guid CourseId)
        {
            var enroll = await context.enrollcourse
                .FirstOrDefaultAsync(s => s.StudentId == StudentId && s.CourseId == CourseId);

            var tracker = await context.courseTrackers
                .FirstOrDefaultAsync(s => s.StudentId == StudentId && s.CourseId == CourseId);

      
            if (enroll is null && tracker is null)
                return CourseTrack.not_enrolled;

        
            if (tracker is not null)
                return tracker.CourseTrack;


            return CourseTrack.not_enrolled;
        }

        public async Task<bool> CourseTrackAdd(Guid StudentId, Guid CourseId, EnrollmentStatus status)
        {
            var tracker = await context.courseTrackers
                .FirstOrDefaultAsync(s => s.StudentId == StudentId && s.CourseId == CourseId);

            var course = await context.course.FirstOrDefaultAsync(c => c.Id == CourseId);
            if (course is null) return false;

            var courseTrackStatus = status switch
            {
                EnrollmentStatus.Enrolled => CourseTrack.Course_Already_Paid,
                EnrollmentStatus.Provisioned => CourseTrack.Course_Already_Provisioned,
                _ => CourseTrack.not_enrolled
            };

            if (tracker is null)
            {
                tracker = new CourseTracker
                {
                    StudentId = StudentId,
                    CourseId = CourseId,
                    CourseName = course.Title,
                    CourseTrack = courseTrackStatus
                };

                context.courseTrackers.Add(tracker);
            }
            else
            {
                tracker.CourseTrack = courseTrackStatus;
            }

            await context.SaveChangesAsync();
            return true;
        }

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
                enrolledCourseStatus = EnrolledCourseStatus.Inprogress,
            };
            await context.Database.ExecuteSqlInterpolatedAsync(
        $"UPDATE Course SET TotalEnrolled = TotalEnrolled + 1 WHERE Id = {course.Id}");
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
                .Where(s => s.CourseStatus == Domain.Entities.CourseStatus.Active)
                .ToListAsync();

            var result = new List<CourseDto>();

            foreach (var c in course)
            {
                var status = await CourseTrackerAsync(StudentId, c.Id);

                var dto = new CourseDto
                {
                    Id = c.Id,
                    AdminId = c.AdminId,
                    Cost = c.Cost,
                    CourseCode = c.CourseCode,
                    Title = c.Title,
                    Unit = c.Unit,
                    Department = c.Department,
                    CourseDescriptiion = c.CourseDescriptiion,
                    MaxCapacity = c.MaxCapacity,
                    TotalEnrolled = c.TotalEnrolled,
                    SchoolYear = c.SchoolYear,
                    Semester = c.Semester,
                    Schedule = c.Schedule,
                    Room = c.Room,
                    CourseStatus = c.CourseStatus,
                    Status = status,
                    Instructor = c.Instructor,
                    Category = new CategoryDto
                    {
                        Id = c.Category!.Id,
                        Name = c.Category.Name,
                        Icon = c.Category.Icon,
                        Color = c.Category.Color,
                    }
                };

                result.Add(dto);
            }

            return result;
        }
        public async Task<List<EnrollCourseDto>?> DisplayAllCourseEnrolledAsync(Guid StudentId)
        {
            var FindUser = await context.users.FindAsync(StudentId);
            if (FindUser is null) return null;
            var request = await context.enrollcourse
                .Include(s => s.Course)
                .Where(s => s.StudentId == StudentId)
                .ToListAsync();
            var E = new List<EnrollCourseDto>();
            foreach (var r in request)
            {
                var course = await context.course
                    .Include(s => s.FacultyPersonals)
                    .Include(s => s.Category)
                    .AsNoTracking()
                    .Where(s => s.CourseCode!.Contains(r.Course.CourseCode!))
                    .Select(s => new EnrollCourseDto
                    {
                        StudentId = r.StudentId,
                        CourseId = s.Id,
                        DateEnrolled = r.DateEnrolled,
                        Category = s.Category,
                        Title = s.Title,
                        Unit = s.Unit,
                        EnrollmentStatus = r.EnrollmentStatus,
                        CourseCode = s.CourseCode,
                        FacultyFullName = s.FacultyPersonals!.FirstName + " " + s.FacultyPersonals.LastName,
                        Schedule = s.Schedule,
                        Room = s.Room,
                         Instructor = s.Instructor,

                    }).ToListAsync();
                E.AddRange(course);
            }
            return E;
        }
        public async Task<EnrolledCourseViewDto?> GetCourse(Guid CourseId, Guid StudentId)
        {
            var request = await context.enrollcourse.FirstOrDefaultAsync(s => s.CourseId == CourseId && s.StudentId == StudentId);
            if (request is null) return null;
            var retrieve = await context.course
                .AsNoTracking()
                .Include(s => s.Category)
                .Include(s => s.FacultyPersonals)
                .FirstOrDefaultAsync(s => s.Id == request.CourseId);
            if (retrieve is null)
                return null;
            var filter = new EnrolledCourseViewDto
            {
                StudentId = request.StudentId,
                CourseId = retrieve.Id,
                CourseCode = retrieve.CourseCode,
                enrolledCourseStatus = request.enrolledCourseStatus,
                CourseName = retrieve.Title,
                Schedule = retrieve.Schedule,
                Room = retrieve.Room,
                Unit = retrieve.Unit,
                SchoolYear = retrieve.SchoolYear,
                FacultyName = retrieve.FacultyPersonals?.FirstName + " " + retrieve.FacultyPersonals?.LastName,
                CourseDescription = retrieve.CourseDescriptiion,
                Category = retrieve.Category,
                StudentStatus = request.StudentCourseStatus,
                Instructor = retrieve.Instructor,
            };
            return filter;
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

            await context.Database.ExecuteSqlInterpolatedAsync(
        $"UPDATE Course SET TotalEnrolled = TotalEnrolled - 1 WHERE Id = {enrollment.CourseId}");

            var invoice = await context.invoice
                .FirstOrDefaultAsync(i => i.StudentId == enrollment.StudentId && i.CourseId == enrollment.CourseId);

            if (invoice != null) { context.invoice.Remove(invoice); }
            var announcements = await context.announcements.Where(s => s.StudentId == studentId && s.CourseCode == courseCode).ToListAsync();
            foreach (var announcement in announcements)
            {
                context.announcements.Remove(announcement);
            }


            var coursestatus = await context.courseTrackers.FirstOrDefaultAsync(s => s.StudentId == studentId && s.CourseId == enrollment.CourseId);
            if (coursestatus is null) return false;

            if (coursestatus.CourseTrack == CourseTrack.Course_Already_Paid)
            {
                coursestatus.CourseTrack = CourseTrack.Deleted_Paid;
            }
            else if (coursestatus.CourseTrack == CourseTrack.Course_Already_Provisioned)
            {
                coursestatus.CourseTrack = CourseTrack.Deleted_Provisioned;
            }

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

        public async Task<bool> ActiveCourseAsync(Guid StudentId, Guid CourseId)
        {
            var request = await context.enrollcourse.FirstOrDefaultAsync(s => s.StudentId == StudentId && s.CourseId == CourseId);
            if (request is null) { return false; }
            request.StudentCourseStatus = StudentCourseStatus.Active;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<PaymentDetailsDto?> AddPaymentAsync(PaymentDetailsDto payment)
        {
            var request = await context.users.FindAsync(payment.StudentId);
            if (request is null)
            {
                return null;
            }
            var course = await context.course

                .Where(s => s.CourseCode == payment.CourseCode)
                .FirstOrDefaultAsync();

            var Add = new PaymentDetails
            {
                StudentId = payment.StudentId,
                AccountNumber = payment.AccountNumber ?? "N/A",
                paymentMethod = payment.paymentMethod,
                PurchaseDate = DateTime.UtcNow.AddHours(8),
                CourseCode = payment.CourseCode,
                Cost = payment.cost,
                CategoryId = course?.CategoryId,
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
                Category = course?.Category,
            };
            return filter;
        }
        public async Task<List<PaymentDetailsDto>?> DisplayPaymentAsync(Guid StudentId)
        {
            var User = await context.users.FindAsync(StudentId);
            if (User is null)
            {
                return null;
            }
            var request = await context.paymentDetails

                .Where(s => s.StudentId == StudentId)
                .Select(s => new PaymentDetailsDto
                {
                    PaymentId = s.Id,
                    AccountNumber = s.AccountNumber,
                    paymentMethod = s.paymentMethod,
                    PurchaseDate = s.PurchaseDate,
                    CourseCode = s.CourseCode,
                    cost = s.Cost,
                    Category = s.Category
                })
                .ToListAsync();
            return request;
        }
        public async Task<bool> DeletePaymentAsync(Guid StudentId, Guid PaymentId)
        {
            var request = await context.paymentDetails
                .FirstOrDefaultAsync(s => s.StudentId == StudentId && s.Id == PaymentId);
            if (request is null) return false;
            context.paymentDetails.Remove(request);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<List<AnnouncementDto>?> DisplayAllAnnouncementAsync(Guid StudentId)
        {
            var FindUser = await context.users.FindAsync(StudentId);
            if (FindUser is null) return null;
            var response = await context.enrollcourse
                .Include(s => s.Course)
                .Where(s => s.StudentId == StudentId).ToListAsync();
            if (response is null) return null;
            var t = new List<AnnouncementDto>();

            foreach (var r in response)
            {
                var request = await context.announcements
            .AsNoTracking()
            .Where(s => (s.Type == AnnouncementType.instructor || s.Type == AnnouncementType.provision) && s.AdminId == r.Course.AdminId && s.course.CourseCode == r.Course.CourseCode && (s.StudentId == StudentId || s.StudentId == null))
            .ToListAsync();
                t.AddRange(request.Adapt<List<AnnouncementDto>>());
            }
            return t;
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
        public async Task<List<AnnouncementDto>?> DisplayAllTypeAnnouncementAsync(Guid CourseId, Guid StudentId)
        {
            var FindUser = await context.users.FindAsync(StudentId);
            if (FindUser is null) return null;
            var CourseAdmin = await context.course.FindAsync(CourseId);

            if (CourseAdmin is null) return null;
            var request = await context.announcements
                .Include(s => s.course)
                .AsNoTracking()
                .Where(s => s.CourseId == CourseId && s.InformationType != InformationType.Warning && s.AdminId == CourseAdmin.AdminId && (s.StudentId == null || s.StudentId == StudentId))
                .Select(s => new AnnouncementDto
                {
                    Title = s.Title,
                    Message = s.Message,
                    CourseName = s.CourseName,
                    CourseId = s.course.Id,
                    InformationType = s.InformationType,
                    DateCreated = s.DateCreated,
                    CourseCode = s.CourseCode,
                    AnnouncementId = s.Id,
                    FacultyName = s.course.FacultyPersonals!.FirstName + " " + s.course.FacultyPersonals.LastName,
                    Type = (AnnouncementType)s.Type!,
                })

                .ToListAsync();
            return request;
        }
        public async Task<List<AnnouncementDto>?> DisplayAnnouncementByType(Guid StudentId, InformationType type)
        {
            var FindUser = await context.users.FindAsync(StudentId);
            if (FindUser is null) return null;
            var response = await context.enrollcourse
                 .Include(s => s.Course)
                 .Where(s => s.StudentId == StudentId).ToListAsync();
            if (response is null) return null;
            var t = new List<AnnouncementDto>();
            foreach (var r in response)
            {
                var request = await context.announcements
            .AsNoTracking()

            .Where(s => (s.Type == AnnouncementType.instructor || s.Type == AnnouncementType.provision) && s.AdminId == r.Course.AdminId && s.course.CourseCode!.Contains(r.Course.CourseCode!) && s.InformationType == type && (s.StudentId == null || s.StudentId == StudentId))
            .ToListAsync();
                t.AddRange(request.Adapt<List<AnnouncementDto>>());
            }
            return t;
        }
        public async Task<SchoolIdDto?> GenerateStudentId(Guid StudentId)
        {
            var request = await context.academicInformation.FirstOrDefaultAsync(s => s.StudentId == StudentId);
            if (request is null) return null;

            int currentYear = DateTime.Now.Year;

            var lastStudent = await context.academicInformation
                .Where(s => s.StudentSchoolId != null && s.StudentSchoolId.StartsWith(currentYear.ToString()) == true)
                .OrderByDescending(s => s.StudentSchoolId)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (lastStudent != null)
            {

                string lastId = lastStudent.StudentSchoolId!;
                string numberPart = lastId.Split('-')[1];
                nextNumber = int.Parse(numberPart) + 1;
            }

            var Id = new SchoolIdDto
            {
                StudentSchoolId = $"{currentYear}-{nextNumber:D4}"
            };
            request.StudentSchoolId = Id.StudentSchoolId;
            await context.SaveChangesAsync();
            return Id;
        }



    }
}
