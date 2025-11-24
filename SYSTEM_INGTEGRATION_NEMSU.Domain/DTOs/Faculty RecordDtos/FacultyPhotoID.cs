using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos
{
   public class FacultyPhotoId
    {
        public Guid FacultyId { get; set; }
        public string? PhotoContentType { get; set; }   
        public byte[]? Photo { get; set; }
    }
}
