using Medicine_Chest.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medicine_Chest.Models.PrescriptionIslemleri
{
    public class PrescriptionIslemViewModel
    {

        public string Id { get; set; }
 
        public string PrescriptionCode { get; set; }

        
        public string UserID { get; set; }

        public DateTime ValidityDate { get; set; }


        public string MedicineID { get; set; }

        public string IslemTuru { get; set; }
        public PrescriptionIslemViewModel()
        {

        }
    }
}
