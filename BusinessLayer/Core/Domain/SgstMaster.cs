using System.Collections.Generic;

#nullable disable

namespace SmartBase.BusinessLayer.Core.Domain
{
    public class SgstMaster
    {
        public SgstMaster()
        {
            VoucherMasters = new HashSet<VoucherMaster>();
        }

        public int SgstId { get; set; }
        public string SgstDetail { get; set; }
        public decimal SgstRate { get; set; }

        public virtual ICollection<VoucherMaster> VoucherMasters { get; set; }
    }
}
