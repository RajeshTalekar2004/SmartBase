using System.Collections.Generic;

namespace SmartBase.BusinessLayer.Core.Domain
{
    public class CgstMaster
    {
        public CgstMaster()
        {
            VoucherMasters = new HashSet<VoucherMaster>();
        }

        public int CgstId { get; set; }
        public string CgstDetail { get; set; }
        public decimal CgstRate { get; set; }

        public virtual ICollection<VoucherMaster> VoucherMasters { get; set; }
    }
}
