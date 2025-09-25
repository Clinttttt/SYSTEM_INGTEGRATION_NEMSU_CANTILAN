using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.External
{
   public interface IAuthApiServices
    {
        Task<User?> RegisterAsync(UserDtos reqest);
        Task<TokenResponseDto?> LoginAsync(LoginDto reqest);
        Task<bool> LogoutAsync(string Accesstoken);
        Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenDto reqest);
        Task<bool> TryRefreshTokenAsync();
        Task<TokenResponseDto?> LoginWithGoogle(string googleid, string email, string fullname);
    }
}
