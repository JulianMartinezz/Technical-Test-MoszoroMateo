﻿using HR_Medical_Records_Management_System.Context;
using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;
using HR_Medical_Records_Management_System.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HR_Medical_Records_Management_System.Repositories.Implementation
{
    public class MedicalRecordRepositoryImpl : IMedicalRecordRepository
    {
        private readonly HRMedicalRecordsContext _context;

        public async Task<TMedicalRecord> CreateAsync(TMedicalRecord entity)
        {
            await _context.TMedicalRecords.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TMedicalRecord> DeleteAsync(DeleteMedicalRecordDto recordToDelete)
        {
            TMedicalRecord entity = await _context.TMedicalRecords.FindAsync(recordToDelete.MedicalRecordId);

            if(entity == null)
            {
                return null;
            }

            entity.DeletionDate = DateOnly.FromDateTime(DateTime.UtcNow);
            entity.DeletedBy = recordToDelete.deletedBy;
            entity.DeletionReason = recordToDelete.deletionReason;

            _context.TMedicalRecords.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TMedicalRecord> GetByIdAsync(int id)
        {
            return await _context.TMedicalRecords.FindAsync(id);
        }

        public async Task<List<TMedicalRecord>> GetListAsync()
        {
            return await _context.TMedicalRecords.ToListAsync();
        }

        public async Task<(List<TMedicalRecord>, int totalRows)> GetMedicalRecordsWithFiltersAsync(MedicalRecordsFiltersDto filtersDto)
        {
            IQueryable<TMedicalRecord> query = _context.Set<TMedicalRecord>().AsNoTracking();

            if(filtersDto.statusId.HasValue)
            {
                query = query.Where(x => x.StatusId.Equals(filtersDto.statusId));
            }
            if(filtersDto.startDate.HasValue)
            {
                query = query.Where(x => x.StartDate >= filtersDto.startDate.Value);
            }
            if (filtersDto.endDate.HasValue)
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

        public async Task<TMedicalRecord> UpdateAsync(TMedicalRecord entity)
        {
            _context.TMedicalRecords.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
