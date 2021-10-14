using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PharmacySysyem.Models
{
    public class OpenningBalanceD
    {
        public long ID { get; set; }
        public int stockID { get; set; }
        public int PatchID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expert Date")]
        public DateTime ExpertDate { get; set; }
        public int UnitID { get; set; }
        public int Qty { get; set; }
        public long OpenningBalanceMID { get; set; }

        public virtual Stock Stocks { get; set; }
        public virtual Unit Units { get; set; }

    }
}