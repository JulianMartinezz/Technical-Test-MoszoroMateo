namespace HR_Medical_Records_Management_System.Dtos.Request
{
    //This class is used to manage the delete medical record dto
    public class DeleteMedicalRecordDto
    {
        //This property is used to manage the medical record id
        public int MedicalRecordId { get; set; }

        //This property is used to manage the deletion reason
        public string deletionReason { get; set; }

        //This property is used to manage who is deleting the record
        public string deletedBy { get; set; }
    }
}
