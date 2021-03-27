using SmartBase.BusinessLayer.Core.Repositories;
using System;

namespace SmartBase.BusinessLayer.Core
{
    public interface IUnitOfWork: IDisposable
    {
        IAccountMasterRepository AccountMasters { get; }
        IBillDetailRepository BillDetails { get; }
        IBillMasterRepository BillMasters { get; }
        ICgstMasterRepository CgstMasters { get; }
        ICompanyRepository Companies { get; }
        ILedgerRepository Ledgers { get; }
        ISgstMasterRepository SgstMasters { get; }
        IIgstMasterRepository IgstMasters { get; }
        ITransactionMasterRepository TransactionMasters { get; }
        ITypeMasterRepository TypeMasters { get; }
        IUserRepository Users { get; }
        IVoucherDetailRepository VoucherDetails { get; }
        IVoucherMasterRepository VoucherMasters { get; }
        
        //Task<int> Complete();
    }
}
