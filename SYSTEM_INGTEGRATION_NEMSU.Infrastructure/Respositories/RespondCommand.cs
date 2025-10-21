using Azure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDto;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories
{
    public class RespondCommand(ApplicationDbContext context) : IRespondCommand
    {

        public async Task<AutoResponsetDto?> AutoResponseAsync(Guid StudentId, string CourseCode)
        {
            var Response = new AutoResponsetDto();
            var request = await context.invoice.FirstOrDefaultAsync(s => s.CourseCode == CourseCode && s.StudentId == StudentId);
            if (request is null) return null;

            if (request.Status == InvoiceStatus.Unpaid)
            {
                Response.Message = "Your Course will Automaticaly Unenrolled if not paid After 15 Days";
                Response.Type = AnnouncementType.System;
                Response.DateCreated = DateTime.UtcNow;

                var deadline = DateTime.UtcNow.AddDays(15);
                var finduser = await context.enrollcourse.FirstOrDefaultAsync(s => s.StudentId == request.StudentId);
                if (finduser is null) return null;

                if (DateTime.UtcNow > deadline)
                {
                    context.enrollcourse.Remove(finduser);
                    await context.SaveChangesAsync();
                }
            }
            else if (request.Status == InvoiceStatus.Paid)
            {
                Response.Message = "Thank you for Enrolling in this Course";
                Response.Type = AnnouncementType.System;
                Response.DateCreated = DateTime.UtcNow;
            }

            Response.CourseCode = request.CourseCode;
            Response.StudentId = StudentId;

            var save = Response.Adapt<InstructorAnnouncement>();
            context.announcements.Add(save);
            await context.SaveChangesAsync();
            return Response;
        }
        public async Task<AnnouncementDto?> AddAnnouncementAsync(CreateAnnouncementDto announcement)
        {
          
            var course = await context.enrollcourse
                .Include(e => e.Course)
                .Where(e => e.Course.CourseCode == announcement.CourseCode
                         && e.Course.AdminId == announcement.AdminId)
                .Select(e => e.Course)
                .FirstOrDefaultAsync();

            if (course is null)
                return null; 

         
            var entity = new InstructorAnnouncement
            {
               
                CourseId = course.Id,           
                Title = announcement.Title,
                Message = announcement.Message,
                Type = AnnouncementType.instructor,
                InformationType = announcement.InformationType,
                DateCreated = DateTime.UtcNow,
                CourseCode = course.CourseCode
            };

            context.announcements.Add(entity);
            await context.SaveChangesAsync();

       
            var response = new AnnouncementDto
            {
                CourseName = course.Title,
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
                  Title = a.Title,
                  Message = a.Message,
                  InformationType = a.InformationType,
                  CourseName = a.course.Title,
                  CourseCode = a.course.CourseCode,
                  DateCreated = a.DateCreated
              }).ToListAsync();
            return request;
        }
                       
        public async Task<bool> DeleteAnnouncementAsync(Guid AdminId, Guid CourseId)
        {
            var request = await context.announcements.Where(s => s.course.AdminId == AdminId && s.CourseId == CourseId).FirstOrDefaultAsync();
            if(request is null)
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
                .Include(s=> s.course)
                .FirstOrDefaultAsync(s => s.course.AdminId == announcement.AdminId && s.Id == announcement.AnnouncementId);
           if(request is null)
            {
                return null;
            }
            request.Title = announcement.Title;
            request.Message = announcement.Message;
            request.InformationType = announcement.InformationType;
            await context.SaveChangesAsync();

            var filter = new AnnouncementDto
            {
                Title = request.Title,
                Message = request.Message,
                InformationType = request.InformationType,
                CourseName = request.course.Title,
                CourseCode = request.course.CourseCode,
                DateCreated = request.DateCreated
            };
            return filter;
            
        }
       

    }

   
}
