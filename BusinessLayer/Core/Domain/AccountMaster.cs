using System.Collections.Generic;

#nullable disable

namespace SmartBase.BusinessLayer.Core.Domain
{
    public class AccountMaster
    {
        public AccountMaster()
        {
            VoucherDetails = new HashSet<VoucherDetail>();
            VoucherMasters = new HashSet<VoucherMaster>();
        }

        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string AccountId { get; set; }
        public string Name { get; set; }
        public decimal? Opening { get; set; }
        public decimal? CurDr { get; set; }
        public decimal? CurCr { get; set; }
        public decimal? Closing { get; set; }

        public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }
        public virtual ICollection<VoucherMaster> VoucherMasters { get; set; }
    }
}
