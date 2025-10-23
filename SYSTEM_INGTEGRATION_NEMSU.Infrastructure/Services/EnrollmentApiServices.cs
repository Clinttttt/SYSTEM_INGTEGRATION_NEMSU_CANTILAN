using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services
{
   public class EnrollmentApiServices : IEnrollmentApiServices
    {
        private readonly HttpClient _http;
        private readonly ProtectedLocalStorage _localstorage;

        public EnrollmentApiServices(HttpClient HttpClient, ProtectedLocalStorage localstorage)
        {
            _http = HttpClient;
            _localstorage = localstorage;
        }

        public async Task SetAuthHeaderAsync()
        {
            var token = await _localstorage.GetAsync<string>("AccessToken");
            var results = token.Success ? token.Value : null;
            if(!string.IsNullOrEmpty(results))                  
                _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", results);
        }
        public async Task<Invoice?> EnrollCourseAsync(string CourseId, double Payment)
        {
            await SetAuthHeaderAsync();
            var payload = new { CourseId, Payment };
            var response = await _http.PostAsJsonAsync("api/EnrollmentHandling/Enroll%20Course", payload);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<Invoice>();
        }
       
        public async Task<IEnumerable<CourseDto>?> DisplayCourseAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<IEnumerable<CourseDto>>("api/EnrollmentHandling/Display%20Course");           
        }
        public async Task<bool> UnenrollCourse(string CourseId)
        {
            await SetAuthHeaderAsync();
            return await _http.DeleteFromJsonAsync<bool>($"api/EnrollmentHandling/UnEnroll%20Course/{CourseId}");     
        }
        public async Task<CourseDto?> PreviewCourseAsync(Guid CourseId)
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<CourseDto>($"api/EnrollmentHandling/Display%20PreviewCourse?CourseId={CourseId}");
        }





    }   
}
