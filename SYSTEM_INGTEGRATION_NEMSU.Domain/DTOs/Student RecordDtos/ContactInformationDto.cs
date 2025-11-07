using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos
{
    public class ContactInformationDto
    {
        public Guid StudentId { get; set; }
        [Required(ErrorMessage = "Mobile Number Required")]
        public string? MobileNumber { get; set; }
        [Required(ErrorMessage = "Email Address Required")]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "Emergency Contact Required")]
        public string? EmergencyContactNumber { get; set; }
        public SaveStatusContact Savestatus { get; set; }
    }
}
