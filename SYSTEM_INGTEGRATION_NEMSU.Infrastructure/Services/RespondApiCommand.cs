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
        private readonly IAuthHelper _IAuthHelper;

        public RespondApiCommand(HttpClient http, IAuthHelper authHelper)
        {
            _http = http;
            _IAuthHelper = authHelper;

        }
        public async Task<AutoResponsetDto?> AutoResponse(string CourseCode)
        {
            await _IAuthHelper.SetAuthHeaderAsync(_http);
            var response = await _http.PostAsJsonAsync("api/HandlingMessage/Auto%20Response", CourseCode);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<AutoResponsetDto>();
        }
        public async Task<AnnouncementDto?> Announcement(AnnouncementDto announcement)
        {
            await _IAuthHelper.SetAuthHeaderAsync(_http);
            var response = await _http.PostAsJsonAsync("api/HandlingMessage/Announcement", announcement);

            if (!response.IsSuccessStatusCode)
                return null;

           
            if (response.Content.Headers.ContentLength > 0)
            {
                return await response.Content.ReadFromJsonAsync<AnnouncementDto>();
            }        
            return announcement;
        }
        public async Task<List<AnnouncementDto>?> DisplayAnnouncementAsync()
        {
            await _IAuthHelper.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<List<AnnouncementDto>>("api/HandlingMessage/Display%20Announcement");
        }
        public async Task<AnnouncementDto?> EditAnnouncementAsync(EditAnnouncementDto announcement)
        {
            await _IAuthHelper.SetAuthHeaderAsync(_http);
            var request = await _http.PatchAsJsonAsync("api/HandlingMessage/Edit%20Announcement", announcement);
            if (!request.IsSuccessStatusCode) return null;
            return await request.Content.ReadFromJsonAsync<AnnouncementDto>();
        }
        public async Task<bool> DeleteAnnouncementAsync(Guid AnnouncementId)
        {
            await _IAuthHelper.SetAuthHeaderAsync(_http);
            return await _http.DeleteFromJsonAsync<bool>($"api/HandlingMessage/Delete%20Announcement/{AnnouncementId}");
        }
    }




   
}

