using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services
{
    public class HandlingStudentsApi : IHandlingStudentsApi
    {
        private readonly HttpClient _http;

        private readonly IAuthHelper _authHelper;
        public HandlingStudentsApi(HttpClient http, IAuthHelper authHelper)
        {
            _http = http;
            _authHelper = authHelper;
        }

        public async Task<List<HandlingStudentsDto>?> DisplayStudentsAsync()
        {

            await _authHelper.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<List<HandlingStudentsDto>>("api/HandlingStudents/Display%20Students");
        }
        public async Task<List<HandlingStudentsDto>?> DisplayStudentByCoursesAsync(string CourseCode)
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<List<HandlingStudentsDto>>($"api/HandlingStudents/Display%20Students%20ByCourses?CourseCode={CourseCode}");
        }
        public async Task<HandlingAllStudentsDetailsDto?> DisplayAllStudentsDetailsAsync(Guid StudentId)
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<HandlingAllStudentsDetailsDto>($"api/HandlingStudents/GetAll%20StudentDetails?StudentId={StudentId}");

        }
        public async Task<List<HandlingStudentsDto>?> DisplayStudentByDepartmentAsync(CourseDepartment department)
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<List<HandlingStudentsDto>>($"api/HandlingStudents/Display%20Students%20ByDepartment?department={department}");
        }
        public async Task<SummaryStatisticsDto?> SummaryStatisticsAsync()
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<SummaryStatisticsDto>("api/HandlingStudents/Summary%20Statistics");
        }
        public async Task<List<DepartmentStatsDto>?> DepartmentStatsAsync()
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<List<DepartmentStatsDto>>("api/HandlingStudents/Department%20Statistics");
        }

        public async Task<StudentsByYearLevelResponse?> StudentByYearLevelAsync(
        CourseProgram choice,
        YearLevelChoice yearLevel,
        int pageNumber = 1,
        int pageSize = 10,
        string searchQuery = "")
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            var url = $"api/HandlingStudents/Display%20StudentByYearLevel?choice={choice}&yearLevel={yearLevel}&pageNumber={pageNumber}&pageSize={pageSize}&searchQuery={Uri.EscapeDataString(searchQuery)}";

            return await _http.GetFromJsonAsync<StudentsByYearLevelResponse>(url);
        }
        public async Task<FacultyPhotoId?> StudentPhotoIDAsync(Guid StudentId)
        {
            return await _http.GetFromJsonAsync<FacultyPhotoId>($"api/HandlingStudents/Get%20StudentId/{StudentId}");
        }
        public async Task<List<StudendBillRecordDtoDto>?> StudentRecordAsync()
        {
            await _authHelper.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<List<StudendBillRecordDtoDto>>("api/HandlingStudents/Student%20BillRecord");
            
        }
    }
}
