﻿using Azure;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
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
        public async Task<AnnouncementDto?> AnnouncementAsync(AnnouncementDto announcement)
        {

            var request = await context.enrollcourse.FirstOrDefaultAsync(s => s.Course.CourseCode == announcement.CourseCode);
            if (request is null) return null;

            var Response = new AnnouncementDto()
            {

                CourseCode = announcement.CourseCode,
                Message = announcement.Message,
                DateCreated = DateTime.UtcNow,
                Type = AnnouncementType.instructor,
                StudentId = announcement.StudentId,
                CourseId = request.CourseId,
                Title = announcement.Title,
                Course = announcement.Course,
                InformationType = announcement.InformationType,

            };
            var save = Response.Adapt<InstructorAnnouncement>();
            context.announcements.Add(save);
            await context.SaveChangesAsync();
            return Response;
        }
      
    }
}
