namespace HR_Medical_Records_Management_System.Dtos.Request
{
    public class MedicalRecordsFiltersDto
    {
        public int? statusId { get; set; }
        public DateOnly? startDate { get; set; }
        public DateOnly? endDate { get; set; }
        public int? medicalRecordTypeId { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
