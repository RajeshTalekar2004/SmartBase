using SmartBase.BusinessLayer.Core;
using SmartBase.BusinessLayer.Core.Repositories;
using SmartBase.BusinessLayer.Persistence.Repositories;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly SmartAccountContext _context;

        public UnitOfWork(SmartAccountContext context)
        {
            _context = context;
            AccountMasters = new AccountMasterRepository(_context);
            BillDetails = new BillDetailRepository(_context);
            BillMasters = new BillMasterRepository(_context);
            CgstMasters = new CgstMasterRepository(_context);
            Companies = new CompanyRepository(_context);
            Ledgers = new LedgerRepository(_context);
            SgstMasters = new SgstMasterRepository(_context);
            IgstMasters = new IgstMasterRepository(_context);
            TransactionMasters = new TransactionMasterRepository(_context);
            TypeMasters = new TypeMasterRepository(_context);
            Users = new UserRepository(_context);
            VoucherDetails = new VoucherDetailRepository(_context);
            VoucherMasters = new VoucherMasterRepository(_context);
        }

        public IAccountMasterRepository AccountMasters { get; private set; }
        public IBillDetailRepository BillDetails  { get; private set; }
        public IBillMasterRepository BillMasters  { get; private set; }
        public ICgstMasterRepository CgstMasters { get; private set; }
        public ICompanyRepository Companies { get; private set; }
        public ILedgerRepository Ledgers  { get; private set; }
        public ISgstMasterRepository SgstMasters { get; private set; }
        public IIgstMasterRepository IgstMasters { get; private set; }
        public ITransactionMasterRepository TransactionMasters { get; private set; }
        public ITypeMasterRepository TypeMasters  { get; private set; }
        public IUserRepository Users { get; private set; }
        public IVoucherDetailRepository VoucherDetails  { get; private set; }
        public IVoucherMasterRepository VoucherMasters  { get; private set; }    

        public async Task<int> Complete()
        {
           return await _context.SaveChangesAsync();
            
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        //Task<int> IUnitOfWork.Complete()
        //{
        //    return  _context.SaveChangesAsync();
        //}
    }
}

