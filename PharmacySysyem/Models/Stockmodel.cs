using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacySysyem.Models
{
    public class Stockmodel
    {
        [Key]
        public int stockId { get; set; }

        public string StockName { get; set; }

        public decimal StockPrice { get; set; }
    }
}