using System;
using System.Collections.Generic;

namespace HR_Medical_Records_Management_System.Models;

public partial class MedicalRecordType
{
    public int MedicalRecordTypeId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<TMedicalRecord> TMedicalRecords { get; set; } = new List<TMedicalRecord>();
}
