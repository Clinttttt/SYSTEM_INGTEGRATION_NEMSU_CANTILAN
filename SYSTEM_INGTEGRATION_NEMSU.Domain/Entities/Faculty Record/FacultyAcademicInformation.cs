using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public FacultyDepartment? FacultyDepartment { get; set; }
        public Position? Position { get; set; }
        public string? YearsOfTeaching { get; set; }

    }
    public enum FacultyDepartment
    {
        [Display(Name = "College of Criminal Justice Education")]
      CCJE,
        [Display(Name = "Department of Industrial Technology")]
        DIT,
        [Display(Name = "Department of Digital Teacher Training")]
        DGTT,
        [Display(Name = "Department of Business and Management")]
        DBM,
        [Display(Name = "Department of Computer Studies")]
        DCS,
    }
    public enum Position
    {
        [Display(Name = "Contractual")]
        Contractual,

        [Display(Name = "Instructor I")]
        Instructor_I,

        [Display(Name = "Instructor II")]
        Instructor_II,

        [Display(Name = "Instructor III")]
        Instructor_III,

        [Display(Name = "Assistant Professor I")]
        Assistant_Professor_I,

        [Display(Name = "Assistant Professor II")]
        Assistant_Professor_II,

        [Display(Name = "Assistant Professor III")]
        Assistant_Professor_III,

        [Display(Name = "Assistant Professor IV")]
        Assistant_Professor_IV,

        [Display(Name = "Associate Professor I")]
        Associate_Professor_I,

        [Display(Name = "Associate Professor II")]
        Associate_Professor_II,

        [Display(Name = "Associate Professor III")]
        Associate_Professor_III,

        [Display(Name = "Associate Professor IV")]
        Associate_Professor_IV,

        [Display(Name = "Associate Professor V")]
        Associate_Professor_V,

        [Display(Name = "Professor I")]
        Professor_I,

        [Display(Name = "Professor II")]
        Professor_II,

        [Display(Name = "Professor III")]
        Professor_III,

        [Display(Name = "Professor IV")]
        Professor_IV,

        [Display(Name = "Professor V")]
        Professor_V
    }

}
