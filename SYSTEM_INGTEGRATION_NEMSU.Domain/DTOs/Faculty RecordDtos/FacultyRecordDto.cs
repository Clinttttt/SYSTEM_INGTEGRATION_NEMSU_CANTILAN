using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Faculty_Record;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos
{
    public class FacultyRecordDto
    {
        public Guid FacultyId { get; set; }
        public FacultyAcademicInformationDto? FacultyAcademicInformationDto { get; set; } = new();
        public FacultyPersonalInformationDto? FacultyPersonalInformationDto { get; set; } = new();
        public FacultySaveStatus facultySaveStatus { get; set; }

    }
}
