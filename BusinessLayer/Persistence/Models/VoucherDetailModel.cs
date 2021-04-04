using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class VoucherDetailModel
    {
        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string VouNo { get; set; }
        public int ItemSr { get; set; }
        public string AccountId { get; set; }
        public string DrCr { get; set; }
        public decimal Amount { get; set; }
        public string VouDetail { get; set; }
        public string SortAccountBy { get; set; }
        public string OrderBy { get; set; } = "vouNo";
        public virtual AccountMasterModel AccountMaster { get; set; }
        public virtual VoucherMasterModel VoucherMaster { get; set; }
    }
}
