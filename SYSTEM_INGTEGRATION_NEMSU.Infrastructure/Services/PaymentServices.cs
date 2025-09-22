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

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services
{

  public class PaymentServices(ApplicationDbContext context, IEnrollmentServices enrollment) : IPaymentServices
    {
        public async Task<Invoice?> InvoiceAsync(Guid studentId, string courseCode, double payment)
        {
        
            var course = await context.course
                .FirstOrDefaultAsync(c => c.CourseCode == courseCode);
            if (course is null) return null;

           
            bool alreadyEnrolled = await context.enrollcourse
                .AnyAsync(e => e.StudentId == studentId && e.CourseId == course.Id);
            if (alreadyEnrolled) return null;

          
            await enrollment.EnrollCourseAsync(studentId, courseCode);

    
            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                CourseId = course.Id,
                CourseCode = course.CourseCode,   
                Cost = course.Cost,
                DateCreated = DateTime.UtcNow,
                Status = payment >= course.Cost ? InvoiceStatus.Paid : InvoiceStatus.Unpaid,
                DatePaid = payment >= course.Cost ? DateTime.UtcNow : null,
                Standing = payment >= course.Cost ? "Enrolled" : "Temporary enrollment"
            };

            context.invoice.Add(invoice);
            await context.SaveChangesAsync();

            return invoice;
        }

    }
}
