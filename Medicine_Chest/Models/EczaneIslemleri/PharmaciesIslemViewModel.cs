
using Medicine_Chest.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medicine_Chest.Models
{
    public class PharmaciesIslemViewModel
    {

        public string Id { get; set; }
        public string EczaneAdi {get;set;}
        public string Adresi {get;set;}
        public string Semt {get;set;}
        public string YolTarifi {get;set;}
        public string Telefon {get;set;}
        public string Sehir {get;set;}
        public string ilce {get;set;}
     

 

   


        public string IslemTuru { get; set; }

        public PharmaciesIslemViewModel()
        {

        }


    }
}
