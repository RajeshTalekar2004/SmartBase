using System;
using System.Collections.Generic;

#nullable disable

namespace SmartBase.BusinessLayer.Core.Domain
{
    public class VoucherMaster
    {
        public VoucherMaster()
        {
            Ledgers = new HashSet<Ledger>();
            VoucherDetails = new HashSet<VoucherDetail>();
        }

        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string VouNo { get; set; }
        public DateTime VouDate { get; set; }
        public string TrxType { get; set; }
        public string BilChq { get; set; }
        public string BillId { get; set; }
        public string AccountId { get; set; }
        public string DrCr { get; set; }
        public string VouDetail { get; set; }
        public decimal? VouAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public int? SgstId { get; set; }
        public decimal? SgstAmount { get; set; }
        public int? CgstId { get; set; }
        public decimal? CgstAmount { get; set; }
        public int? IgstId { get; set; }
        public decimal? IgstAmount { get; set; }
        public virtual AccountMaster AccountMaster { get; set; }
        public virtual BillMaster BillMaster { get; set; }
        public virtual CgstMaster Cgst { get; set; }
        public virtual CompInfo CompInfo { get; set; }
        public virtual SgstMaster Sgst { get; set; }
        public virtual IgstMaster Igst { get; set; }
        public virtual TypeMaster TypeMaster { get; set; }
        public virtual ICollection<Ledger> Ledgers { get; set; }
        public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }
    }
}
