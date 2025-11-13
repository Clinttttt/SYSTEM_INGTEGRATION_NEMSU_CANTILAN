using Azure;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDto;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos.EnrollmentFormDto;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos.NewFolder;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services
{
    public class StudentRecordApiCommand : IStudentRecordApiCommand
    {
        private readonly HttpClient _http;
        private readonly IAuthHelper _authApi;

        public StudentRecordApiCommand(HttpClient http, IAuthHelper authApi)
        {
            _http = http;
            _authApi = authApi;

        }


        public async Task<PersonalInformation?> AddPersonalDetailsAsync(PersonalInformationDto personalInformation)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var results = await _http.PostAsJsonAsync("api/StudentRecord/Add%20Personal%20Details", personalInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<PersonalInformation>();
        }
        public async Task<PersonalInformation?> UpdatePersonalInformationAsync(PersonalInformationDto personalInformation)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var results = await _http.PatchAsJsonAsync("api/StudentRecord/Update%20Personal%20Details", personalInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<PersonalInformation>();
        }
        public async Task<PersonalInformationDto?> DisplayPersonalInformationAsync()
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<PersonalInformationDto>("api/StudentRecord/Display%20Personal%20Details");
        }
        public async Task<AcademicInformation?> AddAcademicDetailsAsync(AcademicInformationDto academicInformation)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var results = await _http.PostAsJsonAsync("api/StudentRecord/Add%20Academic%20Details", academicInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<AcademicInformation>();
        }
        public async Task<AcademicInformation?> UpdateAcademicInformationAsync(AcademicInformationDto academicInformation)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var results = await _http.PostAsJsonAsync("api/StudentRecord/Update%20Academic%20Details", academicInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<AcademicInformation>();
        }
        public async Task<AcademicInformationDto?> DisplayAcademicInformation()
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<AcademicInformationDto>("api/StudentRecord/Display%20Academic%20Details");
        }
        public async Task<ContactInformation?> AddContactInformationAsync(ContactInformationDto contactInformation)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var results = await _http.PostAsJsonAsync("api/StudentRecord/Add%20Contact%20Details", contactInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<ContactInformation>();
        }
        public async Task<ContactInformation?> UpdateContactInformationAsync(ContactInformationDto contactInformation)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var results = await _http.PostAsJsonAsync("api/StudentRecord/Update%20Contact%20Details", contactInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<ContactInformation>();
        }
        public async Task<ContactInformationDto?> DisplayContactInformationAsync()
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<ContactInformationDto>("api/StudentRecord/Display%20Contact%20Details");
        }
        public async Task<SchoolIdDto?> StudentSchoolIdAsync(StudentMiniInfoDto data)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var request = await _http.PostAsJsonAsync($"api/StudentRecord/Assign%20StudentID", data);
            if (!request.IsSuccessStatusCode) { return null; }
            return await request.Content.ReadFromJsonAsync<SchoolIdDto>();

        }
        public async Task<ProfileUpdateDto?> UpdateAllDetailsAsync(ProfileUpdateDto studentUpdate)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var request = await _http.PatchAsJsonAsync("api/StudentRecord/UpdateAllDetails", studentUpdate);
            if (!request.IsSuccessStatusCode)
            {
                var content = await request.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"Failed to update student record. Status: {request.StatusCode}. Response: {content}");
            }

            return await request.Content.ReadFromJsonAsync<ProfileUpdateDto>();
        }

        public async Task<EnrollmentFormDto?> UpdateEnrollmentFormAsync(EnrollmentFormDto studentUpdate)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var request = await _http.PatchAsJsonAsync("api/StudentRecord/Update%20EnrollmentForm", studentUpdate);
            if (!request.IsSuccessStatusCode) { return null; }
            return await request.Content.ReadFromJsonAsync<EnrollmentFormDto>();

        }
        public async Task<bool> StudentSaveInformationAsync()
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var request = await _http.PatchAsync("api/StudentRecord/Student%20SaveInformationAsync", null);
            if (!request.IsSuccessStatusCode) { return false; }
            return await request.Content.ReadFromJsonAsync<bool>();
        }
        public async Task<MiniDisplayMenuDto?> MiniDisplayMenuAsync()
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<MiniDisplayMenuDto>("api/StudentRecord/Display%20MiniDetailsMenu");
        }
        public async Task<bool> CheckInformationAsync()
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<bool>("api/StudentRecord/Check%20Information");
        }
        public async Task<FacultyPhotoId?> StudentPhotoIDAsync()
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<FacultyPhotoId>("api/StudentRecord/Student%20PhotoID");
        }
    }
}