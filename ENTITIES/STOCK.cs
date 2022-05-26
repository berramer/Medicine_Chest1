using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES
{
    public class STOCK:EntityBase
    {
     
        public string MedicineID { get; set; }
        public string PharmID { get; set; }
        public int Stock { get; set; }

    }
}
