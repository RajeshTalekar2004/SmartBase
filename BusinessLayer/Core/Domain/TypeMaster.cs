using System.Collections.Generic;

namespace SmartBase.BusinessLayer.Core.Domain
{
    public class TypeMaster
    {
        public TypeMaster()
        {
            VoucherMasters = new HashSet<VoucherMaster>();
        }

        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string TrxCd { get; set; }
        public string TrxDetail { get; set; }
        public string Prefix { get; set; }
        public int? ItemSr { get; set; }

        public virtual TransactionMaster TrxCdNavigation { get; set; }
        public virtual ICollection<VoucherMaster> VoucherMasters { get; set; }
    }
}
