using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PharmacySysyem.Models
{
    public class ReceiptPurchaseM
    {
        public long ID { get; set; }
        [Display(Name = "Receipt Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReceiptDate { get; set; }
        public long PurchaseOrderMsID { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public Decimal Sum { get; set; }
        public Decimal Discount { get; set; }
        public Decimal NetValue { get; set; }
        public string Remark { get; set; }
        public bool Confirmed { get; set; }
        public virtual ICollection<ReceiptPurchaseD> RecPurD { get; set; }
    }
}