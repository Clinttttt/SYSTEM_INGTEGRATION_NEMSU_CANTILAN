using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.Interface
{
    public interface IEnrollmentServices
    {
        Task<EnrollCourseDto?> EnrollCourseAsync(Guid studentId, string courseCode, EnrollmentStatus status);
        Task<IEnumerable<CourseDto>?> DisplayCourseAsync(Guid StudentId);
        Task<bool> UnEnrollCourseAsync(Guid studentId, string courseCode);
        Task<CourseDto?> GetCourse(Guid CourseId, Guid StudentId);
        Task<bool> InactiveCourseAsync(Guid StudentId, Guid CourseId);
        Task<bool> IctiveCourseAsync(Guid StudentId, Guid CourseId);
        Task<List<AnnouncementDto>> DisplayAnnounceMentAsync(Guid StudentId, string CourseCode);
        Task<CourseDto?> PreviewCourseAsync(Guid StudentId, Guid CourseId);
        Task<PaymentDetailsDto?> AddPaymentAsync( PaymentDetailsDto payment);
        Task<List<PaymentDetailsDto>?> DisplayPaymentAsync(Guid StudentVerification, Guid StudentId);
    }
}
