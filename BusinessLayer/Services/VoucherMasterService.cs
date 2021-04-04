using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services
{
    public class VoucherMasterService : IVoucherMasterService
    {
        public VoucherMasterService(SmartAccountContext context, ILogger<VoucherMasterService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public SmartAccountContext _context { get; }
        private readonly IMapper _mapper;
        private readonly ILogger<VoucherMasterService> _logger;


        /// <summary>
        /// Save voucher with detail
        /// </summary>
        /// <param name="newVoucherMasterModel"></param>
        /// <returns></returns>
        public async Task<ServiceResponseModel<VoucherMasterModel>> Add(VoucherMasterModel newVoucherMasterModel)
        {

            String autoVoucher = null;
            string voucherNo = null;
            int vouno = 1;
            int itemSr = 1;
            decimal mtot = 0;
            Ledger ledger = null;
            List<Ledger> ledgersRec = new List<Ledger>();
            VoucherDetailModel voucherDetailModel = null;
            AccountMaster accountMaster = null;
            ServiceResponseModel<VoucherMasterModel> serviceResponse = new ServiceResponseModel<VoucherMasterModel>();

            try
            {
                // autovoucher?
                CompInfo compInfo = await UnitOfWork.Companies.SingleOrDefaultAsync(c => c.CompCode == newVoucherMasterModel.CompCode);
                if (null != compInfo)
                {
                    autoVoucher = compInfo.AutoVoucher;
                } else
                {
                    throw new Exception(string.Format("{0}{1}","Company object missing from table CompInfo:",newVoucherMasterModel.CompCode));
                }

                if (autoVoucher.ToUpper() == "Y")
                { 
                    //Generate auto voucher
                    TypeMaster typeMaster = await UnitOfWork.TypeMasters.SingleOrDefaultAsync(t =>
                                                   t.CompCode == newVoucherMasterModel.CompCode &&
                                                   t.AccYear == newVoucherMasterModel.AccYear &&
                                                   t.TrxCd == newVoucherMasterModel.TrxType);
                                    
                    if (null != typeMaster)
                    {
                        vouno = typeMaster.ItemSr == null ? 1 : (int) typeMaster.ItemSr + 1;
                        typeMaster.ItemSr = vouno;
                        UnitOfWork.TypeMasters.Update(typeMaster);
                    }
                    else
                    {
                        throw new Exception(string.Format("{0}{1}", "TypeMasters object missing from table TypeMasters:", newVoucherMasterModel.TrxType));
                    }
                    //padding '0' char with len
                    voucherNo = string.Format("{0}{1}", typeMaster.TrxCd, vouno.ToString().PadLeft(7, '0'));
                }
                else
                {
                    voucherNo = newVoucherMasterModel.VouNo;
                }

                //Validate Debit / Credit
                mtot = newVoucherMasterModel.DrCr == "1" ? mtot - (decimal)newVoucherMasterModel.VouAmount : mtot + (decimal)newVoucherMasterModel.VouAmount;

                //Set corresponding voucher details
                voucherDetailModel = newVoucherMasterModel.VoucherDetails.FirstOrDefault();
                if (null == voucherDetailModel)
                {
                    throw new Exception(string.Format("{0}{1}", "Incomplete transaction. Dr/Cr mismatch:", voucherNo));
                }

                //Check Dr/CR
                newVoucherMasterModel.VouNo = voucherNo;
                foreach (VoucherDetailModel items in newVoucherMasterModel.VoucherDetails)
                {
                    mtot = items.DrCr == "1" ? mtot - (decimal) items.Amount : mtot + (decimal) items.Amount;
                    items.VouNo = voucherNo;
                }

                if (mtot != 0)
                {
                    throw new Exception(string.Format("{0}{1}", "Debit/Credit total mismatch for voucher :", voucherNo));
                } 

                VoucherMaster newVoucherMaster = _mapper.Map<VoucherMaster>(newVoucherMasterModel);
                await UnitOfWork.VoucherMasters.AddAsync(newVoucherMaster);

               //First ledger record
               ledger = new Ledger {    CompCode = newVoucherMasterModel.CompCode ,
                                        AccYear = newVoucherMasterModel.AccYear,
                                        VouNo = voucherNo,
                                        VouDate = newVoucherMasterModel.VouDate,
                                        TrxType = newVoucherMasterModel.TrxType,
                                        BilChq = newVoucherMasterModel.BilChq,
                                        ItemSr = itemSr,
                                        AccountId = newVoucherMasterModel.AccountId,
                                        DrCr = newVoucherMasterModel.DrCr,
                                        Amount = newVoucherMasterModel.VouAmount,
                                        CorrAccountId = voucherDetailModel.AccountId,
                                        VouDetail = voucherDetailModel.VouDetail,
                                        VoucherMaster= newVoucherMaster   };
                ledgersRec.Add(ledger);
                
                //Remaining ledger record
                foreach (VoucherDetailModel items in newVoucherMasterModel.VoucherDetails)
                {
                    itemSr = itemSr + 1;
                    items.ItemSr = itemSr;

                    ledger = new Ledger
                    {
                        CompCode = items.CompCode,
                        AccYear = items.AccYear,
                        VouNo = voucherNo,
                        VouDate = newVoucherMasterModel.VouDate,
                        TrxType = newVoucherMasterModel.TrxType,
                        BilChq = newVoucherMasterModel.BilChq,
                        ItemSr = itemSr,
                        AccountId = items.AccountId,
                        DrCr = items.DrCr,
                        Amount = items.Amount,
                        CorrAccountId = newVoucherMasterModel.AccountId,
                        VouDetail = items.VouDetail,
                        VoucherMaster = newVoucherMaster
                    };
                    //await UnitOfWork.Ledgers.AddAsync(ledger);
                    ledgersRec.Add(ledger);
                }
                 await UnitOfWork.Ledgers.AddRangeAsync(ledgersRec);

                //Update Voucher Master accountID Balance
                accountMaster = await UnitOfWork.AccountMasters.SingleOrDefaultAsync(a =>
                                                   a.CompCode == newVoucherMasterModel.CompCode &&
                                                   a.AccYear == newVoucherMasterModel.AccYear &&
                                                   a.AccountId == newVoucherMasterModel.AccountId);
                if (newVoucherMasterModel.DrCr == "1")
                {
                    accountMaster.CurDr = accountMaster.CurDr.GetValueOrDefault()  + newVoucherMasterModel.VouAmount;
                    accountMaster.Closing = accountMaster.Opening.GetValueOrDefault() - accountMaster.CurDr.GetValueOrDefault() + accountMaster.CurCr.GetValueOrDefault();
                }
                else 
                {
                    accountMaster.CurCr = accountMaster.CurCr.GetValueOrDefault() + newVoucherMasterModel.VouAmount;
                    accountMaster.Closing = accountMaster.Opening.GetValueOrDefault() - accountMaster.CurDr.GetValueOrDefault() + accountMaster.CurCr.GetValueOrDefault();
                }
                UnitOfWork.AccountMasters.Update(accountMaster);

                foreach (VoucherDetailModel items in newVoucherMasterModel.VoucherDetails)
                {
                    accountMaster = await UnitOfWork.AccountMasters.SingleOrDefaultAsync(a =>
                                                       a.CompCode == items.CompCode &&
                                                       a.AccYear == items.AccYear &&
                                                       a.AccountId == items.AccountId);
                    if (items.DrCr == "1")
                    {
                        accountMaster.CurDr = accountMaster.CurDr.GetValueOrDefault() + items.Amount;
                        accountMaster.Closing = accountMaster.Opening.GetValueOrDefault() - accountMaster.CurDr.GetValueOrDefault() + accountMaster.CurCr.GetValueOrDefault();
                    } 
                    else 
                    {
                        accountMaster.CurCr = accountMaster.CurCr.GetValueOrDefault() + items.Amount;
                        accountMaster.Closing = accountMaster.Opening.GetValueOrDefault() - accountMaster.CurDr.GetValueOrDefault() + accountMaster.CurCr.GetValueOrDefault();
                    }
                    UnitOfWork.AccountMasters.Update(accountMaster);
                }
                await UnitOfWork.Complete();
                serviceResponse.Data = newVoucherMasterModel;
            }
            catch (Exception ex)
            {
                UnitOfWork.Dispose();
                _logger.LogError(ex.StackTrace);
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;


        }

        public async Task<ServiceResponseModel<VoucherMasterModel>> Delete(VoucherMasterModel delVoucherMasterModel)
        {
            ServiceResponseModel<VoucherMasterModel> serviceResponse = new ServiceResponseModel<VoucherMasterModel>();
            AccountMaster accountMaster = null;
            try
            {
                IEnumerable<Ledger> ledgerRecords = await UnitOfWork.Ledgers.FindAsync(t =>
                                            t.CompCode == delVoucherMasterModel.CompCode &&
                                            t.AccYear == delVoucherMasterModel.AccYear &&
                                            t.VouNo == delVoucherMasterModel.VouNo);

                foreach (Ledger ledger in ledgerRecords)
                {
                    accountMaster = await UnitOfWork.AccountMasters.SingleOrDefaultAsync(a =>
                                                       a.CompCode == ledger.CompCode &&
                                                       a.AccYear == ledger.AccYear &&
                                                       a.AccountId == ledger.AccountId);
                    if (ledger.DrCr == "1")
                    {
                        accountMaster.CurDr = accountMaster.CurDr.GetValueOrDefault() - ledger.Amount;
                        accountMaster.Closing = accountMaster.Opening.GetValueOrDefault() - accountMaster.CurDr.GetValueOrDefault() + accountMaster.CurCr.GetValueOrDefault();
                    }
                    else
                    {
                        accountMaster.CurCr = accountMaster.CurCr.GetValueOrDefault() - ledger.Amount;
                        accountMaster.Closing = accountMaster.Opening.GetValueOrDefault() - accountMaster.CurDr.GetValueOrDefault() + accountMaster.CurCr.GetValueOrDefault();
                    }
                    UnitOfWork.AccountMasters.Update(accountMaster);

                }

                VoucherMaster delVoucherMaster = await _context.VoucherMasters
                                                    .Include(vm => vm.VoucherDetails)
                                                    .Include(vm => vm.Ledgers)
                                                    .Include(vm => vm.Sgst)
                                                    .Include(vm => vm.Cgst)
                                                    .Include(vm => vm.Igst)
                                                    .Where(t =>
                                                        t.CompCode == delVoucherMasterModel.CompCode &&
                                                        t.AccYear == delVoucherMasterModel.AccYear &&
                                                        t.VouNo == delVoucherMasterModel.VouNo)
                                                    .SingleAsync();

                UnitOfWork.Ledgers.RemoveRange(delVoucherMaster.Ledgers);
                UnitOfWork.VoucherDetails.RemoveRange(delVoucherMaster.VoucherDetails);
                UnitOfWork.VoucherMasters.Remove(delVoucherMaster);
                await UnitOfWork.Complete();
                serviceResponse.Data = delVoucherMasterModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<VoucherMasterModel>> Edit(VoucherMasterModel editVoucherMasterModel)
        {
            ServiceResponseModel<VoucherMasterModel> serviceResponse = new ServiceResponseModel<VoucherMasterModel>();
            int itemSr = 1;
            decimal mtot = 0;
            Ledger ledger = null;
            List<Ledger> ledgersRec = new List<Ledger>();
            VoucherDetailModel voucherDetailModel = null;
            AccountMaster accountMaster = null;


            try
            {
                //Delete existing records
                //=======================

                IEnumerable<Ledger> ledgerRecords = await UnitOfWork.Ledgers.FindAsync(t =>
                                            t.CompCode == editVoucherMasterModel.CompCode &&
                                            t.AccYear == editVoucherMasterModel.AccYear &&
                                            t.VouNo == editVoucherMasterModel.VouNo);

                foreach (Ledger ledgerTmp in ledgerRecords)
                {
                    accountMaster = await UnitOfWork.AccountMasters.SingleOrDefaultAsync(a =>
                                                       a.CompCode == ledgerTmp.CompCode &&
                                                       a.AccYear == ledgerTmp.AccYear &&
                                                       a.AccountId == ledgerTmp.AccountId);
                    if (ledgerTmp.DrCr == "1")
                    {
                        accountMaster.CurDr = accountMaster.CurDr.GetValueOrDefault() - ledgerTmp.Amount;
                        accountMaster.Closing = accountMaster.Opening.GetValueOrDefault() - accountMaster.CurDr.GetValueOrDefault() + accountMaster.CurCr.GetValueOrDefault();
                    }
                    else
                    {
                        accountMaster.CurCr = accountMaster.CurCr.GetValueOrDefault() - ledgerTmp.Amount;
                        accountMaster.Closing = accountMaster.Opening.GetValueOrDefault() - accountMaster.CurDr.GetValueOrDefault() + accountMaster.CurCr.GetValueOrDefault();
                    }
                    UnitOfWork.AccountMasters.Update(accountMaster);

                }

                VoucherMaster delVoucherMaster = await _context.VoucherMasters
                                                    .Include(vm => vm.VoucherDetails)
                                                    .Include(vm => vm.Ledgers)
                                                    .Include(vm => vm.Sgst)
                                                    .Include(vm => vm.Cgst)
                                                    .Include(vm => vm.Igst)
                                                    .Where(t =>
                                                        t.CompCode == editVoucherMasterModel.CompCode &&
                                                        t.AccYear == editVoucherMasterModel.AccYear &&
                                                        t.VouNo == editVoucherMasterModel.VouNo)
                                                    .SingleAsync();

                UnitOfWork.Ledgers.RemoveRange(delVoucherMaster.Ledgers);
                UnitOfWork.VoucherDetails.RemoveRange(delVoucherMaster.VoucherDetails);
                UnitOfWork.VoucherMasters.Remove(delVoucherMaster);
                await UnitOfWork.Complete();

                //Create new record
                //=================
                //Validate Debit / Credit
                mtot = editVoucherMasterModel.DrCr == "1" ? mtot - (decimal)editVoucherMasterModel.VouAmount : mtot + (decimal)editVoucherMasterModel.VouAmount;

                //Set corresponding voucher details
                voucherDetailModel = editVoucherMasterModel.VoucherDetails.FirstOrDefault();
                if (null == voucherDetailModel)
                {
                    throw new Exception(string.Format("{0}{1}", "Incomplete transaction. Dr/Cr mismatch:", editVoucherMasterModel.VouNo));
                }

                //Check Dr/CR
                foreach (VoucherDetailModel items in editVoucherMasterModel.VoucherDetails)
                {
                    mtot = items.DrCr == "1" ? mtot - (decimal) items.Amount : mtot + (decimal) items.Amount;
                }

                if (mtot != 0)
                {
                    throw new Exception(string.Format("{0}{1}", "Debit/Credit total mismatch for voucher :", editVoucherMasterModel.VouNo));
                } 

                VoucherMaster newVoucherMaster = _mapper.Map<VoucherMaster>(editVoucherMasterModel);
                await UnitOfWork.VoucherMasters.AddAsync(newVoucherMaster);

               //First ledger record
               ledger = new Ledger {    CompCode = editVoucherMasterModel.CompCode ,
                                        AccYear = editVoucherMasterModel.AccYear,
                                        VouNo = editVoucherMasterModel.VouNo,
                                        VouDate = editVoucherMasterModel.VouDate,
                                        TrxType = editVoucherMasterModel.TrxType,
                                        BilChq = editVoucherMasterModel.BilChq,
                                        ItemSr = itemSr,
                                        AccountId = editVoucherMasterModel.AccountId,
                                        DrCr = editVoucherMasterModel.DrCr,
                                        Amount = editVoucherMasterModel.VouAmount,
                                        CorrAccountId = voucherDetailModel.AccountId,
                                        VouDetail = voucherDetailModel.VouDetail,
                                        VoucherMaster= newVoucherMaster   };
                ledgersRec.Add(ledger);
                
                //Remaining ledger record
                foreach (VoucherDetailModel items in editVoucherMasterModel.VoucherDetails)
                {
                    itemSr = itemSr + 1;
                    items.ItemSr = itemSr;

                    ledger = new Ledger
                    {
                        CompCode = items.CompCode,
                        AccYear = items.AccYear,
                        VouNo = editVoucherMasterModel.VouNo,
                        VouDate = editVoucherMasterModel.VouDate,
                        TrxType = editVoucherMasterModel.TrxType,
                        BilChq = editVoucherMasterModel.BilChq,
                        ItemSr = itemSr,
                        AccountId = items.AccountId,
                        DrCr = items.DrCr,
                        Amount = items.Amount,
                        CorrAccountId = editVoucherMasterModel.AccountId,
                        VouDetail = items.VouDetail,
                        VoucherMaster = newVoucherMaster
                    };
                    //await UnitOfWork.Ledgers.AddAsync(ledger);
                    ledgersRec.Add(ledger);
                }
                 await UnitOfWork.Ledgers.AddRangeAsync(ledgersRec);

                //Update Voucher Master accountID Balance
                accountMaster = await UnitOfWork.AccountMasters.SingleOrDefaultAsync(a =>
                                                   a.CompCode == editVoucherMasterModel.CompCode &&
                                                   a.AccYear == editVoucherMasterModel.AccYear &&
                                                   a.AccountId == editVoucherMasterModel.AccountId);
                if (editVoucherMasterModel.DrCr == "1")
                {
                    accountMaster.CurDr = accountMaster.CurDr.GetValueOrDefault()  + editVoucherMasterModel.VouAmount;
                    accountMaster.Closing = accountMaster.Opening.GetValueOrDefault() - accountMaster.CurDr.GetValueOrDefault() + accountMaster.CurCr.GetValueOrDefault();
                }
                else 
                {
                    accountMaster.CurCr = accountMaster.CurCr.GetValueOrDefault() + editVoucherMasterModel.VouAmount;
                    accountMaster.Closing = accountMaster.Opening.GetValueOrDefault() - accountMaster.CurDr.GetValueOrDefault() + accountMaster.CurCr.GetValueOrDefault();
                }
                UnitOfWork.AccountMasters.Update(accountMaster);

                foreach (VoucherDetailModel items in editVoucherMasterModel.VoucherDetails)
                {
                    accountMaster = await UnitOfWork.AccountMasters.SingleOrDefaultAsync(a =>
                                                       a.CompCode == items.CompCode &&
                                                       a.AccYear == items.AccYear &&
                                                       a.AccountId == items.AccountId);
                    if (items.DrCr == "1")
                    {
                        accountMaster.CurDr = accountMaster.CurDr.GetValueOrDefault() + items.Amount;
                        accountMaster.Closing = accountMaster.Opening.GetValueOrDefault() - accountMaster.CurDr.GetValueOrDefault() + accountMaster.CurCr.GetValueOrDefault();
                    } 
                    else 
                    {
                        accountMaster.CurCr = accountMaster.CurCr.GetValueOrDefault() + items.Amount;
                        accountMaster.Closing = accountMaster.Opening.GetValueOrDefault() - accountMaster.CurDr.GetValueOrDefault() + accountMaster.CurCr.GetValueOrDefault();
                    }
                    UnitOfWork.AccountMasters.Update(accountMaster);
                }
                await UnitOfWork.Complete();
                serviceResponse.Data = editVoucherMasterModel;

                serviceResponse.Data = editVoucherMasterModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<VoucherMasterModel>> GetByVouNo(VoucherMasterModel getVoucherMasterModel)
        {
            ServiceResponseModel<VoucherMasterModel> serviceResponse = new ServiceResponseModel<VoucherMasterModel>();
            try
            {
                VoucherMaster getVoucherMaster = await _context.VoucherMasters
                                                    .Include(vm => vm.VoucherDetails)
                                                    .Include(vm => vm.Ledgers)
                                                    .Include(vm => vm.Sgst)
                                                    .Include(vm => vm.Cgst)
                                                    .Include(vm => vm.Igst )
                                                    .Where(t =>
                                                        t.CompCode == getVoucherMasterModel.CompCode &&
                                                        t.AccYear == getVoucherMasterModel.AccYear &&
                                                        t.VouNo == getVoucherMasterModel.VouNo)
                                                    .SingleAsync();
                VoucherMasterModel getVoucherMasterViewModel1 = _mapper.Map<VoucherMasterModel>(getVoucherMaster);
                serviceResponse.Data = getVoucherMasterViewModel1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IEnumerable<VoucherMasterModel>>> GetByTrxType(VoucherMasterModel editVoucherMasterModel)
        {
            ServiceResponseModel<IEnumerable<VoucherMasterModel>> serviceResponse = new ServiceResponseModel<IEnumerable<VoucherMasterModel>>();
            try
            {
                IEnumerable<VoucherMaster> getVoucherMaster = await UnitOfWork.VoucherMasters.FindAsync(t =>
                                            t.CompCode == editVoucherMasterModel.CompCode &&
                                            t.AccYear == editVoucherMasterModel.AccYear &&
                                            t.TrxType== editVoucherMasterModel.TrxType);
                IEnumerable<VoucherMasterModel> getVoucherMasterViewModel = _mapper.Map<IEnumerable<VoucherMasterModel>>(getVoucherMaster);
                serviceResponse.Data = getVoucherMasterViewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponseModel<IEnumerable<AccountMasterModel>>> GetAccountByTrxId(VoucherMasterModel voucherMasterModel)
        {
            ServiceResponseModel<IEnumerable<AccountMasterModel>> serviceResponse = new ServiceResponseModel<IEnumerable<AccountMasterModel>>();
            TransactionMaster transactionMaster = null;
            IEnumerable<AccountMaster> dbAccountMasters = null;
            IEnumerable<AccountMasterModel> accountMasterModelAll = null;
            string message = string.Empty;
            try
            {
                transactionMaster = await UnitOfWork.TransactionMasters.SingleOrDefaultAsync(a =>
                                                        a.TrxId == voucherMasterModel.TrxType);

                if (null == transactionMaster)
                {
                    message = String.Format("{0}:{1} {2}", "Configuration error", "TryType ",voucherMasterModel.TrxType," not defined in TransactionMaster");
                    serviceResponse.Success = false;
                }
                else if (null != transactionMaster.AccountId1 && null != transactionMaster.AccountId2 && null != transactionMaster.AccountId3)
                {
                    dbAccountMasters = await UnitOfWork.AccountMasters.FindAsync(a =>
                                                            (a.CompCode == voucherMasterModel.CompCode &&
                                                            a.AccYear == voucherMasterModel.AccYear) &&
                                                            (a.AccountId.StartsWith(transactionMaster.AccountId1) ||
                                                            a.AccountId.StartsWith(transactionMaster.AccountId2) ||
                                                            a.AccountId.StartsWith(transactionMaster.AccountId3)));
                }
                else if (null != transactionMaster.AccountId1 && null != transactionMaster.AccountId2 )
                {
                    dbAccountMasters = await UnitOfWork.AccountMasters.FindAsync(a =>
                                                            (a.CompCode == voucherMasterModel.CompCode &&
                                                            a.AccYear == voucherMasterModel.AccYear) &&
                                                            (a.AccountId.StartsWith(transactionMaster.AccountId1) ||
                                                            a.AccountId.StartsWith(transactionMaster.AccountId2)));
                }
                else if (null != transactionMaster.AccountId1)
                {
                    dbAccountMasters = await UnitOfWork.AccountMasters.FindAsync(a =>
                                                            (a.CompCode == voucherMasterModel.CompCode &&
                                                            a.AccYear == voucherMasterModel.AccYear) &&
                                                            a.AccountId.StartsWith(transactionMaster.AccountId1));
                }
                else
                {
                    message = String.Format("{0}:{1} {2}", "Configuration error", "TryType ", voucherMasterModel.TrxType, " not defined in TransactionMaster");
                    serviceResponse.Success = false;
                }
                if (voucherMasterModel.SortAccountBy.Equals("A"))
                {
                    dbAccountMasters.OrderBy(a => a.AccountId);
                } else
                {
                    dbAccountMasters.OrderBy(a => a.Name);
                }
                
                accountMasterModelAll = _mapper.Map<IEnumerable<AccountMasterModel>>(dbAccountMasters);
                serviceResponse.Data = accountMasterModelAll;
                serviceResponse.Message = message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            finally
            {

            }


            return serviceResponse;
        }



        public async Task<PagedList<VoucherMaster>> GetAll(PageParams pageParams, VoucherMasterModel getVoucherMasterModel)
        {
            var query = _context.VoucherMasters
                        .Where(a=>a.CompCode== getVoucherMasterModel.CompCode && a.AccYear == getVoucherMasterModel.AccYear 
                        && a.TrxType == getVoucherMasterModel.TrxType) 
                        .AsQueryable();

            switch (getVoucherMasterModel.OrderBy)
            {
                case "vouNo":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.AccYear).ThenBy(c=>c.VouNo).ThenBy(c=>c.VouDate);
                    break;
                case "vouDate":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.AccYear).ThenBy(c=>c.VouDate).ThenBy(c=>c.VouNo);
                    break;
                default:
                    query = query.OrderBy(c => c.CompCode).ThenBy(c => c.AccYear).ThenBy(c => c.VouNo).ThenBy(c => c.VouDate);
                    break;
            }
            return await PagedList<VoucherMaster>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }



        public UnitOfWork UnitOfWork
        {
            get { return new UnitOfWork(_context); }
        }

    }
}
