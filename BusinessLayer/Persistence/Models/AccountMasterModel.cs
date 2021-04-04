using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class AccountMasterModel
    {
        public AccountMasterModel()
        {
            VoucherDetails = new HashSet<VoucherDetailModel>();
            VoucherMasters = new HashSet<VoucherMasterModel>();
        }

        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string AccountId { get; set; }
        public string Name { get; set; }
        public decimal? Opening { get; set; }
        public decimal? CurDr { get; set; }
        public decimal? CurCr { get; set; }
        public decimal? Closing { get; set; }
        public string OrderBy { get; set; } = "accountId";

        public virtual ICollection<VoucherDetailModel> VoucherDetails { get; set; }
        public virtual ICollection<VoucherMasterModel> VoucherMasters { get; set; }
    }
}
