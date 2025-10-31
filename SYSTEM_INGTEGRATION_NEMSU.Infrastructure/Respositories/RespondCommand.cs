using Azure;
using Mapster;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDto;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories
{
    public class RespondCommand(ApplicationDbContext context) : IRespondCommand
    {

        public async Task<bool?> AutoResponseAsync(Guid StudentId, string CourseCode)
        {
          
            var course = await context.course.FirstOrDefaultAsync(s => s.CourseCode == CourseCode);
            if (course is null) return false;

            var request = await context.invoice
                .Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.CourseCode == CourseCode && s.StudentId == StudentId);
            if (request is null) return false;

           
            if (request.Status == InvoiceStatus.Unpaid)
            {
                var Response = new AutoResponsetDto();
                Response.Message = "Your Course will Automaticaly Unenrolled if not paid After 15 Days";
                Response.Type = AnnouncementType.System;
                Response.DateCreated = DateTime.UtcNow;
                Response.CourseName = request.Course.Title;

                var save_unpaid = new InstructorAnnouncement
                {
                    AdminId = course.AdminId,
                    Message = Response.Message,
                    DateCreated = Response.DateCreated,
                    Type = AnnouncementType.System,
                    CourseId = course.Id,
                    InformationType = InformationType.System,
                    CourseCode = course.CourseCode,
                    CourseName = course.Title,
                    Title = "Enrollment Warning",
                    StudentId = StudentId,

                };
                context.announcements.Add(save_unpaid);
                await context.SaveChangesAsync();
            }

            else if (request.Status == InvoiceStatus.Paid)
            {
                var Response = new AutoResponsetDto();
                Response.Message = "Welcome, everyone! It’s great to have you all here. Each new term brings " +
                    "fresh opportunities to learn and connect," +
                    " and I’m excited to see what we’ll accomplish together. Stay open, " +
                    "stay curious, and let’s make this a great start.";
                Response.Type = AnnouncementType.System;
                Response.DateCreated = DateTime.UtcNow;
                Response.CourseCode = request.CourseCode;
                Response.StudentId = StudentId;
                var save = new InstructorAnnouncement
                {
                    AdminId = course.AdminId,
                    Message = Response.Message,
                    DateCreated = Response.DateCreated,
                    Type = AnnouncementType.System,
                    CourseId = course.Id,
                    InformationType = InformationType.System,
                    CourseCode = course.CourseCode,
                    CourseName = course.Title,
                    Title = "Welcome Message",
                    StudentId = StudentId,
                };
                context.announcements.Add(save);
                await context.SaveChangesAsync();             
            }
            return true;

        }
        public async Task<AnnouncementDto?> AddAnnouncementAsync(CreateAnnouncementDto announcement)
        {

            var course = await context.course.FirstOrDefaultAsync(s => s.CourseCode == announcement.CourseCode && s.AdminId == announcement.AdminId);
            if (course is null) return null;


            var entity = new InstructorAnnouncement
            {
                CourseName = course.Title,
                AdminId = course.AdminId,
                CourseId = course.Id,
                Title = announcement.Title,
                Message = announcement.Message,
                Type = AnnouncementType.instructor,
                InformationType = announcement.InformationType,
                DateCreated = DateTime.UtcNow,
                CourseCode = course.CourseCode,
                StudentId = null
            };

            context.announcements.Add(entity);
            await context.SaveChangesAsync();


            var response = new AnnouncementDto
            {
                CourseName = entity.CourseName,
                CourseCode = course.CourseCode,
                Title = announcement.Title,
                Message = announcement.Message,
                InformationType = announcement.InformationType,
                Type = AnnouncementType.instructor,
                DateCreated = entity.DateCreated,


            };

            return response;
        }

        public async Task<List<AnnouncementDto>?> DisplayAnnouncementAsync(Guid AdminId)
        {
            var request = await context.announcements
                .Include(s => s.course)
                .Where(s => s.Type == AnnouncementType.instructor && s.course.AdminId == AdminId)
              .Select(a => new AnnouncementDto
              {
                  CourseName = a.CourseName,
                  AnnouncementId = a.Id,
                  Title = a.Title,
                  Message = a.Message,
                  InformationType = a.InformationType,
                  CourseCode = a.course.CourseCode,
                  DateCreated = a.DateCreated
              }).ToListAsync();
            return request;
        }

        public async Task<bool> DeleteAnnouncementAsync(Guid AdminId, Guid AnnouncementId)
        {
            var request = await context.announcements.Where(s => s.course.AdminId == AdminId && s.Id == AnnouncementId).FirstOrDefaultAsync();
            if (request is null)
            {
                return false;
            }
            context.announcements.Remove(request);
            await context.SaveChangesAsync();
            return true;

        }
        public async Task<AnnouncementDto?> EditAnnouncementAsync(EditAnnouncementDto announcement)
        {
            var request = await context.announcements
                .Include(s => s.course)
                .FirstOrDefaultAsync(s => s.course.AdminId == announcement.AdminId && s.Id == announcement.AnnouncementId);
            if (request is null)
            {
                return null;
            }
            request.Title = announcement.Title;
            request.Message = announcement.Message;
            request.InformationType = announcement.InformationType;
            await context.SaveChangesAsync();

            var filter = new AnnouncementDto
            {
                AnnouncementId = request.Id,
                Title = request.Title,
                Message = request.Message,
                InformationType = request.InformationType,
                CourseName = request.course.Title,
                CourseCode = request.course.CourseCode,
                DateCreated = request.DateCreated
            };
            return filter;

        }
        public async Task<bool> RemoveInvoiceAsync(Guid StudentId, Guid CourseId)
        {
            var request = await context.invoice.FirstOrDefaultAsync(s => s.StudentId == StudentId && s.CourseId == CourseId);
            if(request is null)
            {
                return false;
            }
            var announcements = await context.announcements.Where(s => s.StudentId == StudentId).ToListAsync();
            foreach (var announcement in announcements)
            {
                context.announcements.Remove(announcement);
            }
            context.invoice.Remove(request);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task CheckUnpaidInvoicesAsync()
        {
            var datenow = DateTime.UtcNow;
            var expiredInvoices = await context.invoice
                .Where(s => s.Status == InvoiceStatus.Unpaid &&
                            s.PaymentDeadline < datenow )
                .ToListAsync();

            foreach (var invoice in expiredInvoices)
            {
                var enrollment = await context.enrollcourse
                    .Include(s => s.Course)
                    .FirstOrDefaultAsync(s => s.StudentId == invoice.StudentId && s.CourseId == invoice.CourseId);

                if (enrollment is not null)
                {
                   
                    if (enrollment.Course != null)
                    {
                        enrollment.Course.TotalEnrolled = Math.Max(0, enrollment.Course.TotalEnrolled - 1);
                    }
                    context.enrollcourse.Remove(enrollment);
                 await  RemoveInvoiceAsync(invoice.StudentId, invoice.CourseId);
                }

              
            
            }

            await context.SaveChangesAsync();
        }



    }


}
