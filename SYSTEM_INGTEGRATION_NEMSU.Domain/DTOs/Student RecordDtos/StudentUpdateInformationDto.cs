using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDto;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos
{
    public class StudentUpdateInformationDto
    {
     
        public Guid StudentId { get; set; }
        public PersonalInformationDto? personalInformation { get; set; }
        public AcademicInformationDto? academicInformation { get; set; }
        public ContactInformationDto? contactInformation { get; set; }

    }
}
