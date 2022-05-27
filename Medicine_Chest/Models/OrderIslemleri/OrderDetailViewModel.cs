using ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicine_Chest.Models.OrderIslemleri
{
    public class OrderDetailViewModel
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string Address { get; set; }
        public string Phonenumber { get; set; }
        public string MailAddress { get; set; }
   
   
        public decimal? Price { get; set; }
        public List<MEDICINE> medicineList { get; set; }
    }
}
