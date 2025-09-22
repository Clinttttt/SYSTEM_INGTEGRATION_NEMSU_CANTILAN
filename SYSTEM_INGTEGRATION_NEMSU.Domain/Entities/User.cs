using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string? FullName { get; set; }
        public string? Email { get; set; }

    }
    public enum UserRole
    { 
        Student,
        Facilitator
    }
    public class StudentProfile
    {
        public Guid Id { get; set; }
        public Guid StudentId_FK { get; set; }
        public string? StudentId { get; set; }
        public string? Course { get; set; }
        public int YearLevel { get; set; }

    }
    public class FacilitatorProfile
    {
        public Guid Id { get; set; }
        public Guid Faculty_FK { get; set; }
        public string? FacultyId { get; set; }
        public List<string> CoursesTaught { get; set; } = new();
    }
}
