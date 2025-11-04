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
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDto;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services
{
    public class StudentRecordApiCommand : IStudentRecordApiCommand
    {
        private readonly HttpClient _http;
        private readonly ProtectedLocalStorage _localstorage;
        private readonly IAuthApiServices _authApi;
        public StudentRecordApiCommand(HttpClient http, ProtectedLocalStorage localStorage, IAuthApiServices authApi)
        {
            _http = http;
            _localstorage = localStorage;
            _authApi = authApi;
        }
        public async Task SetAuthHeaderAsync()
        {
            var token = await _localstorage.GetAsync<string>("AccessToken");
            var results = token.Success ? token.Value : null;
                      
            if (string.IsNullOrEmpty(results))
            {
                var refresh = await _authApi.TryRefreshTokenAsync();
                if (refresh)
                {
                    var set = await _localstorage.GetAsync<string>("AccessToken");
                    results = set.Success ? set.Value : null;                
                }  
            }
            if (!string.IsNullOrEmpty(results)) _http.DefaultRequestHeaders.Authorization =
       new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", results);
        }

        public async Task<PersonalInformation?> AddPersonalDetailsAsync(PersonalInformationDto personalInformation)
        {
            await SetAuthHeaderAsync();
            var results = await _http.PostAsJsonAsync("api/StudentRecord/Add%20Personal%20Details", personalInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<PersonalInformation>();
        }
        public async Task<PersonalInformation?> UpdatePersonalInformationAsync(PersonalInformationDto personalInformation)
        {
            await SetAuthHeaderAsync();
            var results = await _http.PatchAsJsonAsync("api/StudentRecord/Update%20Personal%20Details", personalInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<PersonalInformation>();
        }
        public async Task<PersonalInformationDto?> DisplayPersonalInformationAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<PersonalInformationDto>("api/StudentRecord/Display%20Personal%20Details");
        }
        public async Task<AcademicInformation?> AddAcademicDetailsAsync(AcademicInformationDto academicInformation)
        {
            await SetAuthHeaderAsync();
            var results = await _http.PostAsJsonAsync("api/StudentRecord/Add%20Academic%20Details", academicInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<AcademicInformation>();
        }
        public async Task<AcademicInformation?> UpdateAcademicInformationAsync(AcademicInformationDto academicInformation)
        {
            await SetAuthHeaderAsync();
            var results = await _http.PostAsJsonAsync("api/StudentRecord/Update%20Academic%20Details", academicInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<AcademicInformation>();
        }
        public async Task<AcademicInformationDto?> DisplayAcademicInformation()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<AcademicInformationDto>("api/StudentRecord/Display%20Academic%20Details");
        }
        public async Task<ContactInformation?> AddContactInformationAsync(ContactInformationDto contactInformation)
        {
            await SetAuthHeaderAsync();
            var results = await _http.PostAsJsonAsync("api/StudentRecord/Add%20Contact%20Details", contactInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<ContactInformation>();
        }
        public async Task<ContactInformation?> UpdateContactInformationAsync(ContactInformationDto contactInformation)
        {
            await SetAuthHeaderAsync();
            var results = await _http.PostAsJsonAsync("api/StudentRecord/Update%20Contact%20Details", contactInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<ContactInformation>();
        }
        public async Task<ContactInformationDto?> DisplayContactInformationAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<ContactInformationDto>("api/StudentRecord/Display%20Contact%20Details");
        }
        public async Task<SchoolIdDto?> StudentSchoolIdAsync(string SchoolId)
        {
            await SetAuthHeaderAsync();
           var request = await _http.PostAsync($"api/StudentRecord/Assign%20StudentID?SchoolId={SchoolId}",null);
            if (!request.IsSuccessStatusCode){ return null; }
            return await request.Content.ReadFromJsonAsync<SchoolIdDto>();

        }
        public async Task<StudentUpdateInformationDto?> UpdateAllDetailsAsync(StudentUpdateInformationDto studentUpdate)
        {
            await SetAuthHeaderAsync();
            var request = await _http.PatchAsJsonAsync("api/StudentRe   var request = await _http.PostAsync($\"api/StudentRecord/Assign%20StudentID?SchoolId={SchoolId}\",null);cord/UpdateAll%20Details", studentUpdate);
            if (!request.IsSuccessStatusCode) { return null; }
            return await request.Content.ReadFromJsonAsync<StudentUpdateInformationDto>();
        }
        public async Task<bool> StudentSaveInformationAsync()
        {
            await SetAuthHeaderAsync();
            var request = await _http.PatchAsync("api/StudentRecord/Student%20SaveInformationAsync", null);
            if (!request.IsSuccessStatusCode) { return false; }
            return await request.Content.ReadFromJsonAsync<bool>();
        }
        public async Task<MiniDisplayMenuDto?> MiniDisplayMenuAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<MiniDisplayMenuDto>("api/StudentRecord/Display%20MiniDetailsMenu");
        }
        public async Task<bool> CheckInformationAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<bool>("api/StudentRecord/Check%20Information");
        }
    }
}
