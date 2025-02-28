using AutoMapper;
using HR_Medical_Records_Management_System.Dtos.Request;
using HR_Medical_Records_Management_System.Models;
using HR_Medical_Records_Management_System.Repositories.Interfaces;
using HR_Medical_Records_Management_System.Responses;
using HR_Medical_Records_Management_System.Services.Interface;
using HR_Medical_Records_Management_System.Validators;

namespace HR_Medical_Records_Management_System.Services.Implementation
{
    /// <summary>
    /// Service implementation for managing medical records.
    /// This service provides methods for creating, updating, deleting, and retrieving medical records.
    /// It also validates the input data using various DTO validators before performing any actions.
    /// </summary>
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


        /// <summary>
        /// Deletes a medical record from the database with a soft delete mechanism, 
        /// marking the record as deleted instead of physically removing it.
        /// Validates the provided delete DTO and handles any errors that might occur during the deletion process.
        /// </summary>
        /// <param name="deleteDto">The data transfer object containing the information of the medical record to be deleted.</param>
        /// <returns>Returns a <see cref="BaseResponse{TMedicalRecord}"/> with the status of the deletion process.</returns>
        public async Task<BaseResponse<TMedicalRecord>> DeleteAsync(DeleteMedicalRecordDto deleteDto)
        {
            var validate = _DeleteValidator.Validate(deleteDto);
            if(!validate.IsValid)
            {
                return BaseResponse<TMedicalRecord>.BadRequestResponse(validate.Errors.ToString());
            }

            TMedicalRecord deleted = await _medicalRecordRepository.GetByIdAsync(deleteDto.MedicalRecordId);
            if (deleted == null) 
            {
                return BaseResponse<TMedicalRecord>.NotFoundResponse();
            }
            else if (deleted.Status.Equals(2))
            {
                return BaseResponse<TMedicalRecord>.BadRequestResponse("Tried to Delete a DeletedRecord");
            }

            try
            {
                TMedicalRecord data = await _medicalRecordRepository.DeleteAsync(_mapper.Map(deleteDto, deleted));
                return BaseResponse<TMedicalRecord>.SuccessResponse(data, 1);
            }
            catch (Exception e)
            {
                return BaseResponse<TMedicalRecord>.ErrorResponse(e.Message);
            }



        }
        /// <summary>
        /// Retrieves a medical record by its unique identifier.
        /// Validates the provided ID and handles any errors that may arise during retrieval.
        /// </summary>
        /// <param name="id">The unique identifier of the medical record to retrieve.</param>
        /// <returns>Returns a <see cref="BaseResponse{TMedicalRecord}"/> with the requested medical record, or a not found error if not found.</returns>
        public async Task<BaseResponse<TMedicalRecord>> GetByIdAsync(int id)
        {
            if(id == 0)
            {
                return BaseResponse<TMedicalRecord>.BadRequestResponse("Id can't be 0");
            }

            try
            {
                TMedicalRecord data = await _medicalRecordRepository.GetByIdAsync(id);
                if (data == null)
                {
                    return BaseResponse<TMedicalRecord>.NotFoundResponse();
                }
                return BaseResponse<TMedicalRecord>.SuccessResponse(data, 1);
            }
            catch(Exception e)
            {
                return BaseResponse<TMedicalRecord>.ErrorResponse(e.Message);
            }
        }


        /// <summary>
        /// Retrieves a list of medical records based on the provided filters.
        /// Validates the filters and handles any errors during the filtering process.
        /// </summary>
        /// <param name="filters">The filter criteria used to query medical records.</param>
        /// <returns>Returns a <see cref="BaseResponse{List{TMedicalRecord}}"/> with the filtered medical records and the total count.</returns>
        public async Task<BaseResponse<List<TMedicalRecord>>> GetFilteredListAsync(MedicalRecordsFiltersDto filters)
        {
            var validate = _FilterValidator.Validate(filters);
            if (!validate.IsValid)
            {
                return BaseResponse<List<TMedicalRecord>>.BadRequestResponse(validate.Errors.ToString());
            }

            try
            {
                var data = await _medicalRecordRepository.GetFilteredListAsync(filters);

                return BaseResponse<List<TMedicalRecord>>.SuccessResponse(data.Item1, data.Item2);
            }
            catch (Exception e)
            {
                return BaseResponse<List<TMedicalRecord>>.ErrorResponse(e.Message);
            }
        }


        /// <summary>
        /// Creates a new medical record in the database.
        /// Validates the provided DTO and handles any errors during the creation process.
        /// </summary>
        /// <param name="createDto">The data transfer object containing the information of the new medical record.</param>
        /// <returns>Returns a <see cref="BaseResponse{TMedicalRecord}"/> with the created medical record.</returns>
        public async Task<BaseResponse<TMedicalRecord>> PostAsync(PostMedicalRecordDto createDto)
        {
            var validate = _PostValidator.Validate(createDto);
            if (!validate.IsValid)
            {
                return BaseResponse<TMedicalRecord>.BadRequestResponse(validate.Errors.ToString());
            }

            try
            {
                TMedicalRecord data = await _medicalRecordRepository.CreateAsync(_mapper.Map<TMedicalRecord>(createDto));
                return BaseResponse<TMedicalRecord>.SuccessResponse(data, 1);
            }
            catch(Exception e)
            {
                return BaseResponse<TMedicalRecord>.ErrorResponse(e.Message);
            }
        }


        /// <summary>
        /// Updates an existing medical record in the database.
        /// Validates the provided DTO and handles any errors during the update process.
        /// </summary>
        /// <param name="updateDto">The data transfer object containing the updated information of the medical record.</param>
        /// <returns>Returns a <see cref="BaseResponse{TMedicalRecord}"/> with the updated medical record.</returns>
        public async Task<BaseResponse<TMedicalRecord>> PutAsync(UpdateMedicalRecordDto updateDto)
        {
            var validate = _UpdateValidator.Validate(updateDto);
            if (!validate.IsValid)
            {
                return BaseResponse<TMedicalRecord>.BadRequestResponse(validate.Errors.ToString());
            }

            TMedicalRecord data = await _medicalRecordRepository.GetByIdAsync(updateDto.MedicalRecordId);
            if (data == null)
            {
                return BaseResponse<TMedicalRecord>.NotFoundResponse();
            }
            if (data.MedicalRecordId.Equals(2))
            {
                return BaseResponse<TMedicalRecord>.BadRequestResponse("Tried to Update a DeletedRecord");
            }

            try
            {
                TMedicalRecord updated = await _medicalRecordRepository.UpdateAsync(_mapper.Map(updateDto,data));
                return BaseResponse<TMedicalRecord>.SuccessResponse(updated, 1);
            }
            catch (Exception e) { return BaseResponse<TMedicalRecord>.ErrorResponse(e.Message); }

        }
    }
}
