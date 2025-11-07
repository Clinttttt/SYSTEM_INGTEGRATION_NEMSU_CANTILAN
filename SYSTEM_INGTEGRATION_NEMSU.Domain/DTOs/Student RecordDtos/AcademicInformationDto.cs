using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos
{
    public class AcademicInformationDto
    {
        public Guid StudentId { get; set; }
        public string? StudentSchoolId { get; set; }
        [Required(ErrorMessage = "Student Type is required")]
        [EnumDataType(typeof(StudentTypeChoice), ErrorMessage = "Please select a valid Student Type")]
        public StudentTypeChoice? StudentType { get; set; }
        [Required(ErrorMessage = " Year Level Required")]
        [EnumDataType(typeof(YearLevelChoice), ErrorMessage = "Please Select a valid Year Level")]
        public YearLevelChoice? YearLevel { get; set; }
        [Required(ErrorMessage = "Semester Required")]
        [EnumDataType(typeof(SemesterChoice), ErrorMessage = "Please Select a valid Semester")]
        public SemesterChoice? Semester { get; set; }
        [Required(ErrorMessage = "Program Required")]
        [EnumDataType(typeof(CourseProgram), ErrorMessage = "Please Select a valid Semester")]
        public CourseProgram? Program { get; set; }
   

        [Required(ErrorMessage = "Strand Required")]
        [EnumDataType(typeof(StrandChoice), ErrorMessage = "Please Select a valid Strand")]
        public StrandChoice? Strand { get; set; }      
        public SaveStatusAcademic Savestatus { get; set; }
      
    }
}
