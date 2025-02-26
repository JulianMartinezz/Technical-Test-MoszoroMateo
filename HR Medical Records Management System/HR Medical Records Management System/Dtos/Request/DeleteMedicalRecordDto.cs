namespace HR_Medical_Records_Management_System.Dtos.Request
{
    public class DeleteMedicalRecordDto
    {
        public int MedicalRecordId { get; set; }
        public string deletionReason { get; set; }
        public string deletedBy { get; set; }
    }
}
