using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PharmacySysyem.DAL;
using System.Net;

namespace PharmacySysyem.Services
{
    public class ServiceClass : Controller
    {
        //private PharmacyContext db = new PharmacyContext();
        //Get Small Unit To insert Into Transaction
        public static long SumQty = 0;
        public static long ConvertToSmallUnit(long ItemID, int UnitID, int Qty)
        {
            PharmacyContext db = new PharmacyContext();
            //Load All ItemCardDetalis To Check Package Value
            var CheckUnit = db.ItemCardDetaliss.Where(I => I.ItemCardMasterID == ItemID).ToList();
            var SmallUnit = CheckUnit.LastOrDefault().UnitID;
            var BigUnit = CheckUnit.First().UnitID;
            //check Unit Small Or Not 
            if (UnitID == SmallUnit)
            {
                SumQty =  Qty;
            }
            if (UnitID == BigUnit)
            {
                SumQty = CheckUnit.Last().Package * Qty;
            }
            return SumQty;
        }
        public static long ConvertToBigUnit(int ItemID, int UnitID, int Qty)
        {
            PharmacyContext db = new PharmacyContext();
            //check Unit Small Or Not 
            var CheckUnit = db.ItemCardDetaliss.Where(I => I.ItemCardMasterID == ItemID).ToList();
            if (CheckUnit.Count >= 1)
            {
                var package = CheckUnit.Last().Package;
                SumQty = Qty / package;
            }
            return SumQty;
        }
    }
}