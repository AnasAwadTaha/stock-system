using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PharmacySysyem.Models;
using System.ComponentModel.DataAnnotations;

namespace PharmacySysyem.Models
{
    public class PurchaseOrderM
    {
        public long ID { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        public Decimal Sum { get; set; }
        public Decimal Discount { get; set; }
        public Decimal NetValue { get; set; }
        public string Remark { get; set; }
        public bool Confirmed { get; set; }
        public virtual Supplier Suppliers { get; set; }
        public virtual ICollection<PurchaseOrderD> PurOrD { get; set; }
    }
}