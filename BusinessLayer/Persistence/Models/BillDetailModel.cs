using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class BillDetailModel
    {
        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string BillId { get; set; }
        public int ItemSr { get; set; }
        public string VouNo { get; set; }
        public decimal Amount { get; set; }
        public string OrderBy { get; set; } = "billId";

        public virtual BillMasterModel BillMaster { get; set; }
    }
}