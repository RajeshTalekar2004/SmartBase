using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class LederReportDetailModel
    {
        public virtual AccountMasterModel AccountHeader { get; set; }
        public virtual List<LedgerDetalItemModel> AccountLedger { get; set; }
        public virtual AccountMasterModel AccountFooter { get; set; }
    }
}
