using System;

namespace SmartBase.BusinessLayer.Core.Domain
{
    public class Ledger
    {
        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string VouNo { get; set; }
        public DateTime? VouDate { get; set; }
        public string TrxType { get; set; }
        public string BilChq { get; set; }
        public int ItemSr { get; set; }
        public string AccountId { get; set; }
        public string DrCr { get; set; }
        public decimal? Amount { get; set; }
        public string CorrAccountId { get; set; }
        public string VouDetail { get; set; }

        public virtual VoucherMaster VoucherMaster { get; set; }
    }
}
