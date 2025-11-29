using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Application.Interface;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDto;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos.EnrollmentFormDto;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos.NewFolder;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Data;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Entities;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories
{
    public class StudentRecordCommand(ApplicationDbContext context) : IStudentRecordCommand
    {

        public async Task<PersonalInformationDto?> AddPersonalDetailsAsync(PersonalInformation personalInformation)
        {
            var IfExists = await context.personalInformation
                 .AnyAsync(s => s.StudentId == personalInformation.StudentId);
            if (IfExists) { return null; }

            var request = await context.users.FirstOrDefaultAsync(s => s.Id == personalInformation.StudentId);
            if (request is null) return null;

            context.personalInformation.Add(personalInformation);

            request.StudentsDetails = personalInformation;
            await context.SaveChangesAsync();
            var dto = personalInformation.Adapt<PersonalInformationDto>();
            return dto;
        }
        public async Task<ProfileUpdateDto?> UpdateAllDetailsAsync(ProfileUpdateDto studentUpdate)
        {
            var personal = await context.personalInformation.FirstOrDefaultAsync(s => s.StudentId == studentUpdate.StudentId);
            var academic = await context.academicInformation.FirstOrDefaultAsync(s => s.StudentId == studentUpdate.StudentId);
            var contact = await context.contactInformation.FirstOrDefaultAsync(s => s.StudentId == studentUpdate.StudentId);
            if (personal is null || academic is null || contact is null) { return null; }

            personal.FirstName = studentUpdate?.FirstName;
            personal.MiddleName = studentUpdate?.MiddleName;
            personal.LastName = studentUpdate?.LastName;
            personal.DateOfBirth = studentUpdate?.DateOfBirth;
            personal.Gender = studentUpdate!.Gender;
            personal.CivilStatus = studentUpdate.CivilStatus;
            personal.Nationality = studentUpdate.Nationality;
            personal.PermanentAddress = studentUpdate.PermanentAddress;
            contact.MobileNumber = studentUpdate?.MobileNumber;
            contact.EmailAddress = studentUpdate?.EmailAddress;
            contact.EmergencyContactNumber = studentUpdate?.EmergencyContactNumber;
            academic.StudentType = studentUpdate!.StudentType;
            academic.YearLevel = studentUpdate.YearLevel;
            academic.Semester = studentUpdate.Semester;
            academic.Strand = studentUpdate.Strand;
            academic.Program = studentUpdate.Program;
            academic.StudentSchoolId = studentUpdate.StudentSchoolId;

            await context.SaveChangesAsync();
            return studentUpdate;
        }
        public async Task<EnrollmentFormDto?> UpdateEnrollmentFormAsync(EnrollmentFormDto studentUpdate )
        {
            var personal = await context.personalInformation.FirstOrDefaultAsync(s => s.StudentId == studentUpdate.StudentId);
            var academic = await context.academicInformation.FirstOrDefaultAsync(s => s.StudentId == studentUpdate.StudentId);
            var contact = await context.contactInformation.FirstOrDefaultAsync(s => s.StudentId == studentUpdate.StudentId);
            if (personal is null || academic is null || contact is null) { return null; }

            personal.FirstName = studentUpdate?.FirstName;
            personal.MiddleName = studentUpdate?.MiddleName;
            personal.LastName = studentUpdate?.LastName;
            personal.DateOfBirth = studentUpdate?.DateOfBirth;
            personal.Gender = studentUpdate!.Gender;
            personal.CivilStatus = studentUpdate.CivilStatus;
            personal.Nationality = studentUpdate.Nationality;
            personal.PermanentAddress = studentUpdate.PermanentAddress;
            contact.MobileNumber = studentUpdate?.MobileNumber;
            contact.EmailAddress = studentUpdate?.EmailAddress;  
            academic.StudentType = studentUpdate!.StudentType;
            academic.YearLevel = studentUpdate.YearLevel;
            academic.Semester = studentUpdate.Semester;
            academic.Strand = studentUpdate.Strand;
            academic.Program = studentUpdate.Program;        

            await context.SaveChangesAsync();
            return studentUpdate;
        }


        public async Task<PersonalInformation?> UpdatePersonalInformationAsync(PersonalInformationDto personalInformation)
        {

            var currentdetails = await context.personalInformation.FirstOrDefaultAsync(s => s.StudentId == personalInformation.StudentId);
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
        public async Task<PersonalInformationDto?> DisplayPersonalInformationAsync(Guid StudentId)
        {
            var details = await context.personalInformation.Where(s => s.StudentId == StudentId).FirstOrDefaultAsync();
            return details.Adapt<PersonalInformationDto>();
        }
        public async Task<AcademicInformationDto?> AddAcademicInformationAsync(AcademicInformation academicInformation)
        {
            var IfExists = await context.academicInformation
                 .AnyAsync(s => s.StudentId == academicInformation.StudentId);
            if (IfExists) { return null; }

            var request = await context.users.FirstOrDefaultAsync(s => s.Id == academicInformation.StudentId);
            if (request is null) return null;

            var details = context.academicInformation.Add(academicInformation);
            var filter = details.Adapt<AcademicInformationDto>();
            request.StudentAcademicDetails = academicInformation;
            await context.SaveChangesAsync();
            return filter;
        }
        public async Task<AcademicInformation?> UpdateAcademicInformationAsync(AcademicInformationDto academicInformation)
        {
            var currentdetails = await context.academicInformation.FirstOrDefaultAsync(s => s.StudentId == academicInformation.StudentId);
            if (currentdetails is null) return null;
            currentdetails.StudentType = academicInformation.StudentType;
            currentdetails.YearLevel = academicInformation.YearLevel;
            currentdetails.Semester = academicInformation.Semester;
            currentdetails.Program = academicInformation.Program;
         
            currentdetails.Strand = academicInformation.Strand;
            currentdetails.Savestatus = academicInformation.Savestatus;
            context.academicInformation.Update(currentdetails);
            var filter = currentdetails.Adapt<AcademicInformation>();
            await context.SaveChangesAsync();
            return filter;
        }

        public async Task<AcademicInformationDto?> DisplayAcademicInformation(Guid Student)
        {

            var details = await context.academicInformation
                .AsNoTracking()
                .Where(s => s.StudentId == Student)
                .Select(s => new AcademicInformationDto
                {
                    StudentId = s.StudentId,
                    StudentSchoolId = s.StudentSchoolId,
                    StudentType = s.StudentType,
                    YearLevel = s.YearLevel,
                    Semester = s.Semester,
                    Program = s.Program,
                    
                    Strand = s.Strand,
                    Savestatus = s.Savestatus,

                })
                .FirstOrDefaultAsync();
            return details;

        }

        public async Task<ContactInformationDto?> AddContactInformationAsync(ContactInformation contactInformation)
        {
            var IfExists = await context.contactInformation
               .AnyAsync(s => s.StudentId == contactInformation.StudentId);
            if (IfExists) { return null; }

            var request = await context.users.FirstOrDefaultAsync(s => s.Id == contactInformation.StudentId);
            if (request is null) return null;
            var details = context.contactInformation.Add(contactInformation);
            var filter = details.Adapt<ContactInformationDto>();
            request.StudentContactDetails = contactInformation;
            await context.SaveChangesAsync();
            return filter;
        }
        public async Task<ContactInformation?> UpdateContactInformationAsync(ContactInformationDto contactInformation)
        {
            var currentdetails = await context.contactInformation.FirstOrDefaultAsync(s => s.StudentId == contactInformation.StudentId);
            if (currentdetails is null) return null;
            currentdetails.MobileNumber = contactInformation.MobileNumber;
            currentdetails.EmailAddress = contactInformation.EmailAddress;
            currentdetails.EmergencyContactNumber = contactInformation.EmergencyContactNumber;
            context.contactInformation.Update(currentdetails);
            var filter = currentdetails.Adapt<ContactInformation>();
            await context.SaveChangesAsync();
            return filter;
        }
        public async Task<ContactInformationDto?> DisplayContactInformationAsync(Guid Student)
        {
            var details = await context.contactInformation.Where(s => s.StudentId == Student).FirstOrDefaultAsync();
            return details.Adapt<ContactInformationDto>();
        }

        public async Task<StudentMiniInfoDto?> StudentSchoolIdAsync(StudentMiniInfoDto data)
        {
          
            var personal = await context.personalInformation.FirstOrDefaultAsync(s => s.StudentId == data.StudentId);
            if (personal is null) return null;
            personal.Photo = data.Photo;
            personal.PhotoContentType = data.PhotoContentType;

            var request = await context.academicInformation.FirstOrDefaultAsync(s => s.StudentId == data.StudentId);
            if (request is null) return null;

            request.StudentSchoolId = data.StudentSchoolId;
            await context.SaveChangesAsync();
            return request.Adapt<StudentMiniInfoDto>();
        }
        public async Task<bool> StudentSaveInformationAsync(Guid StudentId)
        {
            var personal = await context.personalInformation.FirstOrDefaultAsync(s => s.StudentId == StudentId);
            if (personal is null) return false;
            personal.Savestatus = SaveStatus.Save;
            var academic = await context.academicInformation.FirstOrDefaultAsync(s => s.StudentId == StudentId);
            if (academic is null) return false;
            academic.Savestatus = SaveStatusAcademic.Save;
            var contact = await context.contactInformation.FirstOrDefaultAsync(s => s.StudentId == StudentId);
            if (contact is null) return false;
            contact.Savestatus = SaveStatusContact.Save;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<MiniDisplayMenuDto?> MiniDisplayMenuAsync(Guid StudentId)
        {
            var personal = await context.personalInformation
                .FirstOrDefaultAsync(s => s.StudentId == StudentId);
            if (personal is null) return null;
            var academic = await context.academicInformation
                .FirstOrDefaultAsync(s => s.StudentId == StudentId);
            if (academic is null) return null;
            var filter = new MiniDisplayMenuDto
            {
                FullName = personal.FirstName + " " + personal.MiddleName  + " " + personal.LastName,
                StudentId = academic.StudentSchoolId,
                FirstName = personal.FirstName,
            };
            return filter;
        }
        public async Task<bool> CheckInformationAsync(Guid StudentId)
        {
            var personal = await context.personalInformation.FirstOrDefaultAsync(s => s.StudentId == StudentId);
            if (personal is null) return false;
            var academic = await context.academicInformation.FirstOrDefaultAsync(s => s.StudentId == StudentId);
            if (academic is null) return false;
            var contact = await context.contactInformation.FirstOrDefaultAsync(s => s.StudentId == StudentId);
            if (contact is null) return false;
            return true;
        }
        public async Task<FacultyPhotoId?> StudentPhotoIDAsync(Guid StudentId)
        {
            var request = await context.personalInformation.FirstOrDefaultAsync(s => s.StudentId == StudentId);
            if (request is null) return null;
            return new FacultyPhotoId()
            {
                Photo = request.Photo,
                PhotoContentType = request.PhotoContentType,
            };
        }
        public async Task<string?> StudentForgotPassword(string EmailAddress)
        {
            var request = await context.contactInformation.FirstOrDefaultAsync(s=> s.EmailAddress == EmailAddress);
            if (request is null)
                return null;
         
            var generatecode = new Byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(generatecode);
            var base64 = Convert.ToBase64String(generatecode);
            return base64.Substring(0, 6);
        }
        public async Task<bool> StudentNewPassword(string Password, string EmailAdress)
        {
            var studentId = await context.contactInformation
        .FirstOrDefaultAsync(s => s.EmailAddress == EmailAdress);

            if (studentId is null)
                return false;

            var user = await context.users
                .FirstOrDefaultAsync(u => u.Id == studentId.StudentId);

            if (user is null)
                return false;

            var PasswordHasher = new PasswordHasher<User>()
                .HashPassword(user, Password);
            user.Password = PasswordHasher;
            await context.SaveChangesAsync();
            return true;
        }

    }
}
