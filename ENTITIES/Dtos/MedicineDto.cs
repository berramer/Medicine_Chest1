using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES.Dtos
{
    public class MedicineDto
    {


        public string Name { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal BasisWeight { get; set; }
        public decimal Price { get; set; }
        public string Producer { get; set; }

        public string MedicineID { get; set; }
        public string PharmID { get; set; }
        public int Stock { get; set; }
    }
}
