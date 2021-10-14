using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacySysyem.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string fax { get; set; }
        public string Resp1 { get; set; }
        public string PhoneResp1 { get; set; }
        public string Resp2 { get; set; }
        public string PhoneResp2 { get; set; }
        public bool CheckStop { get; set; }
    }
}