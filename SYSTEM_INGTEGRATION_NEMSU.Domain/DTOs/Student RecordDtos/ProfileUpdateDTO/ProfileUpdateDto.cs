using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos.NewFolder
{
    public class ProfileUpdateDto
    {
        public Guid StudentId { get; set; }

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
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Middle Name is required")]
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Date of Birth is required")]
        public DateOnly DateOfBirth { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        [EnumDataType(typeof(GenderChoice), ErrorMessage = "Please select a valid gender")]
        public GenderChoice Gender { get; set; }
        [Required(ErrorMessage = "Civil Status is required")]
        [EnumDataType(typeof(CivilStatusChoice), ErrorMessage = "Please select a valid civil status")]
        public CivilStatusChoice CivilStatus { get; set; }
        [Required(ErrorMessage = "Nationality is required")]
        public string? Nationality { get; set; }
        [Required(ErrorMessage = "Permanent Address is required")]
        public string? PermanentAddress { get; set; }
        [Required(ErrorMessage = "Mobile Number Required")]
        public string? MobileNumber { get; set; }
        [Required(ErrorMessage = "Email Address Required")]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "Emergency Contact Required")]
        public string? EmergencyContactNumber { get; set; }
        public string? StudentSchoolId { get; set; }




    }
}
