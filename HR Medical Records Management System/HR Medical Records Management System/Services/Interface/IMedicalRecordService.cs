using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;
using HR_Medical_Records_Management_System.Responses;

namespace HR_Medical_Records_Management_System.Services.Interface
{
    public interface IMedicalRecordService : IBaseService<BaseResponse<TMedicalRecord>,int, PostMedicalRecordDto, UpdateMedicalRecordDto, DeleteMedicalRecordDto>
    {
        Task<BaseResponse<TMedicalRecord>> GetMedicalRecordsFiltered(MedicalRecordsFiltersDto filtersDto);
    }
}
