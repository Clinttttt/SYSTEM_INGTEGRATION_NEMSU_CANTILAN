using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos
{
  public  class StudentMiniInfoDto
    {
        public Guid StudentId { get; set; }
        [Required(ErrorMessage = "Photo Required")]
        public byte[]? Photo { get; set; }
        public string? PhotoContentType { get; set; }
        [Required(ErrorMessage = "Student ID is required")]
        public string StudentSchoolId { get; set; } = string.Empty;
    }
}
