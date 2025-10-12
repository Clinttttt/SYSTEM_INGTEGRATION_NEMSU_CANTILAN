using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDto;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Student_RecordDtos;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities;
using SYSTEM_INGTEGRATION_NEMSU.Domain.Entities.Student_Rcord;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.External
{
   public interface IStudentRecordApiCommand
    {
        Task<PersonalInformation?> AddPersonalDetailsAsync(PersonalInformationDto personalInformation);
        Task<PersonalInformation?> UpdatePersonalInformationAsync(PersonalInformationDto personalInformation);
        Task<IEnumerable<PersonalInformationDto>?> DisplayPersonalInformationAsync();
        Task<AcademicInformation?> AddAcademicDetailsAsync(AcademicInformationDto academicInformation);
        Task<AcademicInformation?> UpdateAcademicInformationAsync(AcademicInformationDto academicInformation);
        Task<IEnumerable<AcademicInformationDto>?> DisplayAcademicInformation();
        Task<ContactInformation?> AddContactInformationAsync(ContactInformationDto contactInformation);
        Task<ContactInformation?> UpdateContactInformationAsync(ContactInformationDto contactInformation);
        Task<IEnumerable<ContactInformationDto>?> DisplayContactInformationAsync();
    }
}
