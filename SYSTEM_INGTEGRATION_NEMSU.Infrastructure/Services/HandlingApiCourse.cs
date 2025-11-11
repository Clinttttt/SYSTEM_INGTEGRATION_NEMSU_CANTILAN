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
    
        private readonly IAuthHelper _authHelper;


        public HandlingApiCourse(HttpClient httpClient, IAuthHelper authHelper)
        {
            _http = httpClient;     
            _authHelper = authHelper;
        }
    
        public async Task<CourseDto?> AddCourse(CreateCourseDto course)
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            var response = await _http.PostAsJsonAsync("api/CourseHandling/Add%20Course", course);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<CourseDto>();
        }
        public async Task<IEnumerable<CourseDto>?> DisplayAllCourse()
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<IEnumerable<CourseDto>>($"api/CourseHandling/Display%20Course");
        }
        public async Task<CourseDto?> UpdateCourse(UpdateCourseDto course)
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            var response = await _http.PatchAsJsonAsync("api/CourseHandling/Update%20Course", course);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<CourseDto>();
        }
        public async Task<bool> DeleteCourse(Guid course)
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            return await _http.DeleteFromJsonAsync<bool>($"api/CourseHandling/Delete%20Course/{course}");
        }
        public async Task<CourseDto?> GetCourseAsync(Guid CourseId)
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<CourseDto>($"api/CourseHandling/GetCourse%20Admin/{CourseId}");
        }
        public async Task<QuickStatsDto?> DisplayStatsAsync( string CourseCode)
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<QuickStatsDto>($"api/CourseHandling/Quick%20Stats?CourseCode={CourseCode}");
        }
        public async Task<bool> ArchivedCourseAsync( string CourseCode)
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            var response = await _http.PatchAsync($"api/CourseHandling/Archive Course?CourseCode={CourseCode}", null);
            if (!response.IsSuccessStatusCode) return false;
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        public async Task<bool> ActiveCourseAsync(string CourseCode)
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            var response = await _http.PatchAsync($"api/CourseHandling/Active CourseAsync?CourseCode={CourseCode}", null);
            if (!response.IsSuccessStatusCode) return false;
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        public async Task<IEnumerable<CourseDto>?> DisplayArchiveCourseAsync()
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<IEnumerable<CourseDto>>("api/CourseHandling/Display%20ArchiveCourse");
        }


    }
}
