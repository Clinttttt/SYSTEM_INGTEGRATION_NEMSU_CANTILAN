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
        private readonly IAuthHelper _IAuthHelper;
        public FacultyRecordApi(HttpClient http, IAuthHelper IAuthHelper)
        {
            _http = http;
            _IAuthHelper = IAuthHelper;
        }
      
        public async Task<FacultyRecordDto?> AddFacultyInformationAsync(FacultyRecordDto details)
        {
            await _IAuthHelper.SetAuthHeaderAsync(_http);
            var request = await _http.PostAsJsonAsync("api/FacultyRecord/Add%20Faculty%20Information", details);
            if (!request.IsSuccessStatusCode) return null;
            return await request.Content.ReadFromJsonAsync<FacultyRecordDto>();
        }
        public async Task<FacultyRecordDto?> UpdateFacultyInformationAsync(FacultyRecordDto details)
        {
            await _IAuthHelper.SetAuthHeaderAsync(_http);
            var request = await _http.PatchAsJsonAsync("api/FacultyRecord/Update%20Faculty%20Information", details);
            if (!request.IsSuccessStatusCode) return null;
            return await request.Content.ReadFromJsonAsync<FacultyRecordDto>();
        }
        public async Task<FacultyRecordDto?> DisplayFacultyDetailsAsync()
        {
            await _IAuthHelper.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<FacultyRecordDto>("api/FacultyRecord/Display%20Faculty%20Information");
        }
        public async Task<FacultyPhotoId?> FacultyPhotoIDAsync()
        {
            await _IAuthHelper.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<FacultyPhotoId>("api/FacultyRecord/Faculty%20PhotoID");
        }
    }
}
