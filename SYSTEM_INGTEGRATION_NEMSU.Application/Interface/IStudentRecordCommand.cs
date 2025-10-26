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
        Task<IEnumerable<PersonalInformationDto>?> DisplayPersonalInformationAsync(Guid StudentId);

        Task<AcademicInformationDto?> AddAcademicInformationAsync(AcademicInformation academicInformation);

        Task<AcademicInformation?> UpdateAcademicInformationAsync(AcademicInformationDto academicInformation);

        Task<IEnumerable<AcademicInformationDto>?> DisplayAcademicInformation(Guid Student);

        Task<ContactInformationDto?> AddContactInformationAsync(ContactInformation contactInformation);

        Task<ContactInformation?> UpdateContactInformationAsync(ContactInformationDto contactInformation);

        Task<IEnumerable<ContactInformationDto>?> DisplayContactInformationAsync(Guid Student);
        Task<SchoolIdDto?> StudentSchoolIdAsync(Guid StudentId, string SchoolId);

        Task<StudentUpdateInformationDto?> UpdateAllDetailsAsync(StudentUpdateInformationDto studentUpdate);
    }
}
