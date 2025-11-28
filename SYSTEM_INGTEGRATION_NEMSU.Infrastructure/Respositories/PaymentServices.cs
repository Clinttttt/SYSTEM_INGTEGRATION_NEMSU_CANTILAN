using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Migrations;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories
{

  public class PaymentServices(ApplicationDbContext context, IEnrollmentServices enrollmentServices, IRespondCommand respondCommand) : IPaymentServices
    {
        public async Task<PaymentDetailsDto?> InvoiceAsync( PaymentDetailsDto paymentdetails)
        {
        
            var course = await context.course
                .FirstOrDefaultAsync(c => c.CourseCode == paymentdetails.CourseCode);
            if (course is null) return null;
           
            bool alreadyEnrolled = await context.enrollcourse
                .AnyAsync(e => e.StudentId == paymentdetails.StudentId && e.CourseId == course.Id);
            if (alreadyEnrolled) return null;

            var invoice = new Domain.Entities.Invoice
            {
                Id = Guid.NewGuid(),
                StudentId = paymentdetails.StudentId,
                CourseId = course.Id,
                CourseCode = course.CourseCode,
                Cost = course.Cost,
                DateCreated = DateTime.UtcNow,
                Status = InvoiceStatus.Paid,
                DatePaid = DateTime.UtcNow,
                Standing = "Enrolled",
                PaymentDeadline = DateTime.MinValue,
            };

            context.invoice.Add(invoice);
            await context.SaveChangesAsync();
           
            var request  = await enrollmentServices.EnrollCourseAsync(invoice.Id, paymentdetails.StudentId, course.CourseCode!, EnrollmentStatus.Enrolled);
            if (request is null) return null;
            var purchase = await enrollmentServices.AddPaymentAsync(paymentdetails);
            if (purchase is null) return null;
            await enrollmentServices.CourseTrackAdd(paymentdetails.StudentId, request.CourseId, EnrollmentStatus.Enrolled);
      
            await respondCommand.AutoResponseAsync(request.StudentId, request.CourseCode!);
            return purchase;
        }
        public async Task<ProvisionDto?> ProvisionAsync(Guid studentId, string courseCode)
        {
            var course = await context.course
               .FirstOrDefaultAsync(c => c.CourseCode == courseCode);
            if (course is null) return null;


            bool alreadyEnrolled = await context.enrollcourse
                .AnyAsync(e => e.StudentId == studentId && e.CourseId == course.Id);
            if (alreadyEnrolled) return null;

            var invoice = new Domain.Entities.Invoice
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                CourseId = course.Id,
                CourseCode = course.CourseCode,
                Cost = 0,
                DateCreated = DateTime.UtcNow,
                Status = InvoiceStatus.Unpaid,
                DatePaid = DateTime.MinValue,
                Standing = "Temporary enrollment",
                PaymentDeadline = DateTime.UtcNow.AddDays(15)
            };
            context.invoice.Add(invoice);
            await context.SaveChangesAsync();


            var request = await enrollmentServices.EnrollCourseAsync(invoice.Id, studentId, courseCode, EnrollmentStatus.Provisioned);
            if (request is null) return null;
         
            await enrollmentServices.CourseTrackAdd(studentId, request.CourseId, EnrollmentStatus.Provisioned);
      

            var filter = new ProvisionDto
            {
                CourseCode = invoice.CourseCode,
                Status = invoice.Status,
                Standing = invoice.Standing,
               
            };
       
          

            await respondCommand.AutoResponseAsync(studentId, courseCode);
            await respondCommand.ProvisionAnnouncementAsync(studentId);
            return filter;
        }
        public async Task<bool> PayProvisionAsync(PaymentDetailsDto paymentDetails)
        {
            var invoice = await context.invoice.FirstOrDefaultAsync(s => s.StudentId == paymentDetails.StudentId && s.CourseId == paymentDetails.CourseId);
            if (invoice is null) return false;
            var enroll = await context.enrollcourse.FirstOrDefaultAsync(s => s.StudentId == paymentDetails.StudentId && s.CourseId == paymentDetails.CourseId);
            if (enroll is null) return false;
            var course = await context.course.FirstOrDefaultAsync(s => s.Id == paymentDetails.CourseId);
            if (course is null) return false;
            if (course.Cost > paymentDetails.cost) { return false; }
            invoice.Cost = course.Cost;
            invoice.Status = InvoiceStatus.Paid;
            invoice.DatePaid = DateTime.UtcNow;
            invoice.DateCreated = DateTime.UtcNow;
            invoice.Standing = "Enrolled";
            invoice.PaymentDeadline = DateTime.MinValue;
            enroll.EnrollmentStatus = EnrollmentStatus.Enrolled;

            var coursetracker = await context.courseTrackers.FirstOrDefaultAsync(s => s.StudentId == paymentDetails.StudentId && s.CourseId == paymentDetails.CourseId);
            if (coursetracker is null) return false;
            coursetracker.CourseTrack = Domain.Entities.Student_Rcord.CourseTrack.Course_Already_Paid;
            var announcements = await context.announcements.Where(s => s.StudentId == paymentDetails.StudentId && s.CourseId == paymentDetails.CourseId && (s.InformationType == InformationType.Warning || s.InformationType == InformationType.System)).ToListAsync();
          if(announcements is not null)
            {
                foreach (var a in announcements)
                {
                    context.announcements.Remove(a);
                }
            }
          
            var payment = new PaymentDetails
            {
                StudentId = paymentDetails.StudentId,
                AccountNumber = paymentDetails.AccountNumber,
                PurchaseDate = DateTime.UtcNow,
                CourseCode = course.CourseCode,
                Cost = course.Cost,
                CategoryId = course.CategoryId
            };
            context.paymentDetails.Add(payment);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
