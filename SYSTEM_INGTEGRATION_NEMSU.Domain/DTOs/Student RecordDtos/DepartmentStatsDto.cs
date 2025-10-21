using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos
{
    public class DepartmentStatsDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int Count { get; set; }
        public string Color { get; set; } = string.Empty;
        public string LightColor { get; set; } = string.Empty;
        public double Percentage { get; set; }
       
    }
}
