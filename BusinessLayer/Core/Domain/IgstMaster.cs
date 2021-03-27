using System.Collections.Generic;

namespace SmartBase.BusinessLayer.Core.Domain
{
    public class IgstMaster
    {
        public IgstMaster()
        {
            VoucherMasters = new HashSet<VoucherMaster>();
        }

        public int IgstId { get; set; }
        public string IgstDetail { get; set; }
        public decimal IgstRate { get; set; }

        public virtual ICollection<VoucherMaster> VoucherMasters { get; set; }
    }
}
