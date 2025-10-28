using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using static System.Net.WebRequestMethods;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.External
{
    public interface IEnrollmentApiServices
    {
        Task<PaymentDetailsDto?> EnrollCourseAsync(PaymentDetailsDto paymentdetails);
        Task<IEnumerable<CourseDto>?> DisplayCourseAsync();
        Task<bool> UnenrollCourse(string CourseId);
        Task<ProvisionDto?> ProvisionAsync(string courseCode);
        Task<CourseDto?> PreviewCourseAsync(Guid CourseId);
        Task<List<AnnouncementDto>?> DisplayAllAnnouncementAsync();
        Task<List<AnnouncementDto>?> DisplayAnnouncementAsync(Guid CourseId);
        Task<List<AnnouncementDto>?> DisplayAnnouncementByType(InformationType type);

    }
}
