using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Domain.DTOs.Faculty_RecordDtos;

namespace SYSTEM_INGTEGRATION_NEMSU.Application.Interface
{
    public interface IFacultyRecordCommand
    {
        Task<FacultyRecordDto?> AddFacultyInformationAsync(FacultyRecordDto details);
        Task<FacultyRecordDto?> UpdateFacultyInformationAsync(FacultyRecordDto details);
        Task<FacultyRecordDto?> DisplayFacultyDetailsAsync(Guid FacultyId);
        Task<FacultyPhotoId?> FacultyPhotoIDAsync(Guid FacultyId);
    }
}
