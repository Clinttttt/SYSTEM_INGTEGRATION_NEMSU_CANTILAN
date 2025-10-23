using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Client.Helper;
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
                    studentCourseStatus = s.StudentCourseStatus,
                    ProfileColor = s.ProfileColor,
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
                    studentCourseStatus = s.StudentCourseStatus,
                    ProfileColor = s.ProfileColor,
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
                studentCourseStatus = request.StudentCourseStatus,
                ProfileColor = request.ProfileColor,

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
                    studentCourseStatus = s.StudentCourseStatus,
                    Coursedepartment = department,
                    ProfileColor = s.ProfileColor,
                })
                .ToListAsync();

            return request;
        }
        public async Task<SummaryStatisticsDto> SummaryStatisticsAsync(Guid Admin)
        {
            var request = await context.enrollcourse.Where(s => s.Course.AdminId == Admin).ToListAsync();
            var course = await context.course.Where(s => s.AdminId == Admin).ToListAsync();
            var filter = new SummaryStatisticsDto();
            filter.TotalStudent = request.Count();
            filter.TotalActive = request.Where(s => s.StudentCourseStatus == StudentCourseStatus.Active).Count();
            filter.TotalInactive = request.Where(s => s.StudentCourseStatus == StudentCourseStatus.Inactive).Count();
            filter.TotalDepartment = 5;
            filter.TotalCourse = course.Count();
            return filter;
        }


        public async Task<List<DepartmentStatsDto>> DepartmentStatsAsync(Guid AdminId)
        {
            var departmentColors = new Dictionary<CourseDepartment, (string Color, string LightColor)>
    {
        { CourseDepartment.DIT, ("#dc2626", "#f87171") },      // R
        { CourseDepartment.DCS, ("#ea580c", "#fb923c") },      // O
        { CourseDepartment.DGTT, ("#16a34a", "#4ade80") },     // G
        { CourseDepartment.CCJE, ("#0891b2", "#06b6d4") },     // B
        { CourseDepartment.DBM, ("#eab308", "#fde047") }       // Y
    };


            var allDepartments = Enum.GetValues(typeof(CourseDepartment))
                .Cast<CourseDepartment>()
                .ToList();

            var departmentCounts = await context.enrollcourse
                .Include(s => s.Course)
                .Where(s => s.Course.AdminId == AdminId)
                .GroupBy(s => s.Course.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

          
            var totalStudents = departmentCounts.Sum(d => d.Count);

         
            var GetData = allDepartments.Select((dept, index) =>
            {
                var deptCount = departmentCounts.FirstOrDefault(d => d.Department == dept);
                var count = deptCount?.Count ?? 0;

                var colors = departmentColors.ContainsKey(dept) 
                ? departmentColors[dept]
                : ("#6b7280", "#9ca3af");
                return new DepartmentStatsDto
                {
                    DepartmentName = dept.GetDisplayName(),
                    Count = count,
                    Color = colors.Item1,
                    LightColor = colors.Item2,
                    Percentage = totalStudents > 0 ? Math.Round((count * 100.0) / totalStudents, 1) : 0
                };
            })
            .OrderByDescending(d => d.Count)  
            .ToList();

            return GetData;
        }
     

    }
}

