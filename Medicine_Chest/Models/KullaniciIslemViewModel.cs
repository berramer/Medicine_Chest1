using Medicine_Chest.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medicine_Chest.Models
{
    public class KullaniciIslemViewModel
    {

        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
     
        public string IdentificationNo { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public  string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public  string Email { get; set; }

        [Required]
        public  string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public  string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string RePassword { get; set; }
        public string IslemTuru { get; set; }
   
    public KullaniciIslemViewModel()
        {

        }

  
    }
}
