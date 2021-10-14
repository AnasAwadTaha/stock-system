using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PharmacySysyem.Models
{
    public class OpenningBalanceM
    {
        public long ID { get; set; }
        [Display(Name = "Item Name")]
        public long ItemCardMasterID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Entry Date")]
        public DateTime Date { get; set; }
        public virtual ItemCardMaster ItemCardMasters { get; set; }
        public virtual ICollection<OpenningBalanceD> OpBaD { get; set; }
       
    }
}