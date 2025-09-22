using Azure.Core;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services
{
   public class AuthApiServices : IAuthApiServices
    {
        private readonly HttpClient _http;
        private readonly ProtectedLocalStorage _localstorage;
        public AuthApiServices(IHttpClientFactory httpClient, ProtectedLocalStorage localstorage)
        {
            _http = httpClient.CreateClient();
            _localstorage = localstorage;

        }
        public async Task<User?> RegisterAsync(UserDtos reqest)
        {
            var response = await _http.PostAsJsonAsync("api/AuthHandling/Register", reqest);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<User>();
        }
        public async Task<TokenResponseDto?> LoginAsync(LoginDto reqest)
        {
            var response = await _http.PostAsJsonAsync("api/AuthHandling/Login", reqest);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<TokenResponseDto>();
        }
        public async Task<bool> LogoutAsync(string Accesstoken)
        {
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers
                .AuthenticationHeaderValue("Bearer", Accesstoken);
            var response = await _http.PostAsync("api/AuthHandling/Logout", null);
            _http.DefaultRequestHeaders.Authorization = null;
            return response.IsSuccessStatusCode;
        }
        public async Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenDto reqest)
        {
            var response = await _http.PostAsJsonAsync("api/AuthHandling/Refreshtoken", reqest);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<TokenResponseDto>();
        }
        public async Task<bool> TryRefreshTokenAsync()
        {
            var RefreshToken = await _localstorage.GetAsync<string>("RefreshToken");
            var UserId = await _localstorage.GetAsync<string>("UserId");
            if (!RefreshToken.Success || !UserId.Success) return false;
            var request = new RefreshTokenDto()
            {
                UserId = Guid.Parse(UserId.Value!),
                RefreshToken = RefreshToken.Value!
            };
            var NewToken = await RefreshTokenAsync(request);
            if (NewToken is null) return false;
            await _localstorage.SetAsync("AccessToken", NewToken.AccessToken!);
            await _localstorage.SetAsync("RefreshToken", NewToken.RefreshToken!);
            return true;
        }


    }
}
