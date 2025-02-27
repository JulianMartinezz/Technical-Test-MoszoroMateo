using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;
using HR_Medical_Records_Management_System.Repositories.Interfaces;
using HR_Medical_Records_Management_System.Responses;
using HR_Medical_Records_Management_System.Services.Interface;

namespace HR_Medical_Records_Management_System.Services.Implementation
{
    public class MedicalRecordServiceImpl : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public MedicalRecordServiceImpl(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public Task<BaseResponse<TMedicalRecord>> DeleteAsync(DeleteMedicalRecordDto deleteDto)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<TMedicalRecord>> GetByIdAsync(int id)
        { 
            try{
                TMedicalRecord dataValue = await _medicalRecordRepository.GetByIdAsync(id);
                List<TMedicalRecord> data = new List<TMedicalRecord>();
                data.Add(dataValue);
                BaseResponse<TMedicalRecord> response = new BaseResponse<TMedicalRecord>(data,"Registro Encontrado",200,1);
                return response;
            }
            catch (KeyNotFoundException e)
            {
                BaseResponse<TMedicalRecord> response = new BaseResponse<TMedicalRecord>("Registro no encontrado", 404, e.Message);
                return response;
            }
            catch (Exception e)
            {
                BaseResponse<TMedicalRecord> response = new BaseResponse<TMedicalRecord>("Error interno del servidor", 500, e.Message);
                return response;
            }
        }

        public async Task<BaseResponse<TMedicalRecord>> GetListAsync()
        {

            try
            {
                List<TMedicalRecord> data = await _medicalRecordRepository.GetListAsync();
                BaseResponse<TMedicalRecord> response = new BaseResponse<TMedicalRecord>(data,"Registros Encontrados",200,data.Count);
                return response;
            }
            catch (KeyNotFoundException e)
            {
                BaseResponse<TMedicalRecord> response = new BaseResponse<TMedicalRecord>("Registro no encontrado", 404, e.Message);
                return response;
            }
            catch (Exception e)
            {
                BaseResponse<TMedicalRecord> response = new BaseResponse<TMedicalRecord>("Error interno del servidor", 500, e.Message);
                return response;
            }
        }

        public Task<BaseResponse<TMedicalRecord>> GetMedicalRecordsFiltered(MedicalRecordsFiltersDto filtersDto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<TMedicalRecord>> PostAsync(PostMedicalRecordDto createDto)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<TMedicalRecord>> PutAsync(UpdateMedicalRecordDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
