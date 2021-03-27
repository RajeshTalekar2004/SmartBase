using AutoMapper;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Persistence.PageParams;
using SmartBase.BusinessLayer.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services
{
    public class BillDetailService : IBillDetailService
    {
        public BillDetailService(SmartAccountContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public SmartAccountContext _context { get; }
        private readonly IMapper _mapper;

        public async Task<ServiceResponseModel<BillDetailModel>> Add(BillDetailModel newBillDetailModell)
        {
            ServiceResponseModel<BillDetailModel> serviceResponse = new ServiceResponseModel<BillDetailModel>();
            BillDetail billDetail = _mapper.Map<BillDetail>(newBillDetailModell);
            await UnitOfWork.BillDetails.AddAsync(billDetail);
            await UnitOfWork.Complete();
            serviceResponse.Data = newBillDetailModell;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<BillDetailModel>> Delete(BillDetailModel billBillDetailModel)
        {
            ServiceResponseModel<BillDetailModel> serviceResponse = new ServiceResponseModel<BillDetailModel>();
            BillDetail delbillBillDetail = await UnitOfWork.BillDetails.SingleOrDefaultAsync(a =>
                                                a.CompCode == billBillDetailModel.CompCode &&
                                                a.AccYear == billBillDetailModel.AccYear &&
                                                a.BillId == billBillDetailModel.BillId &&
                                                a.ItemSr == billBillDetailModel.ItemSr);
            UnitOfWork.BillDetails.Remove(delbillBillDetail);
            await UnitOfWork.Complete();
            serviceResponse.Data = billBillDetailModel;

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<BillDetailModel>> Edit(BillDetailModel editBillDetailModel)
        {
            ServiceResponseModel<BillDetailModel> serviceResponse = new ServiceResponseModel<BillDetailModel>();
            BillDetail editbillBillDetail = await UnitOfWork.BillDetails.SingleOrDefaultAsync(a =>
                                                a.CompCode == editBillDetailModel.CompCode &&
                                                a.AccYear == editBillDetailModel.AccYear &&
                                                a.BillId == editBillDetailModel.BillId &&
                                                a.ItemSr == editBillDetailModel.ItemSr);
            _mapper.Map<BillDetailModel, BillDetail>(editBillDetailModel, editbillBillDetail);
            UnitOfWork.BillDetails.Update(editbillBillDetail);
            await UnitOfWork.Complete();
            serviceResponse.Data = editBillDetailModel;

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IEnumerable<BillDetailModel>>> GetAll(BillDetailModel getBillDetailModel)
        {
            ServiceResponseModel<IEnumerable<BillDetailModel>> serviceResponse = new ServiceResponseModel<IEnumerable<BillDetailModel>>();
            IEnumerable<BillDetail> editbillBillDetail = await UnitOfWork.BillDetails.FindAsync(a =>
                                                a.CompCode == getBillDetailModel.CompCode &&
                                                a.AccYear == getBillDetailModel.AccYear );
            

            IEnumerable<BillDetailModel> billDetailModel = _mapper.Map<IEnumerable<BillDetailModel>>(editbillBillDetail);
            serviceResponse.Data = billDetailModel;
            return serviceResponse;
        }

        public async Task<PagedList<BillDetail>> GetAll(BillParams billParams)
        {
            var query = _context.BillDetails
                         .Where(a=>a.CompCode==billParams.CompCode && a.AccYear == billParams.AccYear) 
                        .AsQueryable();

            switch (billParams.OrderBy)
            {
                case "billId":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c=>c.AccYear).ThenBy(c=>c.BillId).ThenBy(c=>c.ItemSr);
                    break;
                case "vouNo":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c => c.AccYear).ThenBy(c => c.BillId).ThenBy(c => c.ItemSr);
                    break;
                default:
                    query = query.OrderBy(c => c.CompCode).ThenBy(c => c.AccYear).ThenBy(c => c.BillId).ThenBy(c => c.ItemSr);
                    break;
            }

            return await PagedList<BillDetail>.CreateAsync(query, billParams.PageNumber, billParams.PageSize);
        }


        public async Task<ServiceResponseModel<IEnumerable<BillDetailModel>>> GetBillId(BillDetailModel getBillDetailModel)
        {
            ServiceResponseModel<IEnumerable<BillDetailModel>> serviceResponse = new ServiceResponseModel<IEnumerable<BillDetailModel>>();
            IEnumerable<BillDetail> editbillBillDetail = await UnitOfWork.BillDetails.FindAsync(a =>
                                                a.CompCode == getBillDetailModel.CompCode &&
                                                a.AccYear == getBillDetailModel.AccYear &&
                                                a.BillId == getBillDetailModel.BillId);

            IEnumerable<BillDetailModel> billDetailModel = _mapper.Map<IEnumerable<BillDetailModel>>(editbillBillDetail);
            serviceResponse.Data = billDetailModel;
            return serviceResponse;
        }
        public UnitOfWork UnitOfWork
        {
            get { return new UnitOfWork(_context); }
        }
    }
}
