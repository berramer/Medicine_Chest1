﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ENTITIES
{
    public class MEDICINE : EntityBase
    {
       public string Name { get; set; }
        public DateTime ExpireDate { get; set; }
        public double BasisWeight { get; set; }
        public string Producer { get; set; }

    }
}
