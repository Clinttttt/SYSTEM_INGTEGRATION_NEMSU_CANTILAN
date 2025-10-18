using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos
{
    public class AcademicInformationDto
    {
        public Guid StudentId { get; set; }
        [JsonIgnore]
        public string? StudentSchoolId { get; set; }
        public StudentTypeChoice StudentType { get; set; }
        public YearLevelChoice YearLevel { get; set; }
        public SemesterChoice Semester { get; set; }
        public ProgramChoice Program { get; set; }
        public MajorChoice Major { get; set; }
        public StrandChoice Strand { get; set; }
        public SaveStatusAcademic Savestatus { get; set; }
    }
}
