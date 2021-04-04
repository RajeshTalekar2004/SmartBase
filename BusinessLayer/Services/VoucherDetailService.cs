using AutoMapper;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services
{
    public class VoucherDetailService : IVoucherDetailService
    {
        public VoucherDetailService(SmartAccountContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public SmartAccountContext _context { get; }
        private readonly IMapper _mapper;


        public async Task<ServiceResponseModel<VoucherDetailModel>> Add(VoucherDetailModel newVoucherDetailModel)
        {
            ServiceResponseModel<VoucherDetailModel> serviceResponse = new ServiceResponseModel<VoucherDetailModel>();
            VoucherDetail newVoucherDetail = _mapper.Map<VoucherDetail>(newVoucherDetailModel);
            await UnitOfWork.VoucherDetails.AddAsync(newVoucherDetail);
            await UnitOfWork.Complete();
            serviceResponse.Data = newVoucherDetailModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<VoucherDetailModel>> Delete(VoucherDetailModel delVoucherDetailModel)
        {
            ServiceResponseModel<VoucherDetailModel> serviceResponse = new ServiceResponseModel<VoucherDetailModel>();
            VoucherDetail delVoucherDetail = await UnitOfWork.VoucherDetails.SingleOrDefaultAsync(t =>
                                        t.CompCode == delVoucherDetailModel.CompCode &&
                                        t.AccYear == delVoucherDetailModel.AccYear &&
                                        t.VouNo == delVoucherDetailModel.VouNo &&
                                        t.ItemSr == delVoucherDetailModel.ItemSr);
            UnitOfWork.VoucherDetails.Remove(delVoucherDetail);
            await UnitOfWork.Complete();
            serviceResponse.Data = delVoucherDetailModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<VoucherDetailModel>> Edit(VoucherDetailModel editVoucherDetailModel)
        {
            ServiceResponseModel<VoucherDetailModel> serviceResponse = new ServiceResponseModel<VoucherDetailModel>();
            VoucherDetail editVoucherDetail = await UnitOfWork.VoucherDetails.SingleOrDefaultAsync(t =>
                                        t.CompCode == editVoucherDetailModel.CompCode &&
                                        t.AccYear == editVoucherDetailModel.AccYear &&
                                        t.VouNo == editVoucherDetailModel.VouNo &&
                                        t.ItemSr == editVoucherDetailModel.ItemSr);
            _mapper.Map<VoucherDetailModel, VoucherDetail>(editVoucherDetailModel, editVoucherDetail);
            UnitOfWork.VoucherDetails.Update(editVoucherDetail);
            await UnitOfWork.Complete();
            serviceResponse.Data = editVoucherDetailModel;

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IEnumerable<VoucherDetailModel>>> GetAll(VoucherDetailModel editVoucherDetailModel)
        {
            ServiceResponseModel<IEnumerable<VoucherDetailModel>> serviceResponse = new ServiceResponseModel<IEnumerable<VoucherDetailModel>>();
            IEnumerable<VoucherDetail> getVoucherDetail = await UnitOfWork.VoucherDetails.FindAsync(t =>
                                        t.CompCode == editVoucherDetailModel.CompCode &&
                                        t.AccYear == editVoucherDetailModel.AccYear );
            IEnumerable<VoucherDetailModel> editVoucherDetailModelAll = _mapper.Map<IEnumerable<VoucherDetailModel>>(getVoucherDetail);
            serviceResponse.Data = editVoucherDetailModelAll;

            return serviceResponse;
        }

        public async Task<PagedList<VoucherDetail>> GetAll(PageParams pageParams, VoucherDetailModel getVoucherDetailModel)
        {
            var query = _context.VoucherDetails
                        .Where(a=>a.CompCode== getVoucherDetailModel.CompCode && a.AccYear == getVoucherDetailModel.AccYear) 
                        .AsQueryable();

            switch (getVoucherDetailModel.OrderBy)
            {
                case "vouNo":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.AccYear).ThenBy(c=>c.VouNo).ThenBy(c=>c.ItemSr);
                    break;
                case "accountId":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.AccYear).ThenBy(c=>c.AccountId).ThenBy(c=>c.VouNo);
                    break;
                default:
                    query = query.OrderBy(c => c.CompCode).ThenBy(c => c.AccYear).ThenBy(c => c.VouNo).ThenBy(c => c.ItemSr);
                    break;
            }

            return await PagedList<VoucherDetail>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }



        public async Task<ServiceResponseModel<IEnumerable<VoucherDetailModel>>> GetByVouNo(VoucherDetailModel getVoucherDetailModel)
        {
            ServiceResponseModel<IEnumerable<VoucherDetailModel>> serviceResponse = new ServiceResponseModel<IEnumerable<VoucherDetailModel>>();
            IEnumerable<VoucherDetail> getVoucherDetail = await UnitOfWork.VoucherDetails.FindAsync(t =>
                                        t.CompCode == getVoucherDetailModel.CompCode &&
                                        t.AccYear == getVoucherDetailModel.AccYear &&
                                        t.VouNo == getVoucherDetailModel.VouNo);
            IEnumerable<VoucherDetailModel> editVoucherDetailModelAll = _mapper.Map<IEnumerable<VoucherDetailModel>>(getVoucherDetail);
            serviceResponse.Data = editVoucherDetailModelAll;

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<VoucherDetailModel>> GetByVouNoItemSr(VoucherDetailModel getVoucherDetailModel)
        {
            ServiceResponseModel<VoucherDetailModel> serviceResponse = new ServiceResponseModel<VoucherDetailModel>();
            VoucherDetail getVoucherDetail = await UnitOfWork.VoucherDetails.SingleOrDefaultAsync(t =>
                                        t.CompCode == getVoucherDetailModel.CompCode &&
                                        t.AccYear == getVoucherDetailModel.AccYear &&
                                        t.VouNo == getVoucherDetailModel.VouNo &&
                                        t.ItemSr == getVoucherDetailModel.ItemSr );
            VoucherDetailModel editVoucherDetailModelAll = _mapper.Map<VoucherDetailModel>(getVoucherDetail);
            serviceResponse.Data = editVoucherDetailModelAll;

            return serviceResponse;
        }

        public UnitOfWork UnitOfWork
        {
            get { return new UnitOfWork(_context); }
        }
    }
}
