namespace SmartBase.BusinessLayer.Core.Domain
{
    public class VoucherDetail
    {
        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string VouNo { get; set; }
        public int ItemSr { get; set; }
        public string AccountId { get; set; }
        public string DrCr { get; set; }
        public decimal Amount { get; set; }
        public string VouDetail { get; set; }

        public virtual AccountMaster AccountMaster { get; set; }
        public virtual VoucherMaster VoucherMaster { get; set; }
    }
}
