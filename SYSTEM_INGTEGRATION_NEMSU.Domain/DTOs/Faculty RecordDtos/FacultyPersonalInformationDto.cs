using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Faculty_Record;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos
{
    public class FacultyPersonalInformationDto
    {
        public Guid FacultyId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public FacultyGender FacultyGender { get; set; }
        public DateOnly DateofBirth { get; set; }
        public string? ContactNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
    }
}
