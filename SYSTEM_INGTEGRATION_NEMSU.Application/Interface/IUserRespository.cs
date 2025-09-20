using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.Interface
{
   public interface IUserRespository
    {
        Task<User?> UserInfo(Guid UserId);
    }
}
