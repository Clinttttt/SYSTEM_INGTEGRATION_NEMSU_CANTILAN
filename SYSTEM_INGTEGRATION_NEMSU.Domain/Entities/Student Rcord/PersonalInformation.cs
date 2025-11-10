using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities
{
  public  class PersonalInformation
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public GenderChoice Gender { get; set; }
        public CivilStatusChoice CivilStatus { get; set; }
        public string? Nationality { get; set; }
        public string? PermanentAddress { get; set; }
        public string? GuardianName { get; set; }
        public string? GuardianContact { get; set; }
        public SaveStatus Savestatus { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public byte[]? Photo { get; set; }
        public string? PhotoContentType { get; set; }
    }
    public enum GenderChoice
    {
        [Display(Name = "Male")]
        Male,
        [Display(Name = "Female")]
        Female,
        [Display(Name = "Prefer not to say")]
        Prefer_not_to_say
    }
    public enum CivilStatusChoice
    {
        [Display(Name = "Single")]
        Single,
        [Display(Name = "Taken")]
        Taken,
        [Display(Name = "Married")]
        Married
    }
    public enum SaveStatus
    {
        Save_As_Draft,
        Save
    }
}
