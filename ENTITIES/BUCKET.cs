using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES
{
   public class BUCKET:EntityBase
    {
        public string MedicineId { get; set; }
        public string UserId { get; set; }
        public decimal Price { get; set; }
        public int Item { get; set; }

    }
}
