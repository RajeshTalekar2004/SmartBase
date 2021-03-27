using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class TypeMasterModel
    {
        public TypeMasterModel()
        {
            VoucherMasters = new HashSet<VoucherMasterModel>();
        }

        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string TrxCd { get; set; }
        public string TrxDetail { get; set; }
        public string Prefix { get; set; }
        public int? ItemSr { get; set; }

        public virtual TransactionMasterModel TrxCdNavigation { get; set; }
        public virtual ICollection<VoucherMasterModel> VoucherMasters { get; set; }
    }
}
