using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES
{
    public class MEDICINE : EntityBase
    {
       public string Name { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal BasisWeight { get; set; }
        public decimal Price { get; set; }
        public string Producer { get; set; }

    }
}
