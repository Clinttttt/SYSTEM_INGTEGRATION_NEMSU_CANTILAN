using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.External
{
   public interface IRespondApiCommand
    {
        Task<AutoResponsetDto?> AutoResponse(string CourseCode);
        Task<AnnouncementDto?> Announcement(AnnouncementDto announcement);
        Task<List<AnnouncementDto>?> DisplayAnnouncementAsync();
        Task<AnnouncementDto?> EditAnnouncementAsync(EditAnnouncementDto announcement);
        Task<bool> DeleteAnnouncementAsync(Guid AnnouncementId);
    }
}
