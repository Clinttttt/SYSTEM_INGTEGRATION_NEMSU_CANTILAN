using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs
{
  public  class PaymentDetailsDto
    {
    
        public Guid StudentId { get; set; }
        public string? AccountNumber { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public Guid PaymentId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? CourseCode { get; set; }
        public double cost { get; set; }
        public Guid CourseId { get; set; }
        public Category? Category { get; set; }
    }
}
