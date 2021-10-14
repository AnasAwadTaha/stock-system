using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PharmacySysyem.Models;

namespace PharmacySysyem.Models
{
    public class Transaction
    {
        public long ID { get; set; }
        public int StockID { get; set; }
        public int Transactiontype { get; set; }
        public int Qty { get; set; }
        public DateTime TransactionDate { get; set; }
        public int ItemCardMasterID { get; set; }
        public int PatchNo { get; set; }
        public int UnitID { get; set; }
        public int RefID { get; set; }
     
    }
}