using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class SgstMasterModel
    {
        public SgstMasterModel()
        {
            VoucherMasters = new HashSet<VoucherMasterModel>();
        }

        public int SgstId { get; set; }
        public string SgstDetail { get; set; }
        public decimal SgstRate { get; set; }

        public virtual ICollection<VoucherMasterModel> VoucherMasters { get; set; }
    }
}
