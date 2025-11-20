using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Faculty_Record;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities
{
  public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime ExpiredRefreshToken { get; set; }
        public string? RefreshToken { get; set; }
        public UserRole Role { get; set; }  
        public string? Character { get; set; }

        public Guid? StudentsDetailsId { get; set; }
        [JsonIgnore]
        public PersonalInformation? StudentsDetails { get; set; }
        public Guid? StudentAcademicsId { get; set; }
        [JsonIgnore]
        public AcademicInformation? StudentAcademicDetails { get; set; }
        public Guid? StudentContactsId { get; set; }
        [JsonIgnore]
        public ContactInformation? StudentContactDetails { get; set; }
        [JsonIgnore]
        public FacultyPersonalInformation? FacultyPersonalInformation { get; set; }
    }
    public enum UserRole
    { 
        Student,
        Facilitator
    }
    
}
