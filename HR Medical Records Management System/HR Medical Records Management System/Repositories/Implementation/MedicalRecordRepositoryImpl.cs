using AutoMapper;
using HR_Medical_Records_Management_System.Context;
using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;
using HR_Medical_Records_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HR_Medical_Records_Management_System.Repositories.Implementation
{
    /// <summary>
    /// Repository implementation for managing medical records in the database.
    /// This class provides methods for creating, updating, deleting (soft delete), 
    /// and querying medical records with various filters.
    /// It interacts with the HRMedicalRecordsContext to perform these operations.
    /// </summary>

    public class MedicalRecordRepositoryImpl : IMedicalRecordRepository
    {
        private readonly HRMedicalRecordsContext _context;

        public MedicalRecordRepositoryImpl(HRMedicalRecordsContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Adds a new medical record to the database.
        /// </summary>
        /// <param name="entity">The new medical record to be created.</param>
        /// <returns>Returns the created medical record after it is saved in the database.</returns>
        /// <exception cref="DbUpdateException">Thrown if an error occurs while saving changes to the database.</exception>
        public async Task<TMedicalRecord> CreateAsync(TMedicalRecord entity)
        {
            await _context.TMedicalRecords.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        /// <summary>
        /// Performs a logical deletion of a medical record by marking it as deleted.
        /// This method updates the record to set its 'IsDeleted' flag to true, 
        /// rather than physically removing it from the database.
        /// </summary>
        /// <param name="recordToDelete">The medical record to be marked as deleted.</param>
        /// <returns>Returns the updated medical record after the logical deletion.</returns>
        /// <exception cref="DbUpdateException">Thrown if an error occurs while saving changes to the database.</exception>
        public async Task<TMedicalRecord> DeleteAsync(TMedicalRecord recordToDelete)
        {

            _context.TMedicalRecords.Update(recordToDelete);
            await _context.SaveChangesAsync();

            return recordToDelete;
        }


        /// <summary>
        /// Retrieves a medical record by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the medical record to retrieve.</param>
        /// <returns>Returns the medical record with the specified ID, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the provided ID is null.</exception>
        public async Task<TMedicalRecord> GetByIdAsync(int id)
        {
            return await _context.TMedicalRecords.FindAsync(id);
        }


        /// <summary>
        /// Retrieves a filtered list of medical records based on provided criteria such as status, date range, and record type.
        /// </summary>
        /// <param name="filtersDto">An object containing the filter criteria for the query.</param>
        /// <returns>Returns a tuple containing the filtered list of medical records and the total count of records matching the criteria.</returns>
        /// <exception cref="ArgumentException">Thrown if the filters provided are invalid.</exception>
        public async Task<(List<TMedicalRecord>, int totalRows)> GetFilteredListAsync(MedicalRecordsFiltersDto filtersDto)
        {
            IQueryable<TMedicalRecord> query = _context.Set<TMedicalRecord>().AsNoTracking();

            if(filtersDto.statusId.HasValue)
            {
                query = query.Where(x => x.StatusId.Equals(filtersDto.statusId));
            }
            if (filtersDto.startDate.HasValue && filtersDto.endDate.HasValue)
            {
                query = query.Where(x => x.StartDate >= filtersDto.startDate.Value &&
                           (x.EndDate == null || x.EndDate <= filtersDto.endDate.Value));
            }
            else if (filtersDto.startDate.HasValue)
            {
                query = query.Where(x => x.StartDate >= filtersDto.startDate.Value);
            }
            else if (filtersDto.endDate.HasValue)
            {
                query = query.Where(x => x.EndDate <= filtersDto.endDate.Value);
            }
            if (filtersDto.medicalRecordTypeId.HasValue)
            {
                query = query.Where(x => x.MedicalRecordTypeId.Equals(filtersDto.medicalRecordTypeId));
            }
            
            int totalRows = await query.CountAsync();

            query = query.OrderBy(x => x.MedicalRecordId);

            var lMedicalRecords = await query.Skip((filtersDto.page - 1)*filtersDto.pageSize)
                .Take(filtersDto.pageSize)
                .ToListAsync();

            return (lMedicalRecords,totalRows);
        }


        /// <summary>
        /// Updates an existing medical record in the database.
        /// </summary>
        /// <param name="entity">The medical record entity with updated data to be saved.</param>
        /// <returns>Returns the updated medical record after saving changes to the database.</returns>
        /// <exception cref="DbUpdateException">Thrown if an error occurs while saving changes to the database.</exception>
        public async Task<TMedicalRecord> UpdateAsync(TMedicalRecord entity)
        {
            _context.TMedicalRecords.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
