using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Faculty_Record
{
    public class FacultyPersonalInformation
    {

        public Guid Id { get; set; }
        public Guid FacultyId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public FacultyGender? FacultyGender { get; set; }
        public DateOnly DateofBirth { get; set; }
        public string? ContactNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
        public FacultySaveStatus facultySaveStatus { get; set; }
       public EmploymentStatus? employmentStatus { get; set; }
       public byte[]? Photo { get; set; }
       public string? PhotoContentType { get; set; }
    }
    public enum FacultyGender 
    {
        Male,
        Female,
        Prefer_not_to_say
    }
    public enum FacultySaveStatus
    {
        Save_As_Draft,
        Save
    }
   public enum EmploymentStatus
    {
        Active,
        Inactive
    }
}
