using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.Entities
{
  public class InstructorAnnouncement
    {
        public Guid Id { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? CourseId { get; set; }
        public AnnouncementType? Type { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public InformationType InformationType { get; set; }
        public Course_Subject Course { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public enum AnnouncementType
    {
        instructor,
        System,
    }
    public enum InformationType
    {
        Academic,
        Events,
        Important,
        General
    };
    public enum Course_Subject
    {
       
        GE101_Understanding_the_Self,
        GE102_Purposive_Communication,
        GE103_Readings_in_Philippine_History,
        GE104_The_Contemporary_World,
        GE105_Art_Appreciation,    
        GE107_Ethics,
        GE108_Rizals_Life_and_Works,
        GE109_People_And_Earth_Ecosystem,
        GE110_Science_Technology_Society,

        MATH101_College_Algebra,
        MATH102_Calculus_I,
        MATH103_Calculus_II,
        MATH104_Probability_and_Statistics,
        MATH104_Statistics_and_Probality,
        MATH105_Numerical_Analysis,

        IT101_Introduction_to_Computing,
        IT102_Fundamentals_of_Programming,
        IT103_Data_Structures,
        IT104_Discrete_Mathematics,
        IT201_Object_Oriented_Programming,
        IT202_Advance_Database,
        IT203_Object_Oriented_Programming,
        IT206_Networking1,
        IT207_Networking2,
        IT208_Intermediate_Programming,
        IT209_Event_Driven_Programming,
        IT210_Integrative_Programming,

        IT307_Systems_Analysis_and_Design,          
        IT404_Capstone_Project_II,           
        ELEC4_Internet_of_Things,        
        PE101_Physical_Education_1,
        PE102_Physical_Education_2,
        PE103_Physical_Education_3,     
    }

}
