using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacySysyem.Models
{
    public class Stock
    {
        public int ID { get; set; }
        public string StockName { get; set; }
        public bool CheckStockMain { get; set; }
        public int StockID { get; set; }
        public bool CheckStockUser { get; set; }
        public int UserID { get; set; }
        public virtual ICollection<Stock> Branchs { get; set; }
    }
}