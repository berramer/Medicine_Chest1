using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES
{
    public class MEDICINE : EntityBase
    {
        string Name { get; set; }
        DateTime ExpireDate { get; set; }
        double BasisWeight { get; set; }
        string Producer { get; set; }

    }
}
