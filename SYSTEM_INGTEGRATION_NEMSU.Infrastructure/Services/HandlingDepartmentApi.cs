using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services
{
    public class HandlingDepartmentApi : IHandlingDepartmentApi
    {

        private readonly HttpClient _http;
        private readonly ProtectedLocalStorage _localstorage;

        public HandlingDepartmentApi(IHttpClientFactory httpClient, ProtectedLocalStorage localstorage)
        {
            _http = httpClient.CreateClient();
            _localstorage = localstorage;
        }
        public async Task SetAuthHeader()
        {
            var token = await _localstorage.GetAsync<string>("AccessToken");
            var results = token.Success ? token.Value : null;
            if (!string.IsNullOrEmpty(results))
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", results);
        }
        public async Task<List<CourseDto>?> DisplayDITAsync()
        {
            await SetAuthHeader();
            return await _http.GetFromJsonAsync<List<CourseDto>>("api/HandlingDepartment/Display%20DIT");
         
        }
        public async Task<List<CourseDto>?> DisplayDCSAsync()
        {
            await SetAuthHeader();
            return await _http.GetFromJsonAsync<List<CourseDto>>("api/HandlingDepartment/Display%20DCS");

        }
        public async Task<List<CourseDto>?> DisplayDGTTAsync()
        {
            await SetAuthHeader();
            return await _http.GetFromJsonAsync<List<CourseDto>>("api/HandlingDepartment/Display%20DGTT");
        }
        public async Task<List<CourseDto>?> DisplayDBMAsync()
        {
            await SetAuthHeader();
            return await _http.GetFromJsonAsync<List<CourseDto>>("api/HandlingDepartment/Display%20DBM");
        }
        public async Task<List<CourseDto>?> DisplayCCJEAsync()
        {
            await SetAuthHeader();
            return await _http.GetFromJsonAsync<List<CourseDto>>("api/HandlingDepartment/Display%20CCJE");
        }
    }
}
