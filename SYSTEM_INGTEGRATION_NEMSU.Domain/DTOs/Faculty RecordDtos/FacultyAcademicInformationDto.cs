using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Faculty_Record;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos
{
    public class FacultyAcademicInformationDto
    {
        public string? FacultySchoolId { get; set; }
        public Guid FacultyId { get; set; }
        public FacultyDepartment FacultyDepartment { get; set; }
        public Position Position { get; set; }
        public string? YearsOfTeaching { get; set; }
    }
}
