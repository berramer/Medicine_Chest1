using ENTITIES;
using ENTITIES.Dtos;
using Medicine_Chest.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicine_Chest.Models.OrderIslemleri
{
    public class OrderDetailViewModel
    {
        public string Id { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string Address { get; set; }
        public string Phonenumber { get; set; }
        public string MailAddress { get; set; }
        public string EczaneAdi { get; set; }
        public string EczaneNo { get; set; }
        public string EczaneAdres { get; set; }
        public string KargoId { get; set; }
        public string OrderDate { get; set; }
        public int IsAccepted { get; set; }

        public int IsDeliveredKargo { get; set; }
        public List<SelectListItem> KargoList { get; set; }
        public decimal? Price { get; set; }
        public List<MEDICINE> medicineList { get; set; }
        public List<string> ItemList { get; set; }
        public List<int> stockList { get; set; }
    }
}
