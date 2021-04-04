using System;
using System.Collections.Generic;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class CompanyModel
    {
        public CompanyModel()
        {
            VoucherMasters = new HashSet<VoucherMasterModel>();
        }

        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string Name { get; set; }
        public DateTime YearBegin { get; set; }
        public DateTime YearEnd { get; set; }
        public string TaxId { get; set; }
        public string AutoVoucher { get; set; }
        public string BillMatch { get; set; }
        public string Address { get; set; }
        public string OrderBy { get; set; } = "CompCode";

        public virtual ICollection<VoucherMasterModel> VoucherMasters { get; set; }

    }
}
