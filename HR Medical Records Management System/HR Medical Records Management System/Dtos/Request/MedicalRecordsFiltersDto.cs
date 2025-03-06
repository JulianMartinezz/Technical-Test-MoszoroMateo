namespace HR_Medical_Records_Management_System.Dtos.Request
{
    //This class is used to manage the medical records filters dto
    public class MedicalRecordsFiltersDto
    {
        //This property is used to filter by medical record status
        public int? statusId { get; set; }
        //This property is used to filter by medical record start date
        public DateOnly? startDate { get; set; }
        //This property is used to filter by medical record end date
        public DateOnly? endDate { get; set; }
        //This property is used to filter by medical record type
        public int? medicalRecordTypeId { get; set; }
        //this property is used to manage the page possition
        public int page { get; set; }
        //this property is used to manage the page size
        public int pageSize { get; set; }
    }
}
