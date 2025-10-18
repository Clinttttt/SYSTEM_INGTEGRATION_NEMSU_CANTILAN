using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
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
        public PersonalInformation? StudentsDetails { get; set; }
        public Guid? StudentAcademicsId { get; set; }
        public AcademicInformation? StudentAcademicDetails { get; set; }
        public Guid? StudentContactsId { get; set; }
        public ContactInformation? StudentContactDetails { get; set; }
    }
    public enum UserRole
    { 
        Student,
        Facilitator
    }
    
}
