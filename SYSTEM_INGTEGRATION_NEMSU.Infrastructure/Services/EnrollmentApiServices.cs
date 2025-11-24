using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore.SqlServer.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services
{
   public class EnrollmentApiServices : IEnrollmentApiServices
    {
        private readonly HttpClient _http;
      
        private readonly IAuthHelper _authApi;
        public EnrollmentApiServices(HttpClient HttpClient,  IAuthHelper authHelper )
        {
            _http = HttpClient;     
            _authApi = authHelper;
        }

   
        public async Task<PaymentDetailsDto?> EnrollCourseAsync(PaymentDetailsDto paymentdetails)
        {
            await _authApi.SetAuthHeaderAsync(_http);        
            var response = await _http.PostAsJsonAsync("api/EnrollmentHandling/Enroll%20Course", paymentdetails);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<PaymentDetailsDto>();
        }
        public async Task<ProvisionDto?> ProvisionAsync(string courseCode)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var response = await _http.PostAsync($"api/EnrollmentHandling/ProvisionEnrollCourse?courseCode={courseCode}",null);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<ProvisionDto>();
        }
        public async Task<bool> PayProvisionAsync(PaymentDetailsDto paymentDetails)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var response = await _http.PostAsJsonAsync($"api/EnrollmentHandling/Pay%20ProvisionAsync", paymentDetails);
            if(!response.IsSuccessStatusCode) { return false; }
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<List<EnrollCourseDto>?> DisplayAllCourseEnrolledAsync()
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<List<EnrollCourseDto>>("api/EnrollmentHandling/Display%20AllEnrolledCourse");
        }
        public async Task<IEnumerable<CourseDto>?> DisplayAllCourseDetailsAsync()
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<IEnumerable<CourseDto>>("api/EnrollmentHandling/Display%20AllCourseDetails");           
        }
        public async Task<bool> UnenrollCourse(string CourseId)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.DeleteFromJsonAsync<bool>($"api/EnrollmentHandling/UnEnroll%20Course/{CourseId}");     
        }
        public async Task<bool> InactiveCourse(Guid CourseId)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var response = await _http.PatchAsync(
           $"api/EnrollmentHandling/inactive%20course?courseId={CourseId}",new StringContent(string.Empty));
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        public async Task<bool> ActiveCourseAsync(Guid CourseId)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var response = await _http.PatchAsync(
           $"api/EnrollmentHandling/Active%20Course?courseId={CourseId}", new StringContent(string.Empty));
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        public async Task<CourseDto?> PreviewCourseAsync(Guid CourseId)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<CourseDto>($"api/EnrollmentHandling/Display%20PreviewCourse?CourseId={CourseId}");
        }
        public async Task<PaymentDetailsDto?> AddPaymentAsync( PaymentDetailsDto payment)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var request = await _http.PostAsJsonAsync("api/EnrollmentHandling/Add%20Payment", payment);
            if (!request.IsSuccessStatusCode)
            {
                return null;
            }
            return await request.Content.ReadFromJsonAsync<PaymentDetailsDto>();
        }
        public async Task<List<PaymentDetailsDto>?> DisplayPaymentAsync()
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<List<PaymentDetailsDto>>("api/EnrollmentHandling/Display%20Payment");
        }
        public async Task<bool> DeletePaymentAsync(Guid PaymentId)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.DeleteFromJsonAsync<bool>($"api/EnrollmentHandling/Delete%20PaymentDetails?PaymentId={PaymentId}");
        }
        public async Task<List<AnnouncementDto>?> DisplayAllAnnouncementAsync()
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<List<AnnouncementDto>>($"api/EnrollmentHandling/Display%20AllAnnouncement");
        }
        public async Task<List<AnnouncementDto>?> DisplayAnnouncementAsync(Guid CourseId)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<List<AnnouncementDto>>($"api/EnrollmentHandling/Display%20CourseAnnouncement?CourseId={CourseId}");
        }
        public async Task<List<AnnouncementDto>?> DisplayAnnouncementByType(InformationType type)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<List<AnnouncementDto>>($"api/EnrollmentHandling/Display%20AnnouncementByType?type={type}");
            
        }
        public async Task<EnrolledCourseViewDto?> GetCourse(Guid CourseId)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<EnrolledCourseViewDto>($"api/EnrollmentHandling/Get%20Course?CourseId={CourseId}");
        }
        public async Task<List<AnnouncementDto>?> DisplayAllTypeAnnouncementAsync(Guid CourseId)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<List<AnnouncementDto>>($"api/EnrollmentHandling/Display%20DisplayAllTypeAnnouncementAsync?CourseId={CourseId}");
        }
        public async Task<CourseTrack> CourseTrackerAsync(Guid CourseId)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            return await _http.GetFromJsonAsync<CourseTrack>($"api/EnrollmentHandling/Course%20TrackerAsync?CourseId={CourseId}");
        }
        public async Task<SchoolIdDto?> GenerateStudentId()
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var request = await _http.PostAsync("api/EnrollmentHandling/Generate%20StudentId",null);
            if (!request.IsSuccessStatusCode)
            {
                return null;
            }
            return await request.Content.ReadFromJsonAsync<SchoolIdDto>();
        }
        public async Task<bool> DirectEnrollAsync(Guid CourseId)
        {
            await _authApi.SetAuthHeaderAsync(_http);
            var request = await _http.PostAsync($"api/EnrollmentHandling/Direct%20EnrollAsync?CourseId={CourseId}",null);
            if (!request.IsSuccessStatusCode) { return false; }
            return await request.Content.ReadFromJsonAsync<bool>();
        }






    }   
}
