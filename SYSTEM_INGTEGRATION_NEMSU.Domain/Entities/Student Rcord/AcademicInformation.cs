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
        public StudentTypeChoice? StudentType { get; set; }
        public YearLevelChoice? YearLevel { get; set; }
        public SemesterChoice? Semester { get; set; }
        public CourseProgram? Program { get; set; }
        public StrandChoice? Strand { get; set; }
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

    public enum CourseProgram
    {
        // ===== DCS (Computer Studies) =====
        [Display(Name = "Bachelor of Science in Computer Engineering")]
        Computer_Engineering,

        [Display(Name = "Bachelor of Science in Computer Science")]
        Computer_Science,

        [Display(Name = "Bachelor of Science in Information Technology")]
        Information_Technology,

        // ===== DIT (Industrial Technology) =====
        [Display(Name = "BSIT Major in Architectural Drafting Technology")]
        BSIT_ArchitecturalDraftingTechnology,

        [Display(Name = "BS in Industrial Technology Major in Automotive Technology")]
        BSIT_AutomotiveTechnology,

        [Display(Name = "BS in Industrial Technology Major in Computer Technology")]
        BSIT_ComputerTechnology,

        [Display(Name = "BS in Industrial Technology Major in Electrical Technology")]
        BSIT_ElectricalTechnology,

        [Display(Name = "BS in Industrial Technology Major in Electronics Technology")]
        BSIT_ElectronicsTechnology,

        [Display(Name = "BS in Industrial Technology Major in Food Technology")]
        BSIT_FoodTechnology,

        [Display(Name = "BS in Industrial Technology Major in Garments Technology")]
        BSIT_GarmentsTechnology,

        [Display(Name = "BS in Industrial Technology Major in Mechanical Technology")]
        BSIT_MechanicalTechnology,

        // ===== DGTT - Bachelor of Secondary Education =====
        [Display(Name = "BSEd Major in Filipino")]
        BSEd_Filipino,

        [Display(Name = "BSEd Major in English")]
        BSEd_English,

        [Display(Name = "BSEd Major in Mathematics")]
        BSEd_Mathematics,

        [Display(Name = "BSEd Major in Science")]
        BSEd_Science,

        // ===== DGTT - Bachelor of Technical-Vocational Teacher Education =====
        [Display(Name = "BTVTEd Major in Automotive Technology")]
        BTVTEd_AutomotiveTechnology,

        [Display(Name = "BTVTEd Major in Garments and Fashion Design")]
        BTVTEd_GarmentsAndFashionDesign,

        [Display(Name = "BTVTEd Major in Electrical Technology")]
        BTVTEd_ElectricalTechnology,

        [Display(Name = "BTVTEd Major in Electronics Technology")]
        BTVTEd_ElectronicsTechnology,

        [Display(Name = "BTVTEd Major in Food and Services Management")]
        BTVTEd_FoodAndServicesManagement,

        [Display(Name = "BTVTEd Major in Mechanical Technology")]
        BTVTEd_MechanicalTechnology,

        // ===== DGTT - Bachelor of Technology and Livelihood Education =====
        [Display(Name = "BTLEd Major in Home Economics")]
        BTLEd_HomeEconomics,

        [Display(Name = "BTLEd Major in Industrial Arts")]
        BTLEd_IndustrialArts,

        // ===== CCJE =====
        [Display(Name = "Bachelor of Science in Criminology")]
        BS_Criminology,

        // ===== DBM =====
        [Display(Name = "Bachelor of Science in Hospitality Management")]
        BSHM_HospitalityManagement,

        [Display(Name = "Bachelor of Science in Tourism Management")]
        BSTM_TourismManagement,

        [Display(Name = "BSBA Major in Financial Management")]
        BSBA_FinancialManagement,

        [Display(Name = "BSBA Major in Human Resource Management")]
        BSBA_HumanResourceManagement
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
