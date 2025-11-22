using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Department Required")]
        [EnumDataType(typeof(FacultyDepartment), ErrorMessage = "Please select a valid Position")]
        public FacultyDepartment? FacultyDepartment { get; set; }
        [Required(ErrorMessage = "Position Required")]
        [EnumDataType(typeof(Position), ErrorMessage = "Please select a valid Position")]
        public Position? Position { get; set; }
        [Required(ErrorMessage = "Years or Teaching Required")]
        public int YearsOfTeaching { get; set; }
      
    }
}
