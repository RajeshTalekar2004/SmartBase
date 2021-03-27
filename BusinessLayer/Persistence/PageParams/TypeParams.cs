namespace SmartBase.BusinessLayer.Persistence.PageParams
{
    public class TypeParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public string SearchBy { get; set; }

        public string OrderBy { get; set; } = "TrxCd";
    }
}
