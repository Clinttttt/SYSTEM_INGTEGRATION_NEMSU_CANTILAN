using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos
{
    public class ContactInformationDto
    {
        public Guid StudentId { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public SaveStatusContact Savestatus { get; set; }
    }
}
