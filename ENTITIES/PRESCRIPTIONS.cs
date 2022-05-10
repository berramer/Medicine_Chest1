using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES
{
    public class PRESCRIPTIONS : EntityBase
    {
        public string PrescriptionCode { get; set; }
        public DateTime ValidityDate { get; set; }
        public string MedicineID { get; set; }
        public string UserID { get; set; }
    }
}
