using System;
using System.Collections.Generic;


namespace SmartBase.BusinessLayer.Core.Domain
{
    public class CompInfo
    {
        public CompInfo()
        {
            VoucherMasters = new HashSet<VoucherMaster>();
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

        public virtual ICollection<VoucherMaster> VoucherMasters { get; set; }
    }
}
