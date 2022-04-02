using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES
{
    public class PRESCRIPTIONS : EntityBase
    {
        string PrescriptionCode { get; set; }
        DateTime ValidityDate { get; set; }
        string MedicineID { get; set; }
        string UserID { get; set; }
    }
}
