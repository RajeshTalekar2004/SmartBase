using System;
using System.Collections.Generic;

namespace SmartBase.BusinessLayer.Core.Domain
{
    public class BillMaster
    {
        public BillMaster()
        {
            BillDetails = new HashSet<BillDetail>();
            VoucherMasters = new HashSet<VoucherMaster>();
        }

        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string BillId { get; set; }
        public string AccountId { get; set; }
        public DateTime BillDate { get; set; }
        public decimal Amount { get; set; }
        public decimal? Adjusted { get; set; }
        public decimal? Balance { get; set; }

        public virtual ICollection<BillDetail> BillDetails { get; set; }
        public virtual ICollection<VoucherMaster> VoucherMasters { get; set; }
    }
}
