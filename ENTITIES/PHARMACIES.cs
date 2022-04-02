using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES
{
    public class PHARMACIES : EntityBase
    {

        string Name { get; set; }
        string Address { get; set; }
        string PhoneNumber { get; set; }
        int IsOnDuty { get; set; }


    }
}
