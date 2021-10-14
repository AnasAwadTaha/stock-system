using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PharmacySysyem.Models
{
    public class ItemCardMaster
    {
        public long ItemCardMasterID { get; set; }
        [StringLength(100)]
        [Display(Name = "Item")]
        public string ItemName { get; set; }
        [Display(Name = "Country")]
        public int CountryID { get; set; }
        [Display(Name = "Supplier")]
        public int SupplierID { get; set; }
        [Display(Name = "Classification")]
        public int ClassificationID { get; set; }
        public string CommName { get; set; }
        public bool Status { get; set; }
        public string Barcode { get; set; }
        public virtual Supplier Suppliers { get; set; }
        public virtual Country Countrys { get; set; }
        public virtual Classification Classifications { get; set; }
        public virtual ICollection<ItemCardDetalis> ItemCardDetaliss { get; set; }

    }
}