using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos
{
  public class HandlingAllStudentsDetailsDto
    {
        public Guid StudentId { get; set; }
      public string? FullName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public GenderChoice? Gender { get; set; }
        public CivilStatusChoice? CivilStatus { get; set; }
        public string? Nationality { get; set; }
        public string? PermanentAddress { get; set; }      
        public string? StudentSchoolId { get; set; }
        public StudentTypeChoice? StudentType { get; set; }
        public YearLevelChoice? YearLevel { get; set; }
        public SemesterChoice? Semester { get; set; }
        public CourseProgram? Program { get; set; }

        public StrandChoice? Strand { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public DateTime DateEnrolled { get; set; }
        public StudentCourseStatus? studentCourseStatus { get; set; }
        public string? ProfileColor { get; set; }

    }
}
