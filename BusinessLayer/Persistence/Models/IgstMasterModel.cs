using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class IgstMasterModel
    {
        public IgstMasterModel()
        {
            VoucherMasters = new HashSet<VoucherMasterModel>();
        }

        public int IgstId { get; set; }
        public string IgstDetail { get; set; }
        public decimal IgstRate { get; set; }
        public string OrderBy { get; set; } = "igstId";
        public virtual ICollection<VoucherMasterModel> VoucherMasters { get; set; }
    }
}
