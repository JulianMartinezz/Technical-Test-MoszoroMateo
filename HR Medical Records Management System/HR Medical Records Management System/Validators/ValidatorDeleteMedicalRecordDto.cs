using FluentValidation;
using HR_Medical_Records_Management_System.Dtos.Request;

namespace HR_Medical_Records_Management_System.Validators
{
    public class ValidatorDeleteMedicalRecordDto : AbstractValidator<DeleteMedicalRecordDto>
    {
        public ValidatorDeleteMedicalRecordDto()
        {
            //This rule is used to validate the MedicalRecordId property of the DeleteMedicalRecordDto
            //and ensure that it is not null and is higher than 0
            RuleFor(dto=>dto.MedicalRecordId).NotEmpty().WithMessage("Medical Record ID must not be null")
                .NotEqual(0).WithMessage("Medical Record Id must be higher than 0");

            //This rule is used to validate the deletionReason property of the DeleteMedicalRecordDto 
            //and ensure that it is not null and does not exceed the maximum number of characters allowed
            RuleFor(dto=>dto.deletionReason).NotEmpty().WithMessage("deletion Reason must not be null")
                .MaximumLength(2000).WithMessage("has exceeded the maximum number of characters allowed");

            //This rule is used to validate the deletedBy property of the DeleteMedicalRecordDto
            RuleFor(dto=>dto.deletedBy).NotEmpty().WithMessage("deletedBy must not be null");
        }
    }
}
