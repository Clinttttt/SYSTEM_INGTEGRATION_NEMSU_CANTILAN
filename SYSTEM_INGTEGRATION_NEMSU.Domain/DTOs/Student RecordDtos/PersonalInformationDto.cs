using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDto
{
    public class PersonalInformationDto
    {
        public Guid StudentId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Middle Name is required")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }
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

        [Required(ErrorMessage = "Guardian/Parent Name is required")]
        public string? GuardianName { get; set; }

        [Required(ErrorMessage = "Guardian/Parent Contact is required")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "Contact must be 11 digits starting with 09")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Contact must be exactly 11 digits")]
        public string? GuardianContact { get; set; }

        public SaveStatus Savestatus { get; set; }
    }
}
