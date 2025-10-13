using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Faculty_Record;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories
{
    public class FacultyRecordCommand(ApplicationDbContext context) : IFacultyRecordCommand
    {
        public async Task<FacultyRecordDto?> AddFacultyInformationAsync(FacultyRecordDto details)
        {
            var finduser = await context.users.FindAsync(details.FacultyId);
            if (finduser is null) return null;


            var filterpersonal = details.FacultyPersonalInformationDto.Adapt<FacultyPersonalInformation>();
            if (filterpersonal is null) return null;
            filterpersonal.FacultyId = finduser.Id;
            context.facultyPersonalInformation.Add(filterpersonal);

            var filteracademic = details.FacultyAcademicInformationDto.Adapt<FacultyAcademicInformation>();
            if (filteracademic is null) return null;
            filteracademic.FacultyId = finduser.Id;
            context.facultyAcademics.Add(filteracademic);

            await context.SaveChangesAsync();

            var filterpersonaldto = filterpersonal.Adapt<FacultyPersonalInformationDto>();
            var filteracademicdto = filteracademic.Adapt<FacultyAcademicInformationDto>();

            return new FacultyRecordDto
            {
                FacultyId = finduser.Id,
                FacultyPersonalInformationDto = filterpersonaldto,
                FacultyAcademicInformationDto = filteracademicdto
            };
        }
        public async Task<FacultyRecordDto?> UpdateFacultyInformationAsync(FacultyRecordDto details)
        {
            var finduser = await context.users.FindAsync(details.FacultyId);
            if (finduser is null) return null;
            var personal = await context.facultyPersonalInformation.FirstOrDefaultAsync(s => s.FacultyId == finduser.Id);
            var academic = await context.facultyAcademics.FirstOrDefaultAsync(s => s.FacultyId == finduser.Id);

            if (personal is null || academic is null
                || details.FacultyAcademicInformationDto is null
                || details.FacultyPersonalInformationDto is null) return null;

            personal.FirstName = details.FacultyPersonalInformationDto.FirstName;
            personal.MiddleName = details.FacultyPersonalInformationDto.MiddleName;
            personal.LastName = details.FacultyPersonalInformationDto.LastName;
            personal.FacultyGender = details.FacultyPersonalInformationDto.FacultyGender;
            personal.DateofBirth = details.FacultyPersonalInformationDto.DateofBirth;
            personal.ContactNumber = details.FacultyPersonalInformationDto.ContactNumber;
            personal.EmailAddress = details.FacultyPersonalInformationDto.EmailAddress;
            personal.Address = details.FacultyPersonalInformationDto.Address;

            academic.FacultySchoolId = details.FacultyAcademicInformationDto.FacultySchoolId;
            academic.FacultyDepartment = details.FacultyAcademicInformationDto.FacultyDepartment;
            academic.Position = details.FacultyAcademicInformationDto.Position;
            academic.YearsOfTeaching = details.FacultyAcademicInformationDto.YearsOfTeaching;

            await context.SaveChangesAsync();
            var filterpersonaldto = personal.Adapt<FacultyPersonalInformationDto>();
            var filteracademicdto = academic.Adapt<FacultyAcademicInformationDto>();
            return new FacultyRecordDto
            {
                FacultyId = finduser.Id,
                FacultyPersonalInformationDto = filterpersonaldto,
                FacultyAcademicInformationDto = filteracademicdto,
            };
        }
        public async Task<FacultyRecordDto?> DisplayFacultyDetailsAsync(Guid FacultyId)
        {
            var user = await context.users.FindAsync(FacultyId);
            if (user is null) return null;
            var request_personal = await context.facultyPersonalInformation.FirstOrDefaultAsync(s => s.FacultyId == user.Id);
            var request_academic = await context.facultyAcademics.FirstOrDefaultAsync(s => s.FacultyId == user.Id);
            if (request_personal is null || request_academic is null) return null;

            var filterpersonaldto = request_personal.Adapt<FacultyPersonalInformationDto>();
            var filteracademicdto = request_academic.Adapt<FacultyAcademicInformationDto>();
            return new FacultyRecordDto
            {
                FacultyId = user.Id,
                FacultyPersonalInformationDto = filterpersonaldto,
                FacultyAcademicInformationDto = filteracademicdto
            };
        }

    }
}
