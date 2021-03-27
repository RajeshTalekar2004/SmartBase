using SmartBase.BusinessLayer.Core.Domain;
using System.Collections.Generic;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class LedgerReportModel
    {
        public virtual CompInfo Company { get; set; }
        public virtual List<LederReportDetailModel> LederReportDetailModel { get; set; }
    }
}
