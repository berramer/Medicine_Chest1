using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicine_Chest.Models
{
    public class ResetPasswordModel
    {
        public string userId { get; set; }
        public string token { get; set; }
        public string password { get; set; }
    }
}
