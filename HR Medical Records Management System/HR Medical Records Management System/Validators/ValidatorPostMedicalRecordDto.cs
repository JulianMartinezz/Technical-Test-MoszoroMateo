using FluentValidation;
using HR_Medical_Records_Management_System.Dtos.Request;

namespace HR_Medical_Records_Management_System.Validators
{
    public class ValidatorPostMedicalRecordDto : AbstractValidator<PostMedicalRecordDto>
    {
        //This rule is used to validate the PostMedicalRecordDto properties
        public ValidatorPostMedicalRecordDto()
        {

            //and ensure that it is not null, not empty and is higher than 0
            RuleFor(dto=>dto.FileId)
                .NotNull().WithMessage("File ID must not be null")
                .NotEqual(0).WithMessage("File Id must be higher than 0");


            //and ensure that it is not null, not empty and does not exceed the maximum number of characters allowed
            RuleFor(dto=>dto.Diagnosis)
                .NotNull().WithMessage("Diagnosis must not be null")
                .MaximumLength(100).WithMessage("has exceeded the maximum number of characters allowed");


            //this rule is to check if the medicalRecordTypeId is either 1 or 2 (Actual Data in DB) and not null
            RuleFor(dto => dto.MedicalRecordTypeId)
                .NotNull().WithMessage("medicalRecordTypeId must not be null")
                .Must(id => id == 1 || id == 2).WithMessage("medicalRecordTypeId must be 1 or 2");


            //ensure that it is not null
            RuleFor(dto => dto.CreatedBy)
                .NotNull().WithMessage("CreatedBy must not be null");

 
            //ensure that it is not null and is not a future date and is less than or equal to the EndDate
            RuleFor(dto => dto.StartDate)
                .NotNull().WithMessage("StartDate must not be null")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("startDate cannot be a future Date")
                .LessThanOrEqualTo(dto => dto.EndDate).WithMessage("startDate cannot be later than endDate").When(dto=>dto.EndDate.HasValue);


            //ensure that it is not null and is not a future date and is greater than or equal to the StartDate
            RuleFor(dto => dto.EndDate).GreaterThanOrEqualTo(dto => dto.StartDate).When(dto=>dto.EndDate.HasValue).WithMessage("endDate cannot be earlier than startDate")
               .GreaterThan(DateOnly.FromDateTime(DateTime.UtcNow)).When(dto => dto.EndDate.HasValue).WithMessage("endDate cannot be a past Date");


            //ensure that is either YES or NO
            RuleFor(dto=>dto.Audiometry)
                .Must(str=>str.ToUpper().Equals("Y") || str.ToUpper().Equals("N")).When(src=>src.Audiometry !=null).WithMessage("Audiometry must be either Y or N");


            //ensure that is either YES or NO
            RuleFor(dto=>dto.PositionChange)
                .Must(str => str.ToUpper().Equals("Y") || str.ToUpper().Equals("N")).When(src => src.PositionChange != null).WithMessage("PositionChange must be either Y or N");


            //ensure that it does not exceed the maximum number of characters allowed
            RuleFor(dto=>dto.MotherData).
                MaximumLength(2000).When(src => src.MotherData != null).WithMessage("has exceeded the maximum number of characters allowed");


            //ensure that it does not exceed the maximum number of characters allowed
            RuleFor(dto=>dto.OtherFamilyData)
                .MaximumLength(2000).When(src => src.OtherFamilyData != null).WithMessage("has exceeded the maximum number of characters allowed");


            //ensure that it does not exceed the maximum number of characters allowed
            RuleFor(dto=>dto.FatherData)
                .MaximumLength(2000).When(src => src.FatherData != null).WithMessage("has exceeded the maximum number of characters allowed");


            //ensure that is either YES or NO
            RuleFor(dto => dto.ExecuteMicros)
                .Must(str => str.ToUpper().Equals("Y") || str.ToUpper().Equals("N")).When(src => src.ExecuteMicros != null).WithMessage("ExecuteMicros must be either Y or N");


            //ensure that is either YES or NO
            RuleFor(dto=> dto.ExecuteExtra)
                .Must(str => str.ToUpper().Equals("Y") || str.ToUpper().Equals("N")).When(src => src.ExecuteExtra != null).WithMessage("ExecuteExtra must be either Y or N");


            //ensure that is either YES or NO
            RuleFor(dto=>dto.VoiceEvaluation).
                Must(str => str.ToUpper().Equals("Y") || str.ToUpper().Equals("N")).When(src => src.VoiceEvaluation != null).WithMessage("VoiceEvaluation must be either Y or N");


            //ensure that is either YES or NO
            RuleFor(dto=>dto.Disability)
                .Must(str => str.ToUpper().Equals("Y") || str.ToUpper().Equals("N")).When(src => src.Disability != null).WithMessage("Disability must be either Y or N");


            //ensure that it does not exceed the maximum number of characters allowed
            RuleFor(dto=>dto.MedicalBoard)
                .MaximumLength(200).When(src => src.MedicalBoard != null).WithMessage("has exceeded the maximum number of characters allowed");


            //ensure that it is not null, not empty when PositionChange is YES and does not exceed the maximum number of characters allowed
            RuleFor(dto=>dto.Observations)
                .NotNull().WithMessage("Observations must not be null").When(dto => dto.PositionChange.ToUpper().Equals("Y"))
                .NotEmpty().WithMessage("Observations must not be empty").When(dto => dto.PositionChange.ToUpper().Equals("Y"))
                .MaximumLength(2000).When(src => src.Observations != null).WithMessage("has exceeded the maximum number of characters allowed");


            //ensure that it is not null, not empty when Disability is YES and is higher than 0 and less than or equal to 100
            RuleFor(dto=>dto.DisabilityPercentage)
                .Null().When(dto => dto.Disability.ToUpper().Equals("N")||dto.Disability == null)
                .NotNull().WithMessage("DisabilityPercentage must not be null").When(dto => dto.Disability != null && dto.Disability.ToUpper().Equals("Y"))
                .NotEmpty().WithMessage("DisabilityPercentage must not be empty").When(dto => dto.Disability != null && dto.Disability.ToUpper().Equals("Y"))
                .GreaterThan(0).WithMessage("DisabilityPercentage must be higher than 0").When(dto => dto.Disability != null && dto.Disability.ToUpper().Equals("Y"))
                .LessThanOrEqualTo(100).WithMessage("DisabilityPercentage must be less than or equal to 100").When(dto => dto.Disability != null && dto.Disability.ToUpper().Equals("Y"));


            //ensure that is either YES or NO
            RuleFor(dto => dto.AreaChange)
                .Must(str => str.ToUpper().Equals("Y") || str.ToUpper().Equals("N")).When(src => src.AreaChange != null).WithMessage("AreaChange must be either Y or N");
        }
    }
}
