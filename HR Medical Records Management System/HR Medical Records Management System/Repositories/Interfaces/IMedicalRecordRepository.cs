using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;

namespace HR_Medical_Records_Management_System.Repositories.Interfaces
{
    /// <summary>
    /// Defines the contract for repository operations related to medical records, including basic CRUD 
    /// operations and retrieval of records with specific filters.
    /// </summary>
    public interface IMedicalRecordRepository : IBaseRepository<TMedicalRecord, int,MedicalRecordsFiltersDto>
    {

    }
}
