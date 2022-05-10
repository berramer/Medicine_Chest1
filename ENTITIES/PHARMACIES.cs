using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES
{
    public class PHARMACIES : EntityBase
    {

       public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
       public int IsOnDuty { get; set; }


    }
}
