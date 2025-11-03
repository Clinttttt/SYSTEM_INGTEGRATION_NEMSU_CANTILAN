using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDto;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.Interface
{
    public interface IStudentRecordCommand
    {

        Task<PersonalInformationDto?> AddPersonalDetailsAsync(PersonalInformation personalInformation);

        Task<PersonalInformation?> UpdatePersonalInformationAsync(PersonalInformationDto personalInformation);
        Task<PersonalInformationDto?> DisplayPersonalInformationAsync(Guid StudentId);
        
        Task<AcademicInformationDto?> AddAcademicInformationAsync(AcademicInformation academicInformation);

        Task<AcademicInformation?> UpdateAcademicInformationAsync(AcademicInformationDto academicInformation);

        Task<AcademicInformationDto?> DisplayAcademicInformation(Guid Student);

        Task<ContactInformationDto?> AddContactInformationAsync(ContactInformation contactInformation);

        Task<ContactInformation?> UpdateContactInformationAsync(ContactInformationDto contactInformation);
        Task<ContactInformationDto?> DisplayContactInformationAsync(Guid Student);
        Task<SchoolIdDto?> StudentSchoolIdAsync(Guid StudentId, string SchoolId);

        Task<StudentUpdateInformationDto?> UpdateAllDetailsAsync(StudentUpdateInformationDto studentUpdate);
        Task<bool> StudentSaveInformationAsync(Guid StudentId);
        Task<MiniDisplayMenuDto?> MiniDisplayMenuAsync(Guid StudentId);
    }
}
