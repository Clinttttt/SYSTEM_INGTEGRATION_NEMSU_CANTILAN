using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string? StudentId { get; set; }
        public string? CourseCode { get; set; }
        public double Cost { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DatePaid { get; set; }
       public InvoiceStatus Status { get; set; }

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
