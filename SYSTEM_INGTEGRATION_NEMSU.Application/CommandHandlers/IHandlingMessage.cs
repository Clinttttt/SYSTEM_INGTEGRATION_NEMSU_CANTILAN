using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.CommandHandlers
{
    public interface IHandlingMessage
    {
        Task<AutoResponsetDto?> AutoResponseCommand(Guid StudentId, string CourseCode);
        Task<AnnouncementDto?> AnnouncementCommand(AnnouncementDto announcement);


    }
}
