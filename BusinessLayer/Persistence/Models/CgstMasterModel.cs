using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class CgstMasterModel
    {
        public CgstMasterModel()
        {
            VoucherMasters = new HashSet<VoucherMasterModel>();
        }

        public int CgstId { get; set; }
        public string CgstDetail { get; set; }
        public decimal CgstRate { get; set; }
        public string OrderBy { get; set; } = "cgstId";
        public virtual ICollection<VoucherMasterModel> VoucherMasters { get; set; }
    }
}
