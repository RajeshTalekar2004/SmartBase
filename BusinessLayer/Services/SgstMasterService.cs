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
    public class SgstMasterService : ISgstMasterService
    {

        public SgstMasterService(SmartAccountContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public SmartAccountContext _context { get; }
        private readonly IMapper _mapper;

        public async Task<ServiceResponseModel<SgstMasterModel>> Add(SgstMasterModel newSgstMaster)
        {
            ServiceResponseModel<SgstMasterModel> serviceResponse = new ServiceResponseModel<SgstMasterModel>();
            SgstMaster sgstMaster = _mapper.Map<SgstMaster>(newSgstMaster);
            await UnitOfWork.SgstMasters.AddAsync(sgstMaster);
            await UnitOfWork.Complete();
            serviceResponse.Data = newSgstMaster;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<SgstMasterModel>> Delete(int sgstId)
        {
            ServiceResponseModel<SgstMasterModel> serviceResponse = new ServiceResponseModel<SgstMasterModel>();
            SgstMaster delSgstMaster = await UnitOfWork.SgstMasters.SingleOrDefaultAsync(s => s.SgstId== sgstId);
            UnitOfWork.SgstMasters.Remove(delSgstMaster);
            await UnitOfWork.Complete();
            SgstMasterModel delSgstMasterModel = _mapper.Map<SgstMasterModel>(delSgstMaster);
            serviceResponse.Data = delSgstMasterModel;

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<SgstMasterModel>> Edit(SgstMasterModel editSgstMasterModel)
        {
            ServiceResponseModel<SgstMasterModel> serviceResponse = new ServiceResponseModel<SgstMasterModel>();

            SgstMaster editSgstMaster = await UnitOfWork.SgstMasters.SingleOrDefaultAsync(s => s.SgstId == editSgstMasterModel.SgstId);
            _mapper.Map<SgstMasterModel,SgstMaster>(editSgstMasterModel, editSgstMaster);
            serviceResponse.Data = editSgstMasterModel;
            UnitOfWork.SgstMasters.Update(editSgstMaster);
            await UnitOfWork.Complete();

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IEnumerable<SgstMasterModel>>> GetAll()
        {
            ServiceResponseModel<IEnumerable<SgstMasterModel>> serviceResponse = new ServiceResponseModel<IEnumerable<SgstMasterModel>>();
            IEnumerable<SgstMaster> dbSgstMaster = await UnitOfWork.SgstMasters.GetAllAsync();
            IEnumerable<SgstMasterModel> sgstMasterModel = _mapper.Map<IEnumerable<SgstMasterModel>>(dbSgstMaster);
            serviceResponse.Data = sgstMasterModel.ToList();
            return serviceResponse;
        }

        public async Task<PagedList<SgstMaster>> GetAll(PageParams pageParams, SgstMasterModel getSgstMaster)
        {
            var query = _context.SgstMasters.AsQueryable();

            switch (getSgstMaster.OrderBy)
            {
                case "sgstDetail":
                    query = query.OrderBy(c => c.SgstDetail);
                    break;
                case "sgstRate":
                    query = query.OrderBy(c => c.SgstRate);
                    break;
                default:
                    query = query.OrderBy(c => c.SgstId);
                    break;
            }

            return await PagedList<SgstMaster>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }




        public async Task<ServiceResponseModel<SgstMasterModel>> GetSgstByCode(int sgstId)
        {
            SgstMaster dbSgstMaster = await UnitOfWork.SgstMasters.GetSgstByCode(sgstId);
            SgstMasterModel sgstMasterModel = _mapper.Map<SgstMasterModel>(dbSgstMaster);
            ServiceResponseModel<SgstMasterModel> serviceResponse = new ServiceResponseModel<SgstMasterModel> { Data = sgstMasterModel };
            return serviceResponse;
        }

        public UnitOfWork UnitOfWork
        {
            get { return new UnitOfWork(_context); }
        }


    }
}
