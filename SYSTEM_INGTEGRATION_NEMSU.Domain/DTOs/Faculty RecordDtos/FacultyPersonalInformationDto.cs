using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Faculty_Record;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos
{
    public class FacultyPersonalInformationDto
    {
        public Guid FacultyId { get; set; }
        [Required(ErrorMessage = "First Name Required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Middle Name Required")]
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name Required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = " Faculty Gender Required")]
        [EnumDataType(typeof(FacultyGender), ErrorMessage = "Please Select a valid Gender")]
        public FacultyGender? FacultyGender { get; set; }
       
        public DateOnly DateofBirth { get; set; }
        [Required(ErrorMessage = "Contact Number Required")]
        public string? ContactNumber { get; set; }
        [Required(ErrorMessage = "Email Address Required")]
        public string? EmailAddress { get; set; }
        [Required(ErrorMessage = "Address Required")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Photo Required")]
        public byte[]? Photo { get; set; }
        public string? PhotoContentType { get; set; }
        [EnumDataType(typeof(EmploymentStatus), ErrorMessage = "Please select a valid Employment Status")]
        public EmploymentStatus? EmploymentStatus { get; set; }

    }
}
