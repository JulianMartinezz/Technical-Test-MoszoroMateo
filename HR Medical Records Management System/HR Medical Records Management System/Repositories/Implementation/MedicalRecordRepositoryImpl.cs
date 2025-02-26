using HR_Medical_Records_Management_System.Context;
using HR_Medical_Records_Management_System.Models;
using HR_Medical_Records_Management_System.Repositories.Interfaces;

namespace HR_Medical_Records_Management_System.Repositories.Implementation
{
    public class MedicalRecordRepositoryImpl : IMedicalRecordRepository
    {
        private readonly HRMedicalRecordsContext _context;

        public Task<TMedicalRecord> CreateAsync(TMedicalRecord entity)
        {
            throw new NotImplementedException();
        }

        public Task<TMedicalRecord> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TMedicalRecord> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TMedicalRecord>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<TMedicalRecord>> GetMedicalRecordsWithFiltersAsync(string? statusId, DateOnly? startDate, DateOnly? endDate, int? medicalRecordTypeId, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<TMedicalRecord> UpdateAsync(TMedicalRecord entity)
        {
            throw new NotImplementedException();
        }
    }
}
