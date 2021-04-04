using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class BillMasterModel
    {
        public BillMasterModel()
        {
            BillDetails = new HashSet<BillDetailModel>();
            VoucherMasters = new HashSet<VoucherMasterModel>();
        }

        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string BillId { get; set; }
        public string AccountId { get; set; }
        public DateTime BillDate { get; set; }
        public decimal Amount { get; set; }
        public decimal? Adjusted { get; set; }
        public decimal? Balance { get; set; }
        public string OrderBy { get; set; } = "billId";
        public virtual ICollection<BillDetailModel> BillDetails { get; set; }
        public virtual ICollection<VoucherMasterModel> VoucherMasters { get; set; }
    }
}
