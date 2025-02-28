using FluentValidation;
using HR_Medical_Records_Management_System.Dtos.Request;

namespace HR_Medical_Records_Management_System.Validators
{
    public class ValidatorMedicalRecordsFiltersDto : AbstractValidator<MedicalRecordsFiltersDto>
    {
        //This class is used to validate the MedicalRecordsFiltersDto properties
        public ValidatorMedicalRecordsFiltersDto()
        {
            //This rule is to check if the statusId is either 1 or 2 (Actual Data in DB)
            RuleFor(dto=>dto.statusId)
                .Must(id=>id==1 || id ==2).When(dto=>dto.statusId.HasValue).WithMessage("statusId must be 1 or 2");

            //ensure that the startDate is not null and is less than or equal to the endDate
            RuleFor(dto=>dto.startDate).LessThanOrEqualTo(dto=>dto.endDate).When(dto => dto.startDate.HasValue).WithMessage("startDate cannot be later than endDate")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).When(dto => dto.startDate.HasValue).WithMessage("startDate cannot be a future Date");

            //ensure that the endDate is not null and is greater than or equal to the startDate
            RuleFor(dto=>dto.endDate).GreaterThanOrEqualTo(dto=>dto.startDate).When(dto => dto.endDate.HasValue).WithMessage("endDate cannot be earlier than startDate")
                .GreaterThan(DateOnly.FromDateTime(DateTime.UtcNow)).When(dto => dto.endDate.HasValue).WithMessage("endDate cannot be a past Date");

            //This rule is to check if the medicalRecordTypeId is either 1 or 2 (Actual Data in DB)
            RuleFor(dto => dto.medicalRecordTypeId).Must(id => id == 1 || id == 2).When(dto => dto.medicalRecordTypeId.HasValue).WithMessage("medicalRecordTypeId must be 1 or 2");

            //ensure that the page is not null and is greater than 0
            RuleFor(dto=>dto.page)
                .NotNull().WithMessage("page must not be null")
                .GreaterThan(0).WithMessage("page must be at less 1");

            //ensure that the pageSize is not null
            RuleFor(dto => dto.pageSize)
                .NotNull().WithMessage("pageSize must not be null");


        }
    }
}
