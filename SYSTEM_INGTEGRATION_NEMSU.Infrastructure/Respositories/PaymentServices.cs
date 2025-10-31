using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;

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
            if (course.Cost > paymentdetails.cost) return null;
           
          
            var request  = await enrollmentServices.EnrollCourseAsync(paymentdetails.StudentId, paymentdetails.CourseCode!, EnrollmentStatus.Enrolled);
            var purchase = await enrollmentServices.AddPaymentAsync(paymentdetails); 
            if (request is null) return null;
            if (purchase is null) return null;

           
                var invoice = new Invoice
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

            await enrollmentServices.EnrollCourseAsync(studentId, courseCode, EnrollmentStatus.Provisioned);
         
            var invoice = new Invoice
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
            var filter = new ProvisionDto
            {
                CourseCode = invoice.CourseCode,
                Status = invoice.Status,
                Standing = invoice.Standing,
               
            };
            context.invoice.Add(invoice);
            await context.SaveChangesAsync();
            await respondCommand.AutoResponseAsync(studentId, courseCode);
            return filter;
        }

    }
}
