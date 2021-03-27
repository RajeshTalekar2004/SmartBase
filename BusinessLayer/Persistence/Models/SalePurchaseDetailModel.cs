using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class SalePurchaseDetailModel
    {
        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string VouNo { get; set; }
        public DateTime VouDate { get; set; }
        public string TrxType { get; set; }
        public string BilChq { get; set; }
        public string BillId { get; set; }
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public string DrCr { get; set; }
        public string VouDetail { get; set; }
        public decimal? VouAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public int? SgstId { get; set; }
        public string SgstName { get; set; }
        public decimal? SgstAmount { get; set; }
        public int? CgstId { get; set; }
        public string CgstName { get; set; }
        public decimal? CgstAmount { get; set; }
        public int? IgstId { get; set; }
        public string IgstName { get; set; }
        public decimal? IgstAmount { get; set; }
    }
}
