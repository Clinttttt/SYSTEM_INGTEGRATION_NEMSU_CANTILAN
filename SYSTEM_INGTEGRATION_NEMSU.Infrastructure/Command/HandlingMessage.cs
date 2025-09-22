using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.CommandHandlers;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Command
{
    public class HandlingMessage( IRespondCommand respondcommand) : IHandlingMessage
    {
       
        public async Task<AutoResponsetDto?> AutoResponseCommand(Guid Studentid, string CourseCode)
        {         
           return await respondcommand.AutoResponseAsync(Studentid, CourseCode!);
           
        }
        public async Task<AnnouncementDto?> AnnouncementCommand(Guid StudentId, string Message, string CourseCode)
         {
         
            return await respondcommand.AnnouncementAsync( StudentId, Message,CourseCode);
        }

    }
}
