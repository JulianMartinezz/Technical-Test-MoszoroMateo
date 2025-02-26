using HR_Medical_Records_Management_System.Models;

namespace HR_Medical_Records_Management_System.Repositories.Interfaces
{
    public interface IMedicalRecordRepository : IRepository<TMedicalRecord, int>
    {
        Task<List<TMedicalRecord>> GetMedicalRecordsWithFiltersAsync(string? statusId, DateOnly? startDate, DateOnly? endDate, int? medicalRecordTypeId, int page, int pageSize);
    }
}
