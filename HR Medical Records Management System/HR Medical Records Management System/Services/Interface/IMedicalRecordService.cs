using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;
using HR_Medical_Records_Management_System.Responses;

namespace HR_Medical_Records_Management_System.Services.Interface
{
    /// <summary>
    /// Service interface for managing medical records, extending the IBaseService interface with specific DTOs for medical records.
    /// This service provides methods for creating, updating, deleting, and querying medical records with specific filtering capabilities.
    /// </summary>
    /// <typeparam name="TMedicalRecord">The type representing a medical record entity.</typeparam>
    /// <typeparam name="int">The type of the unique identifier for medical records (usually an integer).</typeparam>
    /// <typeparam name="PostMedicalRecordDto">The type of the DTO used for creating a new medical record.</typeparam>
    /// <typeparam name="UpdateMedicalRecordDto">The type of the DTO used for updating an existing medical record.</typeparam>
    /// <typeparam name="DeleteMedicalRecordDto">The type of the DTO used for deleting a medical record.</typeparam>
    /// <typeparam name="MedicalRecordsFiltersDto">The type of the DTO used for filtering medical records.</typeparam>
    public interface IMedicalRecordService : IBaseService<TMedicalRecord,int, PostMedicalRecordDto, UpdateMedicalRecordDto, DeleteMedicalRecordDto,MedicalRecordsFiltersDto>
    {


    }
}
