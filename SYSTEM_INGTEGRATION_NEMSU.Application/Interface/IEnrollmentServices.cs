using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.Interface
{
    public interface IEnrollmentServices
    {
        Task<EnrollCourseDto?> EnrollCourseAsync(Guid studentId, string courseCode, EnrollmentStatus status);
        Task<IEnumerable<CourseDto>?> DisplayCourseAsync(Guid StudentId);
        Task<List<EnrollCourseDto>?> DisplayAllCourseEnrolledAsync(Guid StudentId);
        Task<bool> UnEnrollCourseAsync(Guid studentId, string courseCode);
        Task<EnrolledCourseViewDto?> GetCourse(Guid CourseId, Guid StudentId);
        Task<bool> InactiveCourseAsync(Guid StudentId, Guid CourseId);
        Task<bool> ActiveCourseAsync(Guid StudentId, Guid CourseId);
        Task<CourseDto?> PreviewCourseAsync(Guid StudentId, Guid CourseId);     
        Task<List<AnnouncementDto>?> DisplayAllAnnouncementAsync(Guid StudentId);
        Task<List<AnnouncementDto>?> DisplayAnnouncementAsync(Guid CourseId, Guid StudentId);
        Task<List<AnnouncementDto>?> DisplayAnnouncementByType(Guid StudentId, InformationType type);
        Task<List<AnnouncementDto>?> DisplayAllTypeAnnouncementAsync(Guid CourseId, Guid StudentId);
        Task<PaymentDetailsDto?> AddPaymentAsync(PaymentDetailsDto payment);
        Task<List<PaymentDetailsDto>?> DisplayPaymentAsync(Guid StudentId);
        Task<bool> DeletePaymentAsync(Guid StudentId, Guid PaymentId);
        Task<SchoolIdDto?> GenerateStudentId(Guid StudentId);
        Task<CourseTrack> CourseTrackerAsync(Guid StudentId, Guid CourseId);
        Task<bool> CourseTrackAdd(Guid StudentId, Guid CourseId, EnrollmentStatus status);
        Task<bool> DirectEnrollAsync(Guid StudentId, Guid CourseId);
    }
}
