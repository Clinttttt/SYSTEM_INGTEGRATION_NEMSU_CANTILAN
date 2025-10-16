using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services
{
   public class FacultyRecordApi : IFacultyRecordApi
    {
        private readonly HttpClient _http;
        private ProtectedLocalStorage _storage;
        public FacultyRecordApi(HttpClient http, ProtectedLocalStorage storage)
        {
            _http = http;
            _storage = storage;
        }
        public async Task SetAuthentication()
        {
            var token = await _storage.GetAsync<string>("AccessToken");
            var results = token.Success ? token.Value : null;
            if (!string.IsNullOrEmpty(results))
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", results);
  
        }
        public async Task<FacultyRecordDto?> AddFacultyInformationAsync(FacultyRecordDto details)
        {
            await SetAuthentication();
            var request = await _http.PostAsJsonAsync("api/FacultyRecord/Add%20Faculty%20Information", details);
            if (!request.IsSuccessStatusCode) return null;
            return await request.Content.ReadFromJsonAsync<FacultyRecordDto>();
        }
        public async Task<FacultyRecordDto?> UpdateFacultyInformationAsync(FacultyRecordDto details)
        {
            await SetAuthentication();
            var request = await _http.PatchAsJsonAsync("api/FacultyRecord/Update%20Faculty%20Information", details);
            if (!request.IsSuccessStatusCode) return null;
            return await request.Content.ReadFromJsonAsync<FacultyRecordDto>();
        }
        public async Task<FacultyRecordDto?> DisplayFacultyDetailsAsync()
        {
            await SetAuthentication();
            return await _http.GetFromJsonAsync<FacultyRecordDto>("api/FacultyRecord/Display%20Faculty%20Information");
        }

    }
}
