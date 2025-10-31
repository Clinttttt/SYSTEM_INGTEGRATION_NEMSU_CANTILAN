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
        Task<bool?> AutoResponseAsync(Guid StudentId, string CourseCode);    
        Task<List<AnnouncementDto>?> DisplayAnnouncementAsync(Guid AdminId);
        Task<AnnouncementDto?> AddAnnouncementAsync(CreateAnnouncementDto announcement);
        Task<bool> DeleteAnnouncementAsync(Guid AdminId, Guid AnnouncementId);
        Task<AnnouncementDto?> EditAnnouncementAsync(EditAnnouncementDto announcement);
        Task CheckUnpaidInvoicesAsync();
    }
}
