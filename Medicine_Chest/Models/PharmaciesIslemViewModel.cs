
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
        [Required]
        public string Name { get; set; }




        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

     

 

       
         public int? IsOnDuty { get; set; }
        //public string Password { get; set; }


        public string IslemTuru { get; set; }

        public PharmaciesIslemViewModel()
        {

        }


    }
}
