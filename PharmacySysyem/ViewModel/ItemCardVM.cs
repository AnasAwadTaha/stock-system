using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PharmacySysyem.Models;


namespace PharmacySysyem.ViewModel
{
    public class ItemCardVM
    {
        public long ItemCardMasterID { get; set; }
        public string ItemName { get; set; }
        public int UnitID { get; set; }
        public decimal CostPrice { get; set; }
        public int ClassificationID { get; set; }
        public string ClassificationName { get; set; }
        public int ContryID { get; set; }
        public long ItemCardDetalisID { get; set; }
        public decimal CurrntPrice { get; set; }
        public decimal AvrgeCost { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal CostPriceAll { get; set; }
        public decimal Distrbut { get; set; }
        public string Barcode { get; set; }
        public int PatchNo { get; set; }
        public int Qty { get; set; }
        public DateTime TransactionDate { get; set; }
        public int RefID { get; set; }
        public int StockID { get; set; }
        public string StockName { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
    }
}