using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos
{
    public class SummaryStatisticsDto
    {
        public int TotalStudent { get; set; }
        public int TotalActive { get; set; }
        public int TotalInactive { get; set; }
        public int TotalDepartment { get; set; }
        public int TotalCourse { get; set; }
    }
    public class StudentStats
    {
        public Guid StudentId { get; set; }
        public int TotalEnrolled { get; set; }
        public int MaxCapacity { get; set; }
        public Guid CourseId {get;set;}
    }
    public class StudentsByYearLevelResponse
    {
        public List<HandlingStudentsDto> Students { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
