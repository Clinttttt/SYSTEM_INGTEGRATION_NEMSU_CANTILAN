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
         public async Task<Invoice?> InvoiceAsync( string StudentId, Guid CourseId, double Payment)
          {
            var request = await context.course.FirstOrDefaultAsync(s => s.Id == CourseId);
            if(request is null)
            {
                return null;
            }
            if (Payment < request.Cost)
            {
                return null;
            }
          
            await enrollment.EnrollCourseAsync(StudentId, CourseId); 
            
            if(await context.invoice.AnyAsync(s => s.StudentId == StudentId && s.CourseId == CourseId))
            {
                return null;
            }          
            var invoice_details = new Invoice()
            {
                Id = Guid.NewGuid(),
                Cost = request.Cost,
                StudentId = StudentId,
                CourseId = CourseId,
                DateCreated = DateTime.UtcNow,
                Status = Payment >= request.Cost ? InvoiceStatus.Paid : InvoiceStatus.Unpaid,
                DatePaid = Payment >= request.Cost ? DateTime.UtcNow : null
            };
       
            context.invoice.Add(invoice_details);
            await context.SaveChangesAsync();
  
            return invoice_details;
          }

    }
}
