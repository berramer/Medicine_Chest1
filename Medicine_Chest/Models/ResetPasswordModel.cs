using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Medicine_Chest.Models
{
    public class ResetPasswordModel
    {
        public string userId { get; set; }
        public string token { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("password")]
        public string RePassword { get; set; }
    }
}
