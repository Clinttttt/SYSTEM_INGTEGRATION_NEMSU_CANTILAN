using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord
{
    public class AcademicInformation
    {

        public Guid Id { get; set; }
        public string? StudentSchoolId { get; set; }
        public Guid StudentId { get; set; }
        public StudentTypeChoice StudentType { get; set; }
        public YearLevelChoice YearLevel { get; set; }
        public SemesterChoice Semester { get; set; }
        public ProgramChoice Program { get; set; }
        public MajorChoice Major { get; set; }
        public StrandChoice Strand { get; set; }
        public SaveStatusAcademic Savestatus { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }
    public enum StudentTypeChoice
    {
        New,
        Transferee,
        Returning,
        Old,
    }
    public enum YearLevelChoice
    {
        First_Year,
        Second_Year,
        Third_Year,
        Fourth_Year

    }
    public enum SemesterChoice
    {
        First_Semester,
        Second_Semester
    }
    public enum ProgramChoice
    {
        Criminology,
        Hospitality_Management,
        Business_Administration,
        Tourism_Management,
        Industrial_Technology,
        Computer_Engineering,
        Computer_Science,
        Computer_Technology,
        Secondary_Education,
        Technology_AndLivelihood_Education,
        Technical_Vocational_TeacherEducation
    }
    public enum MajorChoice
    {
        None,
        Financial_Management,
        HumanResources_Management,
        Architectural_Drafting_Technology,
        Automotive_Technology,
        Electrical_Technology,
        Electronics_Technology,
        Food_Technology,
        Garments_Technology,
        Mechanical_Technology,
        Computer_Technology,
        English,
        Mathematics,
        Filipino,
        Science,
        Home_Economics,
        Foods_And_Services_Management,
        Fashion_And_Design,
        Garments
    }
    public enum StrandChoice
    {
        Stem,
        Abm,
        Humss,
        Gas,
        Tvl
    }
    public enum SaveStatusAcademic
    {
        Save_As_Draft,
        Save
    }
}
