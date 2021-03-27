using SmartBase.BusinessLayer.Core.Domain;
using System.Collections.Generic;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class SalePurchaseReportModel
    {
        public virtual CompInfo Company { get; set; }
        public virtual List<SalePurchaseDetailModel> SalePurchaseDetails { get; set; }
        public virtual List<SalePurchaseDetailModel> SalePurchaseDetailsTotal { get; set; }
        public virtual List<SalePurchaseDetailModel> AccountHeadSummary { get; set; }
        public virtual List<SalePurchaseDetailModel> AccountHeadSummaryTotal { get; set; }
        public virtual List<SalePurchaseDetailModel> CgstHeadSummary { get; set; }
        public virtual List<SalePurchaseDetailModel> CgstHeadSummaryTotal { get; set; }
        public virtual List<SalePurchaseDetailModel> SgstHeadSummary { get; set; }
        public virtual List<SalePurchaseDetailModel> SgstHeadSummaryTotal { get; set; }
        public virtual List<SalePurchaseDetailModel> IgstHeadSummary { get; set; }
        public virtual List<SalePurchaseDetailModel> IgstHeadSummaryTotal { get; set; }

    }
}
