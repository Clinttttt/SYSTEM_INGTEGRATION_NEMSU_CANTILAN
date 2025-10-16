using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services
{
   public  class HandlingApiCourse : IHandlingApiCourse
    {
        private readonly HttpClient _http;
        private readonly ProtectedLocalStorage _localstorage;

        public HandlingApiCourse(HttpClient httpClient, ProtectedLocalStorage localstorage)
        {
            _http = httpClient;
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
        public async Task<CourseDto?> AddCourse(CreateCourseDto course)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("api/CourseHandling/Add%20Course", course);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<CourseDto>();
        }
        public async Task<IEnumerable<CourseDto>?> DisplayAllCourse()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<IEnumerable<CourseDto>>($"api/CourseHandling/Display%20Course");
        }
        public async Task<CourseDto?> UpdateCourse(UpdateCourseDto course)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PatchAsJsonAsync("api/CourseHandling/Update%20Course", course);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<CourseDto>();
        }
        public async Task<bool> DeleteCourse(Guid course)
        {
            await SetAuthHeaderAsync();
            return await _http.DeleteFromJsonAsync<bool>($"api/CourseHandling/Delete%20Course/{course}");
        }
        public async Task<CourseDto?> GetCourseAsync(Guid CourseId)
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<CourseDto>($"api/CourseHandling/GetCourse%20Admin/{CourseId}");
        }
        public async Task<QuickStatsDto?> DisplayStatsAsync( string CourseCode)
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<QuickStatsDto>($"api/CourseHandling/Quick%20Stats?CourseCode={CourseCode}");
        }
        
    }
}
