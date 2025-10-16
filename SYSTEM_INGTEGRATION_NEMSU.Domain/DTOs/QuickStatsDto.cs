using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs
{
  public class QuickStatsDto
    {
        public int MaxCapacity { get; set; }
        public int TotalEnrolled { get; set; }
        public int AvailableSlots { get; set; }
    }
}
