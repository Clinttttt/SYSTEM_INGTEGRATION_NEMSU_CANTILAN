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
}
