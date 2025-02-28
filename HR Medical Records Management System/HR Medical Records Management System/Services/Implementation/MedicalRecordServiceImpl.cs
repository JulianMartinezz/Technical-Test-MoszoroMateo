using AutoMapper;
using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;
using HR_Medical_Records_Management_System.Repositories.Interfaces;
using HR_Medical_Records_Management_System.Responses;
using HR_Medical_Records_Management_System.Services.Interface;
using HR_Medical_Records_Management_System.Validators;

namespace HR_Medical_Records_Management_System.Services.Implementation
{
    //Service that will be used to manage the Medical Records
    public class MedicalRecordServiceImpl : IMedicalRecordService
    {
        //Objects that will be used to interact with the Service
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly ValidatorMedicalRecordsFiltersDto _FilterValidator;
        private readonly ValidatorPostMedicalRecordDto _PostValidator;
        private readonly ValidatorUpdateMedicalRecordDto _UpdateValidator;
        private readonly ValidatorDeleteMedicalRecordDto _DeleteValidator;
        private readonly IMapper _mapper;

        //Constructor that will be used to inject the dependencies
        public MedicalRecordServiceImpl(IMedicalRecordRepository medicalRecordRepository, ValidatorMedicalRecordsFiltersDto FilterValidator,
            ValidatorPostMedicalRecordDto PostValidator, ValidatorUpdateMedicalRecordDto UpdateValidator,
            ValidatorDeleteMedicalRecordDto DeleteValidator, IMapper mapper)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _FilterValidator = FilterValidator;
            _PostValidator = PostValidator;
            _UpdateValidator = UpdateValidator;
            _DeleteValidator = DeleteValidator;
            _mapper = mapper;
        }






        // Asynchronously deletes a medical record.
        // Steps:
        // 1. Validates the input data using FluentValidation.
        // 2. Checks if the record exists and if it has already been deleted.
        // 3. Attempts to delete the record using the repository.
        // 4. Handles possible exceptions, such as record not found or internal errors.
        public async Task<BaseResponse<TMedicalRecord>> DeleteAsync(DeleteMedicalRecordDto deleteDto)
        {
            var validate = _DeleteValidator.Validate(deleteDto);
            if(!validate.IsValid)
            {
                return new BaseResponse<TMedicalRecord>("Datos no validos", 400, validate.Errors.ToString()); ;
            }

            TMedicalRecord dataValue = await _medicalRecordRepository.GetByIdAsync(deleteDto.MedicalRecordId);
            if(dataValue == null)
            {
                return new BaseResponse<TMedicalRecord>("Registro no encontrado", 404, "No se encontró un registro con el ID proporcionado."); ;
            }
            if (dataValue.StatusId.Equals(2))
            {
                return new BaseResponse<TMedicalRecord>("Registro ya se encuentra eliminado", 400, "El registro ya ha sido eliminado"); ;
            }

            try
            {
                _mapper.Map(deleteDto, dataValue);

                TMedicalRecord data = await _medicalRecordRepository.DeleteAsync(dataValue);


                return new BaseResponse<TMedicalRecord>(data, "Registro Eliminado", 200, 1); ;
            }
            catch (Exception e)
            {
                return new BaseResponse<TMedicalRecord>("Error interno del servidor", 500, e.Message);
            }
        }






        // Asynchronously retrieves a medical record by its ID.
        // Steps:
        // 1. Validates that the provided ID is greater than 0.
        // 2. Attempts to fetch the record from the repository using the provided ID.
        // 3. If the record is not found, returns a response indicating the record was not found.
        // 4. If the record is found, adds it to a list and returns it in the response.
        // 5. Handles specific exceptions such as `KeyNotFoundException` and general exceptions, providing appropriate error messages.

        public async Task<BaseResponse<TMedicalRecord>> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                return new BaseResponse<TMedicalRecord>("Datos no validos", 400, "El Id del registro debe ser mayo a 0");
            }

            try
            {
                TMedicalRecord dataValue = await _medicalRecordRepository.GetByIdAsync(id);

                if (dataValue == null)
                {
                    return new BaseResponse<TMedicalRecord>("Registro no encontrado", 404, "No se encontró un registro con el ID proporcionado.");
                }



                return new BaseResponse<TMedicalRecord>(dataValue, "Registro Encontrado", 200, 1); ;
            }
            catch (KeyNotFoundException e)
            {
                return new BaseResponse<TMedicalRecord>("Registro no encontrado", 404, e.Message); ;
            }
            catch (Exception e)
            {
                return new BaseResponse<TMedicalRecord>("Error interno del servidor", 500, e.Message); ;
            }
        }









        // Asynchronously retrieves a list of medical records.
        // Steps:
        // 1. Attempts to fetch the list of records from the repository.
        // 2. If records are found, returns them in the response with a success message and the count of records.
        // 3. If no records are found or an error occurs, handles exceptions such as `KeyNotFoundException` and general exceptions, providing appropriate error messages
        public async Task<BaseResponse<List<TMedicalRecord>>> GetListAsync()
        {
            try
            {
                List<TMedicalRecord> data = await _medicalRecordRepository.GetListAsync();
                return new BaseResponse<List<TMedicalRecord>>(data, "Registros Encontrados", 200, data.Count); ;
            }
            catch (KeyNotFoundException e)
            {
                return new BaseResponse<List<TMedicalRecord>>("Registro no encontrado", 404, e.Message); ;
            }
            catch (Exception e)
            {
                return new BaseResponse<List<TMedicalRecord>>("Error interno del servidor", 500, e.Message); ;
            }
        }







        // Asynchronously retrieves medical records with filters applied.
        // Steps:
        // 1. Validates the input filters using FluentValidation.
        // 2. If the validation fails, returns a response indicating invalid data with the validation errors.
        // 3. Attempts to fetch the filtered records from the repository using the provided filters.
        // 4. If the records are found, returns them in the response with a success message and the total count.
        // 5. Handles exceptions such as `KeyNotFoundException` and general exceptions, providing appropriate error messages.
        public async Task<BaseResponse<TMedicalRecord>> GetMedicalRecordsFiltered(MedicalRecordsFiltersDto filtersDto)
        {
            var validate = _FilterValidator.Validate(filtersDto);
            if (!validate.IsValid)
            {
                return new BaseResponse<TMedicalRecord>("Datos no validos", 400, validate.Errors.ToString()); ;
            }

            try
            {
                var data = await _medicalRecordRepository.GetMedicalRecordsWithFiltersAsync(filtersDto);
                return new BaseResponse<TMedicalRecord>(data.Item1, "Registros Encontrados", 200, data.Item2); ;
            }
            catch (KeyNotFoundException e)
            {
                return new BaseResponse<TMedicalRecord>("Registros no encontrados", 404, e.Message); ;
            }
            catch (Exception e)
            {
                return new BaseResponse<TMedicalRecord>("Error interno del servidor", 500, e.Message); ;
            }
        }









        // Asynchronously creates a new medical record.
        // Steps:
        // 1. Validates the input data using FluentValidation.
        // 2. If the validation fails, returns a response indicating invalid data with the validation errors.
        // 3. Maps the input DTO to the `TMedicalRecord` entity using AutoMapper.
        // 4. Attempts to create the new medical record in the repository.
        // 5. If successful, returns the created record in the response with a success message.
        // 6. Handles exceptions such as `KeyNotFoundException` and general exceptions, providing appropriate error messages.
        public async Task<BaseResponse<TMedicalRecord>> PostAsync(PostMedicalRecordDto createDto)
        {
            var validate = _PostValidator.Validate(createDto);
            if (!validate.IsValid)
            {
                return new BaseResponse<TMedicalRecord>("Datos no validos", 400, validate.Errors.ToString()); ;
            }

            try
            {

                TMedicalRecord record = _mapper.Map<TMedicalRecord>(createDto);
                TMedicalRecord dataValue = await _medicalRecordRepository.CreateAsync(record);

                List<TMedicalRecord> data = new List<TMedicalRecord>();
                data.Add(dataValue);

                return new BaseResponse<TMedicalRecord>(data, "Registro Creado con exito", 200, 1); ;
            }
            catch (KeyNotFoundException e)
            {
                return new BaseResponse<TMedicalRecord>("Registros no encontrados", 404, e.Message); ;
            }
            catch (Exception e)
            {
                return new BaseResponse<TMedicalRecord>("Error interno del servidor", 500, e.Message); ;
            }

        }









        // Asynchronously updates an existing medical record.
        // Steps:
        // 1. Validates the input data using FluentValidation.
        // 2. If the validation fails, returns a response indicating invalid data with the validation errors.
        // 3. Attempts to retrieve the existing medical record from the repository using the provided ID.
        // 4. Checks if the record is marked as deleted (StatusId equals 2), and if so, returns a response indicating it cannot be modified.
        // 5. Maps the updated data from the DTO to the existing medical record entity.
        // 6. Attempts to update the record in the repository.
        // 7. If successful, returns the updated record in the response with a success message.
        // 8. Handles exceptions such as `KeyNotFoundException` and general exceptions, providing appropriate error messages.
        public async Task<BaseResponse<TMedicalRecord>> PutAsync(UpdateMedicalRecordDto updateDto)
        {
            var validate = _UpdateValidator.Validate(updateDto);

            if(!validate.IsValid)
            {
                return new BaseResponse<TMedicalRecord>("Datos no validos", 400, validate.Errors.ToString()); ;
            }

            TMedicalRecord dataValue = await _medicalRecordRepository.GetByIdAsync(updateDto.MedicalRecordId);
            if (dataValue.StatusId.Equals(2))
            {
                return new BaseResponse<TMedicalRecord>("Registro se encuentra eliminado", 400, "El registro no se puede modificar"); ;
            }

            try
            {
                _mapper.Map(updateDto, dataValue);
                TMedicalRecord updatedData = await _medicalRecordRepository.UpdateAsync(dataValue);

                List<TMedicalRecord> data = new List<TMedicalRecord>();
                data.Add(updatedData);


                return new BaseResponse<TMedicalRecord>(data, "Registro Actualizado con exito", 200, 1); ;
            }
            catch (KeyNotFoundException e)
            {
                return new BaseResponse<TMedicalRecord>("Registros no encontrados", 404, e.Message); ;
            }
            catch (Exception e)
            {
                return new BaseResponse<TMedicalRecord>("Error interno del servidor", 500, e.Message); ;
            }
        }
    }
}
