using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }

    
        public Guid StudentId { get; set; }
        public User Student { get; set; } = null!;

      
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;

       
        public string? CourseCode { get; set; } 
        public double Cost { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DatePaid { get; set; }

        public InvoiceStatus Status { get; set; }
        public string? Standing { get; set; }

    }
    public enum InvoiceStatus
    {
        Unpaid,
        Paid,
        Pending,
        Failed,
        Refunded
    }
}
