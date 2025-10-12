using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;
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
        public StudentRecordApiCommand(HttpClient http, ProtectedLocalStorage localStorage)
        {
            _http = http;
            _localstorage = localStorage;
        }
        public async Task SetAuthHeaderAsync()
        {
            var token = await _localstorage.GetAsync<string>("AccessToken");
            var results = token.Success ? token.Value : null;
            if (!string.IsNullOrEmpty(results)) _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", results);
        }
        public async Task<PersonalInformation?> AddPersonalDetailsAsync(PersonalInformationDto personalInformation)
        {
            await SetAuthHeaderAsync();
            var results = await _http.PostAsJsonAsync("api/StudentRecord/AddPersonalDetails", personalInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<PersonalInformation>();
        }
        public async Task<PersonalInformation?> UpdatePersonalInformationAsync(PersonalInformationDto personalInformation)
        {
            await SetAuthHeaderAsync();
            var results = await _http.PatchAsJsonAsync("api/StudentRecord/UpdatePersonalDetails", personalInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<PersonalInformation>();
        }
        public async Task<IEnumerable<PersonalInformationDto>?> DisplayPersonalInformationAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<IEnumerable<PersonalInformationDto>>("api/StudentRecord/DisplayPersonalDetails");
        }
        public async Task<AcademicInformation?> AddAcademicDetailsAsync(AcademicInformationDto academicInformation)
        {
            await SetAuthHeaderAsync();
            var results = await _http.PostAsJsonAsync("api/StudentRecord/AddAcademicDetails", academicInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<AcademicInformation>();
        }
        public async Task<AcademicInformation?> UpdateAcademicInformationAsync(AcademicInformationDto academicInformation)
        {
            await SetAuthHeaderAsync();
            var results = await _http.PostAsJsonAsync("api/StudentRecord/UpdateAcademicDetails", academicInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<AcademicInformation>();
        }
        public async Task<IEnumerable<AcademicInformationDto>?> DisplayAcademicInformation()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<IEnumerable<AcademicInformationDto>>("api/StudentRecord/DisplayAcademicDetails");
        }
        public async Task<ContactInformation?> AddContactInformationAsync(ContactInformationDto contactInformation)
        {
            await SetAuthHeaderAsync();
            var results = await _http.PostAsJsonAsync("api/StudentRecord/AddContactDetails", contactInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<ContactInformation>();
        }
        public async Task<ContactInformation?> UpdateContactInformationAsync(ContactInformationDto contactInformation)
        {
            await SetAuthHeaderAsync();
            var results = await _http.PostAsJsonAsync("api/StudentRecord/UpdateContactDetails", contactInformation);
            if (!results.IsSuccessStatusCode) return null;
            return await results.Content.ReadFromJsonAsync<ContactInformation>();
        }
        public async Task<IEnumerable<ContactInformationDto>?> DisplayContactInformationAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<IEnumerable<ContactInformationDto>>("api/StudentRecord/DisplayContactDetails");
        }
    }
}
