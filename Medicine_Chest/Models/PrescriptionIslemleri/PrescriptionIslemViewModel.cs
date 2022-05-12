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
        [Required]
        public string PrescriptionCode { get; set; }

        [Required]
        public string UserID { get; set; }
        [Required]
        public DateTime ValidityDate { get; set; }

        [Required]
        public string MedicineID { get; set; }
        [Required]

        public string IslemTuru { get; set; }
        public PrescriptionIslemViewModel()
        {

        }
    }
}
