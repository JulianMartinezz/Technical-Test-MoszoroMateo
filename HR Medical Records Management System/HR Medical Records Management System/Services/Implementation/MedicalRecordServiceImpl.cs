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
                _mapper.Map(deleteDto, deleted);
                return BaseResponse<TMedicalRecord>.SuccessResponse(deleted, 1);
            }
            catch (Exception e)
            {
                return BaseResponse<TMedicalRecord>.ErrorResponse(e.Message);
            }



        }

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

        public async Task<BaseResponse<List<TMedicalRecord>>> GetFilteredListAsync(MedicalRecordsFiltersDto filters)
        {
            var validate = _FilterValidator.Validate(filters);
            if (!validate.IsValid)
            {
                return BaseResponse<List<TMedicalRecord>>.BadRequestResponse(validate.Errors.ToString());
            }

            try
            {
                var data = await _medicalRecordRepository.GetMedicalRecordsWithFiltersAsync(filters);

                return BaseResponse<List<TMedicalRecord>>.SuccessResponse(data.Item1, data.Item2);
            }
            catch (Exception e)
            {
                return BaseResponse<List<TMedicalRecord>>.ErrorResponse(e.Message);
            }
        }

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
