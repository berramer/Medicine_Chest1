using System;
using System.Collections.Generic;
using System.Text;


namespace ENTITIES
{
    public class CARGO : EntityBase
    {
        public string OrderId { get; set; }
        public int IsDelivered { get; set; }
        public int Score { get; set; }
        public DateTime OrderDateC { get; set; }


    }
}
