using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories
{
    public class HandlingStudents(ApplicationDbContext context) : IHandlingStudents
    {
        public async Task<List<HandlingStudentsDto>> DisplayStudentsAsync(Guid AdminId)
        {

            var request = await context.enrollcourse
                .Include(s => s.Student)
                    .ThenInclude(s => s.StudentsDetails)
                .Include(s => s.Course)

                .Where(s => s.Course.AdminId == AdminId)
                .Select(s => new HandlingStudentsDto
                {
                    StudentId = s.StudentId,
                    StudentName = s.Student.StudentsDetails != null
                        ? s.Student.StudentsDetails.FirstName + " " + s.Student.StudentsDetails.LastName
                        : "N/A",
                    CourseTitle = s.Course != null ? s.Course.Title! : "N/A",
                    DateEnrolled = s.DateEnrolled,
                    Email = s.Student.StudentContactDetails != null ? s.Student.StudentContactDetails.EmailAddress : "N/A",
                    StudentSchoolId = s.Student.StudentAcademicDetails != null ? s.Student.StudentAcademicDetails.StudentSchoolId : "0000-0000",
                    studentCourseStatus = s.studentCourseStatus,
                })
                .ToListAsync();

            return request;
        }
        public async Task<List<HandlingStudentsDto>> DisplayStudentByCoursesAsync(Guid AdminId, string CourseCode)
        {

            var request = await context.enrollcourse
                .Include(s => s.Student)
                .ThenInclude(s => s.StudentsDetails)
                .Include(s => s.Student)
                .ThenInclude(s => s.StudentContactDetails)
                .Include(s => s.Student)
                .ThenInclude(s => s.StudentAcademicDetails)
                .Include(s => s.Course)
                .Where(s => s.Course.AdminId == AdminId && s.Course.CourseCode == CourseCode)
                .Select(s => new HandlingStudentsDto
                {
                    StudentId = s.StudentId,
                    StudentName = s.Student.StudentsDetails != null
                        ? s.Student.StudentsDetails.FirstName + " " + s.Student.StudentsDetails.LastName
                        : "N/A",
                    CourseTitle = s.Course != null ? s.Course.Title! : "N/A",
                    DateEnrolled = s.DateEnrolled,
                    StudentSchoolId = s.Student.StudentAcademicDetails != null ? s.Student.StudentAcademicDetails.StudentSchoolId : "0000-0000",
                    Email = s.Student.StudentContactDetails != null ? s.Student.StudentContactDetails.EmailAddress : "N/A",
                    studentCourseStatus = s.studentCourseStatus,
                })
                .ToListAsync();

            return request;
        }
        public async Task<HandlingAllStudentsDetailsDto?> DisplayAllStudentsDetailsAsync(Guid AdminId, Guid StudentId)
        {
            var request = await context.enrollcourse
                    .Include(x => x.Course)

                    .Include(x => x.Student)
                     .ThenInclude(s => s.StudentsDetails)
                     .Include(s => s.Student)
                     .ThenInclude(s => s.StudentAcademicDetails)
                     .Include(s => s.Student)
                     .ThenInclude(s => s.StudentContactDetails)
                .FirstOrDefaultAsync(s => s.Course.AdminId == AdminId && s.StudentId == StudentId);
            if (request is null) return null;
            var retrieve = new HandlingAllStudentsDetailsDto
            {
                StudentId = request.StudentId,
                FullName = request.Student.StudentsDetails != null ? request.Student.StudentsDetails.FirstName + " " + request.Student.StudentsDetails.LastName : "N/A",
                DateOfBirth = request.Student.StudentsDetails?.DateOfBirth,
                Gender = request.Student.StudentsDetails?.Gender,
                CivilStatus = request.Student.StudentsDetails?.CivilStatus,
                Nationality = request.Student.StudentsDetails?.Nationality,
                PermanentAddress = request.Student.StudentsDetails?.PermanentAddress,
                StudentSchoolId = request.Student.StudentAcademicDetails?.StudentSchoolId,
                StudentType = request.Student.StudentAcademicDetails?.StudentType,
                YearLevel = request.Student.StudentAcademicDetails?.YearLevel,
                Semester = request.Student.StudentAcademicDetails?.Semester,
                Program = request.Student.StudentAcademicDetails?.Program,
                Major = request.Student.StudentAcademicDetails?.Major,
                Strand = request.Student.StudentAcademicDetails?.Strand,
                MobileNumber = request.Student.StudentContactDetails?.MobileNumber,
                EmailAddress = request.Student.StudentContactDetails?.EmailAddress,
                EmergencyContactNumber = request.Student.StudentContactDetails?.EmergencyContactNumber,
                DateEnrolled = request.DateEnrolled,
                studentCourseStatus = request.studentCourseStatus
            };
            return retrieve;
        }


        public async Task<List<HandlingStudentsDto>> DisplayStudentByDepartmentAsync(Guid AdminId, CourseDepartment department)
        {

            var request = await context.enrollcourse
                .Include(s => s.Student)
                    .ThenInclude(s => s.StudentsDetails)
                .Include(s => s.Course)

                .Where(s => s.Course.AdminId == AdminId && s.Course.Department == department)
                .Select(s => new HandlingStudentsDto
                {
                    StudentId = s.StudentId,
                    StudentName = s.Student.StudentsDetails != null
                        ? s.Student.StudentsDetails.FirstName + " " + s.Student.StudentsDetails.LastName
                        : "N/A",
                    CourseTitle = s.Course != null ? s.Course.Title! : "N/A",
                    DateEnrolled = s.DateEnrolled,
                    Email = s.Student.StudentContactDetails != null ? s.Student.StudentContactDetails.EmailAddress : "N/A",
                    StudentSchoolId = s.Student.StudentAcademicDetails != null ? s.Student.StudentAcademicDetails.StudentSchoolId : "0000-0000",
                    studentCourseStatus = s.studentCourseStatus,
                    Coursedepartment = department,
                })
                .ToListAsync();

            return request;
        }



    }
}
