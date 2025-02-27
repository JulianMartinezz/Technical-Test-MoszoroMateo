namespace HR_Medical_Records_Management_System.Dtos.Request
{
    public class UpdateMedicalRecordDto
    {
        public int MedicalRecordId { get; set; }
        public string ModifiedBy { get; set; }

        public int? FileId { get; set; }
        public string? Audiometry { get; set; }
        public string? PositionChange { get; set; }
        public string? MotherData { get; set; }
        public string? Diagnosis { get; set; }
        public string? OtherFamilyData { get; set; }
        public string? FatherData { get; set; }
        public string? ExecuteMicros { get; set; }
        public string? ExecuteExtra { get; set; }
        public string? VoiceEvaluation { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? MedicalRecordTypeId { get; set; }
        public string? Disability { get; set; }
        public string? MedicalBoard { get; set; }
        public string? DeletionReason { get; set; }
        public string? Observations { get; set; }
        public decimal? DisabilityPercentage { get; set; }
        public string? AreaChange { get; set; }

    }
}
