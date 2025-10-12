using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDto;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories
{
    public class StudentRecordCommand(ApplicationDbContext context) : IStudentRecordCommand
    {

        public async Task<PersonalInformationDto?> AddPersonalDetailsAsync(PersonalInformation personalInformation)
        {
            var request = await context.users.FirstOrDefaultAsync(s => s.Id == personalInformation.StudentId);
            if (request is null) return null;
            var details = context.personalInformation.Add(personalInformation);
            var filter = details.Adapt<PersonalInformationDto>();
            await context.SaveChangesAsync();
            return filter;
        }
        public async Task<PersonalInformation?> UpdatePersonalInformationAsync(PersonalInformationDto personalInformation)
        {
            var request = context.users.FirstOrDefault(s => s.Id == personalInformation.StudentId);
            if (request is null) return null;
            var currentdetails = await context.personalInformation.FirstOrDefaultAsync(s => s.StudentId == request.Id);
            if (currentdetails is null) return null;
            currentdetails.FirstName = personalInformation.FirstName;
            currentdetails.MiddleName = personalInformation.MiddleName;
            currentdetails.LastName = personalInformation.LastName;
            currentdetails.DateOfBirth = personalInformation.DateOfBirth;
            currentdetails.Gender = personalInformation.Gender;
            currentdetails.CivilStatus = personalInformation.CivilStatus;
            currentdetails.Nationality = personalInformation.Nationality;
            currentdetails.PermanentAddress = personalInformation.PermanentAddress;
            currentdetails.GuardianName = personalInformation.GuardianName;
            currentdetails.GuardianContact = personalInformation.GuardianContact;
            context.personalInformation.Update(currentdetails);
            await context.SaveChangesAsync();
            var filter = currentdetails.Adapt<PersonalInformation>();
            return filter;
        }
        public async Task<IEnumerable<PersonalInformationDto>?> DisplayPersonalInformationAsync(Guid StudentId)
        {
            var request = await context.users.FirstOrDefaultAsync(s => s.Id == StudentId);
            if (request is null) return null;
            var details = await context.personalInformation.Where(s => s.StudentId == request.Id).ToListAsync();
            return details.Adapt<List<PersonalInformationDto>>();
        }
        public async Task<AcademicInformationDto?> AddAcademicInformationAsync(AcademicInformation academicInformation)
        {
            var request = await context.users.FirstOrDefaultAsync(s => s.Id == academicInformation.StudentId);
            if (request is null) return null;
            var details = context.Add(academicInformation);
            var filter = details.Adapt<AcademicInformationDto>();
            await context.SaveChangesAsync();
            return filter;
        }
        public async Task<AcademicInformation?> UpdateAcademicInformationAsync(AcademicInformationDto academicInformation)
        {
            var request = await context.users.FirstOrDefaultAsync(s => s.Id == academicInformation.StudentId);
            if (request is null) return null;
            var currentdetails = await context.academicInformation.FirstOrDefaultAsync(s => s.StudentId == request.Id);
            if (currentdetails is null) return null;
            currentdetails.StudentType = academicInformation.StudentType;
            currentdetails.YearLevel = academicInformation.YearLevel;
            currentdetails.Semester = academicInformation.Semester;
            currentdetails.Program = academicInformation.Program;
            currentdetails.Major = academicInformation.Major;
            currentdetails.Strand = academicInformation.Strand;
            context.academicInformation.Update(currentdetails);
            var filter = currentdetails.Adapt<AcademicInformation>();
            await context.SaveChangesAsync();
            return filter;
        }
        public async Task<IEnumerable<AcademicInformationDto>?> DisplayAcademicInformation(Guid Student)
        {
            var request = await context.users.FirstOrDefaultAsync(s => s.Id == Student);
            if (request is null) return null;
            var details = await context.academicInformation.Where(s => s.StudentId == request.Id).ToListAsync();
            return details.Adapt<List<AcademicInformationDto>>();
        }
        public async Task<ContactInformationDto?> AddContactInformationAsync(ContactInformation contactInformation)
        {
            var request = await context.users.FirstOrDefaultAsync(s => s.Id == contactInformation.StudentId);
            if (request is null) return null;
            var details = context.contactInformation.Add(contactInformation);
            var filter = details.Adapt<ContactInformationDto>();
            await context.SaveChangesAsync();
            return filter;
        }
        public async Task<ContactInformation?> UpdateContactInformationAsync( ContactInformationDto contactInformation)
        {
            var request = await context.users.FirstOrDefaultAsync(s => s.Id == contactInformation.StudentId);
            if (request is null) return null;
            var currentdetails = await context.contactInformation.FirstOrDefaultAsync(s => s.StudentId == request.Id);
            if (currentdetails is null) return null;
            currentdetails.MobileNumber = contactInformation.MobileNumber;
            currentdetails.EmailAddress = contactInformation.EmailAddress;
            currentdetails.EmergencyContactNumber = contactInformation.EmergencyContactNumber;
            context.contactInformation.Update(currentdetails);
            var filter = currentdetails.Adapt<ContactInformation>();
            await context.SaveChangesAsync();
            return filter;         
        }
        public async Task <IEnumerable<ContactInformationDto>?> DisplayContactInformationAsync( Guid Student)
        {
            var request = await context.users.FirstOrDefaultAsync(s => s.Id == Student);
            if (request is null) return null;
            var details = await context.contactInformation.Where(s => s.StudentId == request.Id).ToListAsync();
            return details.Adapt<List<ContactInformationDto>>();
        }
    }
}
