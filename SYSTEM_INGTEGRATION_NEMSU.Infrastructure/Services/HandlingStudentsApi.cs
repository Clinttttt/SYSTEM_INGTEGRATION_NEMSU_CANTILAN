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
   public class HandlingStudentsApi : IHandlingStudentsApi
    {
        private readonly HttpClient _http;
        private ProtectedLocalStorage _storage;
        public HandlingStudentsApi(HttpClient http, ProtectedLocalStorage storage)
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
        public async Task<List<HandlingStudentsDto>?> DisplayStudentsAsync()
        {
       
            await SetAuthentication();
            return await _http.GetFromJsonAsync<List<HandlingStudentsDto>>("api/HandlingStudents/Display%20Students");
        }
        public async Task<List<HandlingStudentsDto>?> DisplayStudentByCoursesAsync(string CourseCode)
        {
            await SetAuthentication();
            return await _http.GetFromJsonAsync<List<HandlingStudentsDto>>($"api/HandlingStudents/Display%20Students%20ByCourses?CourseCode={CourseCode}");
        }
        public async Task<HandlingAllStudentsDetailsDto?> DisplayAllStudentsDetailsAsync(Guid StudentId)
        {
            await SetAuthentication();
            return await _http.GetFromJsonAsync<HandlingAllStudentsDetailsDto>($"api/HandlingStudents/GetAll%20StudentDetails?StudentId={StudentId}");

        }
        public async Task<List<HandlingStudentsDto>?> DisplayStudentByDepartmentAsync( CourseDepartment department)
        {
            await SetAuthentication();
            return await _http.GetFromJsonAsync<List<HandlingStudentsDto>>($"api/HandlingStudents/Display%20Students%20ByDepartment?department={department}");
        }


    }
}
