using System;
using System.Collections.Generic;
using System.Text;


namespace ENTITIES
{
    public class ORDER : EntityBase
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string Adress { get; set; }
        public string Phonenumber { get; set; }
        public string MailAddress { get; set; }

        public string PharmaciesID { get; set; }
        public string MedicineID { get; set; }
        public decimal Price { get; set; }
    }
}
