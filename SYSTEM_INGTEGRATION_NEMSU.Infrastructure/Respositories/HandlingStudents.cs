using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Client.Helper;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;
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
                .Include(s => s.Student)
                .ThenInclude(s => s.StudentAcademicDetails)
                .Include(s => s.Student)
                .ThenInclude(s => s.StudentContactDetails)
                .Include(s => s.Course)
                .Where(s => s.Course.AdminId == AdminId).ToListAsync();

            var distinctStudents = request
      .GroupBy(s => s.StudentId)
      .Select(g => g.OrderByDescending(s => s.DateEnrolled).First())

              .Select(s => new HandlingStudentsDto
              {
                  StudentId = s.StudentId,
                  StudentName = s.Student.StudentsDetails != null
                        ? s.Student.StudentsDetails.FirstName + " " + s.Student.StudentsDetails.LastName
                        : "N/A",
                  DateEnrolled = s.DateEnrolled,
                  Email = s.Student.StudentContactDetails != null ? s.Student.StudentContactDetails.EmailAddress : "N/A",
                  StudentSchoolId = s.Student.StudentAcademicDetails != null ? s.Student.StudentAcademicDetails.StudentSchoolId : "0000-0000",
                  studentCourseStatus = s.StudentCourseStatus,
                  ProfileColor = s.ProfileColor,
                  Coursedepartment = s.Course!.Department.GetDisplayName(),
              }).ToList();


            return distinctStudents;
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
                    enrollmentStatus = s.EnrollmentStatus,
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
                FullName = request.Student.StudentsDetails != null ? request.Student.StudentsDetails.FirstName + " " + request.Student.StudentsDetails.MiddleName + "," + " " + request.Student.StudentsDetails.LastName : "N/A",
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
                    .Include(s => s.Student)
                .ThenInclude(s => s.StudentContactDetails)
                .Include(s => s.Student)
                .ThenInclude(s => s.StudentAcademicDetails)
                .Include(s => s.Course)
                .Where(s => s.Course.AdminId == AdminId && s.Course.Department == department).ToListAsync();

            var disctinctResults = request
                .GroupBy(s => s.StudentId)
                .Select(s => s.OrderByDescending(s => s.DateEnrolled).First())

                .Select(s => new HandlingStudentsDto
                {
                    StudentId = s.StudentId,
                    StudentName = s.Student.StudentsDetails != null
                        ? s.Student.StudentsDetails.FirstName + " " + s.Student.StudentsDetails.LastName
                        : "N/A",

                    DateEnrolled = s.DateEnrolled,
                    Email = s.Student.StudentContactDetails != null ? s.Student.StudentContactDetails.EmailAddress : "N/A",
                    StudentSchoolId = s.Student.StudentAcademicDetails != null ? s.Student.StudentAcademicDetails.StudentSchoolId : "0000-0000",
                    studentCourseStatus = s.StudentCourseStatus,
                    Coursedepartment = department.GetDisplayName(),
                    ProfileColor = s.ProfileColor,
                }).ToList();


            return disctinctResults;
        }
        public async Task<SummaryStatisticsDto> SummaryStatisticsAsync(Guid Admin)
        {
            var request = await context.enrollcourse.Where(s => s.Course.AdminId == Admin)
               .ToListAsync();

            var distinctStudent = request
                .GroupBy(s => s.StudentId)
                .Select(s => s.OrderByDescending(s => s.DateEnrolled).First()).ToList();

            var course = await context.course.Where(s => s.AdminId == Admin).ToListAsync();
            var filter = new SummaryStatisticsDto();
            filter.TotalStudent = distinctStudent.Count();
            filter.TotalActive = distinctStudent.Where(s => s.StudentCourseStatus == StudentCourseStatus.Active).Count();
            filter.TotalInactive = distinctStudent.Where(s => s.StudentCourseStatus == StudentCourseStatus.Inactive).Count();
            filter.TotalDepartment = 5;
            filter.TotalCourse = course.Count;
            return filter;
        }


        public async Task<List<DepartmentStatsDto>> DepartmentStatsAsync(Guid AdminId)
        {
            var departmentColors = new Dictionary<CourseDepartment, (string Color, string LightColor)>
    {
        { CourseDepartment.DIT, ("#dc2626", "#f87171") },
        { CourseDepartment.DCS, ("#ea580c", "#fb923c") },
        { CourseDepartment.DGTT, ("#16a34a", "#4ade80") },
        { CourseDepartment.CCJE, ("#0891b2", "#06b6d4") },
        { CourseDepartment.DBM, ("#eab308", "#fde047") }
    };


            var allDepartments = Enum.GetValues(typeof(CourseDepartment))
                .Cast<CourseDepartment>()
                .Where(d => d != CourseDepartment.None)
                .ToList();

            var departmentCounts = await context.enrollcourse
                .Include(s => s.Course)
                .Where(s => s.Course.AdminId == AdminId).ToListAsync();

            var distinctResults = departmentCounts
                .GroupBy(s => s.StudentId)

                .Select(s => s.OrderByDescending(s => s.DateEnrolled).First()).ToList();


            var disctinct = distinctResults
                .GroupBy(s => s.Course.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    Count = g.Count()
                }).ToList();





            var totalStudents = disctinct.Sum(d => d.Count);


            var GetData = allDepartments

                .Select((dept, index) =>
            {
                var deptCount = disctinct.FirstOrDefault(d => d.Department == dept);
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

        public async Task<(List<HandlingStudentsDto> Students, int TotalCount)> StudentByYearLevelAsync(
     Guid AdminId,
     CourseProgram choice,
     YearLevelChoice yearLevel,
     int pageNumber = 1,
     int pageSize = 10,
     string searchQuery = "")
        {
            var query = context.enrollcourse
                .Include(s => s.Student)
                    .ThenInclude(s => s.StudentAcademicDetails)
                .Include(s => s.Course)
                .Include(s => s.Student)
                    .ThenInclude(s => s.StudentContactDetails)
                .Include(s => s.Student)
                    .ThenInclude(s => s.StudentsDetails)
                .AsNoTracking()
                .Where(s => s.Student.StudentAcademicDetails!.Program == choice
                    && s.Course.AdminId == AdminId
                    && s.Student.StudentAcademicDetails.YearLevel == yearLevel);

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var search = searchQuery.ToLower();
                query = query.Where(s =>
                    s.Student.StudentsDetails!.FirstName!.ToLower().Contains(search) ||
                    s.Student.StudentsDetails.LastName!.ToLower().Contains(search) ||
                    s.Student.StudentAcademicDetails!.StudentSchoolId!.ToLower().Contains(search) ||
                    s.Student.StudentContactDetails!.EmailAddress!.ToLower().Contains(search) ||
                    s.Course.Title!.ToLower().Contains(search)
                );
            }


            var totalCount = await query
                .Select(s => s.StudentId)
                .Distinct()
                .CountAsync();


            var studentIds = await query
                .OrderBy(s => s.Student.StudentsDetails!.LastName)
                .Select(s => s.StudentId)
                .Distinct()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            var enrollments = await context.enrollcourse
                .Include(s => s.Student)
                    .ThenInclude(s => s.StudentAcademicDetails)
                .Include(s => s.Course)
                .Include(s => s.Student)
                    .ThenInclude(s => s.StudentContactDetails)
                .Include(s => s.Student)
                    .ThenInclude(s => s.StudentsDetails)
                .AsNoTracking()
                .Where(s => studentIds.Contains(s.StudentId))
                .ToListAsync();


            var students = enrollments
                .GroupBy(s => s.StudentId)
                .Select(g => g.First())
                .Select(s => new HandlingStudentsDto
                {
                    StudentName = $"{s.Student.StudentsDetails!.FirstName} {s.Student.StudentsDetails.MiddleName} {s.Student.StudentsDetails.LastName}",
                    StudentSchoolId = s.Student.StudentAcademicDetails!.StudentSchoolId,
                    CourseTitle = s.Course.Title!,
                    DateEnrolled = s.DateEnrolled,
                    Email = s.Student.StudentContactDetails!.EmailAddress,
                    studentCourseStatus = s.StudentCourseStatus,
                    Coursedepartment = s.Course.Department.GetDisplayName(),
                    ProfileColor = s.ProfileColor,
                    StudentId = s.StudentId,
                })
                .ToList();

            return (students, totalCount);
        }
        public async Task<List<StudendBillRecordDtoDto>> StudentRecordAsync(Guid AdminId)
        {
            var StudentEnrolled = await context.enrollcourse
                .Include(s => s.Course)
                .Include(s => s.Invoice)
                .Include(s=> s.Student)
                .ThenInclude(s=> s.StudentsDetails)
                .Where(s => s.Course.AdminId == AdminId)
                .Select(s => new StudendBillRecordDtoDto
                {
                    StudentId = s.Student.StudentAcademicDetails != null ? s.Student.StudentAcademicDetails.StudentSchoolId : "N/A",
                    DateEnrolled = s.DateEnrolled,
                    CourseCode = s.Course.CourseCode,
                    Status = s.Invoice != null ? s.Invoice.Status : InvoiceStatus.Unpaid,
                    Cost = s.Invoice != null ? s.Invoice.Cost : 0.00,
                    FullName = s.Student.StudentsDetails != null ? s.Student.StudentsDetails.FirstName + " " + s.Student.StudentsDetails.MiddleName + " " + s.Student.StudentsDetails.LastName : "N/A"
                })
                   .OrderByDescending(s => s.DateEnrolled) 
                   .ToListAsync();
            return StudentEnrolled;
        }
    }
}

