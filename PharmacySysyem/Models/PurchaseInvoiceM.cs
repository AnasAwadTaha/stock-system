using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace PharmacySysyem.Models
{
    public class PurchaseInvoiceM
    {
        public long ID { get; set; }
        [Display(Name = "Purchase Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public Decimal Sum { get; set; }
        public Decimal Discount { get; set; }
        public Decimal NetValue { get; set; }
        public string Remark { get; set; }
        public ICollection<PurchaseInvoiceD> PurInvD { get; set; }
    }
}