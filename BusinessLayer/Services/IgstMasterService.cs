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
    public class IgstMasterService: IIgstMasterService
    {
        public IgstMasterService(SmartAccountContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public SmartAccountContext _context { get; }
        private readonly IMapper _mapper;

        public async Task<ServiceResponseModel<IgstMasterModel>> Add(IgstMasterModel newIgstMaster)
        {
            ServiceResponseModel<IgstMasterModel> serviceResponse = new ServiceResponseModel<IgstMasterModel>();
            IgstMaster igstMaster = _mapper.Map<IgstMaster>(newIgstMaster);
            await UnitOfWork.IgstMasters.AddAsync(igstMaster);
            await UnitOfWork.Complete();
            serviceResponse.Data = newIgstMaster;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IgstMasterModel>> Delete(int igstId)
        {
            ServiceResponseModel<IgstMasterModel> serviceResponse = new ServiceResponseModel<IgstMasterModel>();
            IgstMaster delIgstMaster = await UnitOfWork.IgstMasters.SingleOrDefaultAsync(s => s.IgstId == igstId);
            UnitOfWork.IgstMasters.Remove(delIgstMaster);
            await UnitOfWork.Complete();
            IgstMasterModel delIgstMasterModel = _mapper.Map<IgstMasterModel>(delIgstMaster);
            serviceResponse.Data = delIgstMasterModel;

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IgstMasterModel>> Edit(IgstMasterModel editIgstMasterModel)
        {
            ServiceResponseModel<IgstMasterModel> serviceResponse = new ServiceResponseModel<IgstMasterModel>();

            IgstMaster editIgstMaster = await UnitOfWork.IgstMasters.SingleOrDefaultAsync(s => s.IgstId == editIgstMasterModel.IgstId);
            _mapper.Map<IgstMasterModel, IgstMaster>(editIgstMasterModel, editIgstMaster);
            serviceResponse.Data = editIgstMasterModel;
            UnitOfWork.IgstMasters.Update(editIgstMaster);
            await UnitOfWork.Complete();

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IEnumerable<IgstMasterModel>>> GetAll()
        {
            ServiceResponseModel<IEnumerable<IgstMasterModel>> serviceResponse = new ServiceResponseModel<IEnumerable<IgstMasterModel>>();
            IEnumerable<IgstMaster> dbIgstMaster = await UnitOfWork.IgstMasters.GetAllAsync();
            IEnumerable<IgstMasterModel> igstMasterModel = _mapper.Map<IEnumerable<IgstMasterModel>>(dbIgstMaster);
            serviceResponse.Data = igstMasterModel.ToList();
            return serviceResponse;
        }

        public async Task<PagedList<IgstMaster>> GetAll(PageParams pageParams, IgstMasterModel getIgstMaster)
        {
            var query = _context.IgstMasters.AsQueryable();

            switch (getIgstMaster.OrderBy)
            {
                case "igstDetail":
                    query = query.OrderBy(c => c.IgstDetail);
                    break;
                case "igstRate":
                    query = query.OrderBy(c => c.IgstRate);
                    break;
                default:
                    query = query.OrderBy(c => c.IgstId);
                    break;
            }

            return await PagedList<IgstMaster>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }




        public async Task<ServiceResponseModel<IgstMasterModel>> GetIgstByCode(int igstId)
        {
            IgstMaster dbIgstMaster = await UnitOfWork.IgstMasters.GetIgstByCode(igstId);
            IgstMasterModel igstMasterModel = _mapper.Map<IgstMasterModel>(dbIgstMaster);
            ServiceResponseModel<IgstMasterModel> serviceResponse = new ServiceResponseModel<IgstMasterModel> { Data = igstMasterModel };
            return serviceResponse;
        }
        public UnitOfWork UnitOfWork
        {
            get { return new UnitOfWork(_context); }
        }

    }
}
