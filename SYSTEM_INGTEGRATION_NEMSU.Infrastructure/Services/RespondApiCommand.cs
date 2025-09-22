using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services
{
    public class RespondApiCommand : IRespondApiCommand
    {
        private readonly HttpClient _http;
        private readonly ProtectedLocalStorage _localstorage;

        public RespondApiCommand(IHttpClientFactory httpClient, ProtectedLocalStorage localstorage)
        {
            _http = httpClient.CreateClient();
            _localstorage = localstorage;
        }

        public async Task SetAuthHeaderAsync()
        {
            var token = await _localstorage.GetAsync<string>("AccessToken");
            var results = token.Success ? token.Value : null;
            if (!string.IsNullOrEmpty(results))
                _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", results);
        }
        public async Task<AutoResponsetDto?> AutoResponse(string CourseCode)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/HandlingMessage/AutoResponse", CourseCode);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<AutoResponsetDto>();
        }
        public async Task<AnnouncementDto?> Announcement(string CourseCode, string Message)
        {
            await SetAuthHeaderAsync();
            var payload = new { CourseCode, Message };
            var response = await _http.PostAsJsonAsync("api/HandlingMessage/Announcement", payload);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<AnnouncementDto>();
        }
    }




   
}

