using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.Interface
{
  public interface IRespondCommand
    {
        Task<AutoResponsetDto?> AutoResponseAsync(Guid StudentId, string CourseCode);
        Task<AnnouncementDto?> AnnouncementAsync(Guid StudentId, string Message, string CourseId);
    }
}
