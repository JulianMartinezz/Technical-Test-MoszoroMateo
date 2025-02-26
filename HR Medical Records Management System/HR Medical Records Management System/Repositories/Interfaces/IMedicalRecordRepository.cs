using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;

namespace HR_Medical_Records_Management_System.Repositories.Interfaces
{
    public interface IMedicalRecordRepository : IRepository<TMedicalRecord, int,DeleteMedicalRecordDto>
    {
        Task<(List<TMedicalRecord>, int totalRows)> GetMedicalRecordsWithFiltersAsync(MedicalRecordsFiltersDto filtersDto);
    }
}
