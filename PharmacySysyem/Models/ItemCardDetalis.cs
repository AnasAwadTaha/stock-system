using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacySysyem.Models
{
    public class ItemCardDetalis
    {
        public long ItemCardDetalisID { get; set; }
        public int UnitID { get; set; }
        public int Package { get; set; }
        public decimal CostPrice { get; set; }
        public decimal CurrntPrice { get; set; }
        public decimal AvrgeCost { get; set; }
        public decimal SalesPrice { get; set; }
        //public decimal CostPriceAll { get; set; }
        //public decimal Distrbut { get; set; }
        public long ItemCardMasterID { get; set; }
        public virtual Unit Unit { get; set; }

    }
}