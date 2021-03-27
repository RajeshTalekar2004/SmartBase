using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class VoucherMasterModel
    {
        public VoucherMasterModel()
        {
            Ledgers = new HashSet<LedgerModel>();
            VoucherDetails = new HashSet<VoucherDetailModel>();
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
        public string SortAccountBy { get; set; }

        public virtual AccountMasterModel AccountMaster { get; set; }
        public virtual BillMasterModel BillMaster { get; set; }
        public virtual CgstMasterModel Cgst { get; set; }
        public virtual CompanyModel CompInfo { get; set; }
        public virtual SgstMasterModel Sgst { get; set; }
        public virtual IgstMasterModel Igst { get; set; }
        public virtual TypeMasterModel TypeMaster { get; set; }
        public virtual ICollection<LedgerModel> Ledgers { get; set; }
        public virtual ICollection<VoucherDetailModel> VoucherDetails { get; set; }
    }
}
