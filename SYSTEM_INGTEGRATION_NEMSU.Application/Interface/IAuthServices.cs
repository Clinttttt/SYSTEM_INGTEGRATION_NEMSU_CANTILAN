using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.Interface
{
   public interface IAuthServices
    {
         Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenDto request);
         Task<User?> RegisterAsync(UserDto request);
         Task<TokenResponseDto?> LoginAsync(UserDto request);
         Task<bool> LogoutAsync(Guid user);
    }
}
