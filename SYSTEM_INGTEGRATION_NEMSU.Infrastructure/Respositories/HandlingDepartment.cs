using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services
{
    public class HandlingDepartment(ApplicationDbContext context) : IHandlingDepartment
    {
        public async Task<List<CourseDto>?> DisplayDITAsync(Guid AdminId)
        {
            var liststudent = await context.enrollcourse
                .Where(s => s.Course.AdminId == AdminId && s.Course.Department == CourseDepartment.DIT)
                .ToListAsync();
            var request = await context.course.Where(s => s.AdminId == AdminId && s.Department == CourseDepartment.DIT)
               .Include(s => s.Category)
               .Include(s => s.LearningObjectives)
               .ToListAsync();

            if (request is null) return null;
            var Filter = request.Adapt<List<CourseDto>>();
            foreach (var student in Filter) { student.ListEnrolled = liststudent; }
            return Filter.ToList();
        }
        public async Task<List<CourseDto>?> DisplayDCSAsync(Guid AdminId)
        {
            var liststudent = await context.enrollcourse.Where(s => s.Course.AdminId == AdminId && s.Course.Department == CourseDepartment.DCS).ToListAsync();
            var request = await context.course.Where(s => s.AdminId == AdminId && s.Department == CourseDepartment.DCS)
               .Include(s => s.Category)
               .Include(s => s.LearningObjectives)
               .ToListAsync();

            if (request is null) return null;
            var Filter = request.Adapt<List<CourseDto>>();
            foreach (var student in Filter) { student.ListEnrolled = liststudent; }
            return Filter.ToList();

        }
        public async Task<List<CourseDto>?> DisplayDGTTAsync(Guid AdminId)
        {
            var liststudent = await context.enrollcourse.Where(s => s.Course.AdminId == AdminId && s.Course.Department == CourseDepartment.DGTT).ToListAsync();
            var request = await context.course.Where(s => s.AdminId == AdminId && s.Department == CourseDepartment.DGTT)
               .Include(s => s.Category)
               .Include(s => s.LearningObjectives)
               .ToListAsync();
            if (request is null) return null;
            var Filter = request.Adapt<List<CourseDto>>();
            foreach (var student in Filter) { student.ListEnrolled = liststudent; }

            return Filter.ToList();
        }
        public async Task<List<CourseDto>?> DisplayDBMAsync(Guid AdminId)
        {
            var liststudent = await context.enrollcourse.Where(s => s.Course.AdminId == AdminId && s.Course.Department == CourseDepartment.DBM).ToListAsync();
            var request = await context.course.Where(s => s.AdminId == AdminId && s.Department == CourseDepartment.DBM)
               .Include(s => s.Category)
               .Include(s => s.LearningObjectives)
               .ToListAsync();
            if (request is null) return null;
            var Filter = request.Adapt<List<CourseDto>>();
            foreach (var student in Filter) { student.ListEnrolled = liststudent; }

            return Filter.ToList();
        }
        public async Task<List<CourseDto>?> DisplayCCJEAsync(Guid AdminId)
        {
            var liststudent = await context.enrollcourse.Where(s => s.Course.AdminId == AdminId && s.Course.Department == CourseDepartment.CCJE).ToListAsync();
            var request = await context.course.Where(s => s.AdminId == AdminId && s.Department == CourseDepartment.CCJE)
               .Include(s => s.Category)
               .Include(s => s.LearningObjectives)
               .ToListAsync();
            if (request is null) return null;
            var Filter = request.Adapt<List<CourseDto>>();
            foreach (var student in Filter) { student.ListEnrolled = liststudent; }

            return Filter.ToList();
        }
    }
}
