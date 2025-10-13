using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Faculty_Record
{
    public class FacultyAcademicInformation
    {
        public Guid Id { get; set; }
        public string? FacultySchoolId { get; set; }
        public Guid FacultyId { get; set; }
        public FacultyDepartment FacultyDepartment { get; set; }
        public Position Position { get; set; }
        public string? YearsOfTeaching { get; set; }

    }
    public enum FacultyDepartment
    {
        BSHM,
        DIT,
        DGTT,
        DBM,
        DCS,
    }
    public enum Position
    {
        Contractual,
        Instructor_I,
        Instructor_II,
        Instructor_III,
        Assistant_Professor_I,
        Assistant_Professor_II,
        Assistant_Professor_III,
        Assistant_Professor_IV,
        Associate_Professor_I,
        Associate_Professor_II,
        Associate_Professor_III,
        Associate_Professor_IV,
        Associate_Professor_V,
        Professor_I,
        Professor_II,
        Professor_III,
        Professor_IV,
        Professor_V,
    }
}
