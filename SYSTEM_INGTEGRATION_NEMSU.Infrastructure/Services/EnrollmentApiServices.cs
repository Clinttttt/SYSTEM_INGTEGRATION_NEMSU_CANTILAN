using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore.SqlServer.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
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
        public async Task<PaymentDetailsDto?> EnrollCourseAsync(PaymentDetailsDto paymentdetails)
        {
            await SetAuthHeaderAsync();        
            var response = await _http.PostAsJsonAsync("api/EnrollmentHandling/Enroll%20Course", paymentdetails);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<PaymentDetailsDto>();
        }
        public async Task<ProvisionDto?> ProvisionAsync(string courseCode)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsync($"api/EnrollmentHandling/ProvisionEnrollCourse?courseCode={courseCode}",null);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<ProvisionDto>();
        }

        public async Task<List<EnrollCourseDto>?> DisplayAllCourseEnrolledAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<List<EnrollCourseDto>>("api/EnrollmentHandling/Display%20AllEnrolledCourse");
        }
        public async Task<IEnumerable<CourseDto>?> DisplayAllCourseDetailsAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<IEnumerable<CourseDto>>("api/EnrollmentHandling/Display%20AllCourseDetails");           
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
        public async Task<PaymentDetailsDto?> AddPaymentAsync( PaymentDetailsDto payment)
        {
            await SetAuthHeaderAsync();
            var request = await _http.PostAsJsonAsync("api/EnrollmentHandling/Add%20Payment", payment);
            if (!request.IsSuccessStatusCode)
            {
                return null;
            }
            return await request.Content.ReadFromJsonAsync<PaymentDetailsDto>();
        }
        public async Task<List<PaymentDetailsDto>?> DisplayPaymentAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<List<PaymentDetailsDto>>("api/EnrollmentHandling/Display%20Payment");
        }
        public async Task<bool> DeletePaymentAsync(Guid PaymentId)
        {
            await SetAuthHeaderAsync();
            return await _http.DeleteFromJsonAsync<bool>($"api/EnrollmentHandling/Delete%20PaymentDetails?PaymentId={PaymentId}");
        }
        public async Task<List<AnnouncementDto>?> DisplayAllAnnouncementAsync()
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<List<AnnouncementDto>>($"api/EnrollmentHandling/Display%20AllAnnouncement");
        }
        public async Task<List<AnnouncementDto>?> DisplayAnnouncementAsync(Guid CourseId)
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<List<AnnouncementDto>>($"api/EnrollmentHandling/Display%20CourseAnnouncement?CourseId={CourseId}");
        }
        public async Task<List<AnnouncementDto>?> DisplayAnnouncementByType(InformationType type)
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<List<AnnouncementDto>>($"api/EnrollmentHandling/Display%20AnnouncementByType?type={type}");
            
        }
        public async Task<EnrolledCourseViewDto?> GetCourse(Guid CourseId)
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<EnrolledCourseViewDto>($"api/EnrollmentHandling/Get%20Course?CourseId={CourseId}");
        }
        public async Task<List<AnnouncementDto>?> DisplayAllTypeAnnouncementAsync(Guid CourseId)
        {
            await SetAuthHeaderAsync();
            return await _http.GetFromJsonAsync<List<AnnouncementDto>>($"api/EnrollmentHandling/Display%20DisplayAllTypeAnnouncementAsync?CourseId={CourseId}");
        }
     




    }   
}
