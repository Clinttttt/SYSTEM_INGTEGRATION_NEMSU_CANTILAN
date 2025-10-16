using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
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
                    StudentSchoolId = s.Student.StudentAcademicDetails != null ? s.Student.StudentAcademicDetails.StudentSchoolId : "0000-0000"
                })
                .ToListAsync();

            return request;
        }






    }
}
