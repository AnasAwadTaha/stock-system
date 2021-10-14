﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacySysyem.Models
{
    public class PurchaseInvoiceD
    {
        public long ID { get; set; }
        public long ItemCardMasterID { get; set; }
        public int StockID { get; set; }
        public int UnitID { get; set; }
        public int Qty { get; set; }
        public Decimal Price { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal DiscountItem { get; set; }
        public long PurchaseInvoiceMID { get; set; }
    }
}