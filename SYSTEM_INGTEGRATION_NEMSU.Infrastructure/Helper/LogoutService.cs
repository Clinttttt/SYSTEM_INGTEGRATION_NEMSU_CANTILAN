using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Helper
{
    public class LogoutService
    {
        private readonly ProtectedLocalStorage _localStorage;
        private readonly NavigationManager _navigation;
        private readonly IAuthApiServices _authApi;

        public LogoutService(
            ProtectedLocalStorage localStorage,
            NavigationManager navigation,
            IAuthApiServices authApi)
        {
            _localStorage = localStorage;
            _navigation = navigation;
            _authApi = authApi;
        }

        public async Task HandleLogoutAsync()
        {
            var tokenResult = await _localStorage.GetAsync<string>("AccessToken");

            if (tokenResult.Success && !string.IsNullOrEmpty(tokenResult.Value))
            {
                await _authApi.LogoutAsync(tokenResult.Value);
                await _localStorage.DeleteAsync("AccessToken");
                await _localStorage.DeleteAsync("RefreshToken");
            }

            _navigation.NavigateTo("/", true);
        }
    }
}
