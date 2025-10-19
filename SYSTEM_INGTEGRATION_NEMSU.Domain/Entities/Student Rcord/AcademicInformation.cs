using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "New Student")]
        New,
        [Display(Name = "Transferee Student")]
        Transferee,
        [Display(Name = "Return Student")]
        Returning,
        [Display(Name = "Old Student")]
        Old,
    }
    public enum YearLevelChoice
    {
        [Display(Name = "1st Year")]
        First_Year,
        [Display(Name = "2nd Year")]
        Second_Year,
        [Display(Name = "3rd Year")]
        Third_Year,
        [Display(Name = "4th Year")]
        Fourth_Year

    }
    public enum SemesterChoice
    {
        [Display(Name = "First Semester")]
        First_Semester,
        [Display(Name = "Second Semester")]
        Second_Semester
    }
    public enum ProgramChoice
    {
        [Display(Name = "College of Criminal Justice Education")]
        Criminology,
        [Display(Name = "Bachelor of Science in Hospitality Management")]
        Hospitality_Management,
        [Display(Name = "Bachelor of Science in Business Administration")]
        Business_Administration,
        [Display(Name = "Bachelor of Science in Tourism Management")]
        Tourism_Management,
        [Display(Name = "Bachelor of Science in Industrial Technology")]
        Industrial_Technology,
        [Display(Name = "Bachelor of Science in Computer Engineering")]
        Computer_Engineering,
        [Display(Name = "Bachelor of Science in Computer Science")]
        Computer_Science,
        [Display(Name = "Bachelor of Science in Computer Technology")]
        Computer_Technology,
        [Display(Name = "Bachelor of Science in Secondary Education")]
        Secondary_Education,
        [Display(Name = "Bachelor of Science in Technology And Livelihood Education")]
        Technology_AndLivelihood_Education,
        [Display(Name = "Bachelor of Science in Technical Vocational Teacher Education")]
        Technical_Vocational_TeacherEducation
    }
    public enum MajorChoice
    {
        [Display(Name = "None")]
        None,

        [Display(Name = "Financial Management")]
        Financial_Management,

        [Display(Name = "Human Resources Management")]
        HumanResources_Management,

        [Display(Name = "Architectural Drafting Technology")]
        Architectural_Drafting_Technology,

        [Display(Name = "Automotive Technology")]
        Automotive_Technology,

        [Display(Name = "Electrical Technology")]
        Electrical_Technology,

        [Display(Name = "Electronics Technology")]
        Electronics_Technology,

        [Display(Name = "Food Technology")]
        Food_Technology,

        [Display(Name = "Garments Technology")]
        Garments_Technology,

        [Display(Name = "Mechanical Technology")]
        Mechanical_Technology,

        [Display(Name = "Computer Technology")]
        Computer_Technology,

        [Display(Name = "English")]
        English,

        [Display(Name = "Mathematics")]
        Mathematics,

        [Display(Name = "Filipino")]
        Filipino,

        [Display(Name = "Science")]
        Science,

        [Display(Name = "Home Economics")]
        Home_Economics,

        [Display(Name = "Foods and Services Management")]
        Foods_And_Services_Management,

        [Display(Name = "Fashion and Design")]
        Fashion_And_Design,

        [Display(Name = "Garments")]
        Garments
    }
    public enum StrandChoice
    {
        [Display(Name = "STEM")]
        Stem,
        [Display(Name = "ABM")]
        Abm,
        [Display(Name = "HUMSS")]
        Humss,
        [Display(Name = "GAS")]
        Gas,
        [Display(Name = "TVL")]
        Tvl
    }
    public enum SaveStatusAcademic
    {
        Save_As_Draft,
        Save
    }
}
