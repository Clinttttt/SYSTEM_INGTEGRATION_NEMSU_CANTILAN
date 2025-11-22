using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.External
{
   public interface IFacultyRecordApi
    {
      Task<FacultyRecordDto?> AddFacultyInformationAsync(FacultyRecordDto details);
        Task<FacultyRecordDto?> UpdateFacultyInformationAsync(FacultyRecordDto details);
        Task<FacultyRecordDto?> DisplayFacultyDetailsAsync();
        Task<FacultyPhotoId?> FacultyPhotoIDAsync();
        Task<string?> ForgotPassword(string EmailAddress);
        Task<bool> NewPassword(NewPasswordDto dto);
    }
}
