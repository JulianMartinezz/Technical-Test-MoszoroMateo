using FluentValidation;
using HR_Medical_Records_Management_System.Dtos.Request;

namespace HR_Medical_Records_Management_System.Validators
{
    public class ValidatorUpdateMedicalRecordDto : AbstractValidator<UpdateMedicalRecordDto>
    {
        //This rules are used to validate the UpdateMedicalRecordDto properties
        public ValidatorUpdateMedicalRecordDto()
        {
            
            //ensure that it is not null and is higher than 0
            RuleFor(dto=>dto.MedicalRecordId)
                .NotEmpty().WithMessage("Medical Record ID must not be null")
                .NotEqual(0).WithMessage("Medical Record Id must be higher than 0");


            //ensure that it is not null and not empty
            RuleFor(dto=>dto.ModifiedBy)
                .NotNull().WithMessage("ModifiedBy must not be null")
                .NotEmpty().WithMessage("ModifiedBy must not be empty");

 
            //and ensure that it is not null, not empty and is higher than 0
            RuleFor(dto => dto.FileId)
                .NotEmpty().WithMessage("File ID must not be null")
                .NotEqual(0).WithMessage("File Id must be higher than 0");


            //ensure that is either YES or NO
            RuleFor(dto => dto.Audiometry)
                .Must(str => str.ToUpper().Equals("YES") || str.ToUpper().Equals("NO")).WithMessage("Audiometry must be either YES or NO");


            //ensure that is either YES or NO
            RuleFor(dto => dto.PositionChange)
                .Must(str => str.ToUpper().Equals("YES") || str.ToUpper().Equals("NO")).WithMessage("PositionChange must be either YES or NO");


            //ensure that it does not exceed the maximum number of characters allowed
            RuleFor(dto => dto.MotherData).
               MaximumLength(2000).WithMessage("has exceeded the maximum number of characters allowed");


            //ensure that it is not empty and does not exceed the maximum number of characters allowed
            RuleFor(dto => dto.Diagnosis)
                .NotEmpty().WithMessage("Diagnosis must not be empty")
                .MaximumLength(100).WithMessage("has exceeded the maximum number of characters allowed");


            //ensure that it does not exceed the maximum number of characters allowed
            RuleFor(dto => dto.OtherFamilyData)
               .MaximumLength(2000).WithMessage("has exceeded the maximum number of characters allowed");


            //ensure that it does not exceed the maximum number of characters allowed
            RuleFor(dto => dto.FatherData)
                .MaximumLength(2000).WithMessage("has exceeded the maximum number of characters allowed");
            

            //ensure that is either YES or NO
            RuleFor(dto => dto.ExecuteMicros)
               .Must(str => str.ToUpper().Equals("YES") || str.ToUpper().Equals("NO")).WithMessage("ExecuteMicros must be either YES or NO");


            //ensure that is either YES or NO
            RuleFor(dto => dto.ExecuteExtra)
              .Must(str => str.ToUpper().Equals("YES") || str.ToUpper().Equals("NO")).WithMessage("ExecuteExtra must be either YES or NO");


            //ensure that is either YES or NO
            RuleFor(dto => dto.VoiceEvaluation).
                Must(str => str.ToUpper().Equals("YES") || str.ToUpper().Equals("NO")).WithMessage("VoiceEvaluation must be either YES or NO");


            //ensure that it is not a future date and is not later than the EndDate
            RuleFor(dto => dto.StartDate)
               .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("startDate cannot be a future Date")
               .LessThanOrEqualTo(dto => dto.EndDate).WithMessage("startDate cannot be later than endDate").When(dto => dto.EndDate.HasValue);


            //ensure that it is not a past date and is not earlier than the StartDate
            RuleFor(dto => dto.EndDate).GreaterThanOrEqualTo(dto => dto.StartDate).WithMessage("endDate cannot be earlier than startDate")
               .GreaterThan(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("endDate cannot be a past Date");

            //this rule is to check if the medicalRecordTypeId is either 1 or 2 (Actual Data in DB)
            RuleFor(dto => dto.MedicalRecordTypeId)
                .Must(id => id == 1 || id == 2).WithMessage("medicalRecordTypeId must be 1 or 2");


            //ensure that is either YES or NO
            RuleFor(dto => dto.Disability)
              .Must(str => str.ToUpper().Equals("YES") || str.ToUpper().Equals("NO")).WithMessage("Disability must be either YES or NO");


            //ensure that it does not exceed the maximum number of characters allowed
            RuleFor(dto => dto.MedicalBoard)
                .MaximumLength(200).WithMessage("has exceeded the maximum number of characters allowed");


            //ensure that it does not exceed the maximum number of characters allowed
            RuleFor(dto => dto.DeletionReason)
                .MaximumLength(2000).WithMessage("has exceeded the maximum number of characters allowed");


            //ensure that it is not null and empty when PositionChange is "YES", not empty and does not exceed the maximum number of characters allowed
            RuleFor(dto => dto.Observations)
                .NotNull().WithMessage("Observations must not be null").When(dto => dto.PositionChange.ToUpper().Equals("YES"))
                .NotEmpty().WithMessage("Observations must not be empty").When(dto => dto.PositionChange.ToUpper().Equals("YES"))
                .MaximumLength(2000).WithMessage("has exceeded the maximum number of characters allowed");


            //ensure that it is not null, not empty when Disability is "YES", higher than 0 and less than or equal to 100
            RuleFor(dto => dto.DisabilityPercentage)
                .NotNull().WithMessage("DisabilityPercentage must not be null").When(dto => dto.Disability.ToUpper().Equals("YES"))
                .NotEmpty().WithMessage("DisabilityPercentage must not be empty").When(dto => dto.Disability.ToUpper().Equals("YES"))
                .GreaterThan(0).WithMessage("DisabilityPercentage must be higher than 0").When(dto => dto.Disability.ToUpper().Equals("YES"))
                .LessThanOrEqualTo(100).WithMessage("DisabilityPercentage must be less than or equal to 100").When(dto => dto.Disability.ToUpper().Equals("YES"));


            //ensure that is either YES or NO
            RuleFor(dto => dto.AreaChange)
                .Must(str => str.ToUpper().Equals("YES") || str.ToUpper().Equals("NO")).WithMessage("AreaChange must be either YES or NO");

        }
    }
}
