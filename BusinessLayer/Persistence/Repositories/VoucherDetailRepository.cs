using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Core.Repositories;

namespace SmartBase.BusinessLayer.Persistence.Repositories
{
    public class VoucherDetailRepository : Repository<VoucherDetail>, IVoucherDetailRepository
    {
        public VoucherDetailRepository(SmartAccountContext context) : base(context)
        {
        }
        public SmartAccountContext SmartAccountContext
        {
            get { return Context as SmartAccountContext; }
        }
    }
}
