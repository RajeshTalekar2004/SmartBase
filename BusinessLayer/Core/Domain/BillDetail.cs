#nullable disable

namespace SmartBase.BusinessLayer.Core.Domain
{
    public class BillDetail
    {
        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string BillId { get; set; }
        public int ItemSr { get; set; }
        public string VouNo { get; set; }
        public decimal Amount { get; set; }

        public virtual BillMaster BillMaster { get; set; }
    }
}