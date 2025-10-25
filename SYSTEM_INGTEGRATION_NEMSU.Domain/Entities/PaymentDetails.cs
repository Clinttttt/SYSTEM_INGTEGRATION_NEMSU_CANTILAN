using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities
{
    public class PaymentDetails
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string? AccountNumber { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? CourseCode { get; set; }
        public double Cost { get; set; }
    }
    public enum PaymentMethod
    {
        Gcash,
        Card,
        Bank
    }
}
