
using Medicine_Chest.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medicine_Chest.Models.MedicineIslemleri
{
    public class MedicineIslemViewModel
    {

        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }

        [Required]
       
        public decimal BasisWeight { get; set; }

        [Required]
        public string Producer { get; set; }


        [Required]
        public decimal Price { get; set; }

        public string IslemTuru { get; set; } 
        public MedicineIslemViewModel()
        {

        }

    }
}
