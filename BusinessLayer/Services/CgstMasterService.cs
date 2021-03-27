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
    public class CgstMasterService : ICgstMasterService
    {

        public CgstMasterService(SmartAccountContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public SmartAccountContext _context { get; }
        private readonly IMapper _mapper;

        public async Task<ServiceResponseModel<CgstMasterModel>> Add(CgstMasterModel newCgstMaster)
        {
            ServiceResponseModel<CgstMasterModel> serviceResponse = new ServiceResponseModel<CgstMasterModel>();
            CgstMaster cgstMaster = _mapper.Map<CgstMaster>(newCgstMaster);
            await UnitOfWork.CgstMasters.AddAsync(cgstMaster);
            await UnitOfWork.Complete();
            serviceResponse.Data = newCgstMaster;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<CgstMasterModel>> Delete(int cgstId)
        {
            ServiceResponseModel<CgstMasterModel> serviceResponse = new ServiceResponseModel<CgstMasterModel>();
            CgstMaster delCgstMaster = await UnitOfWork.CgstMasters.SingleOrDefaultAsync(s => s.CgstId == cgstId);
            UnitOfWork.CgstMasters.Remove(delCgstMaster);
            await UnitOfWork.Complete();
            CgstMasterModel delCgstMasterModel = _mapper.Map<CgstMasterModel>(delCgstMaster);
            serviceResponse.Data = delCgstMasterModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<CgstMasterModel>> Edit(CgstMasterModel editCgstMasterModel)
        {
            ServiceResponseModel<CgstMasterModel> serviceResponse = new ServiceResponseModel<CgstMasterModel>();
            CgstMaster editCgstMaster = await UnitOfWork.CgstMasters.SingleOrDefaultAsync(s => s.CgstId == editCgstMasterModel.CgstId);
            _mapper.Map<CgstMasterModel, CgstMaster>(editCgstMasterModel, editCgstMaster);
            serviceResponse.Data = editCgstMasterModel;
            UnitOfWork.CgstMasters.Update(editCgstMaster);
            await UnitOfWork.Complete();
            return serviceResponse;
        }

        public async Task<PagedList<CgstMaster>> GetAll(CgstParams cgstParams)
        {
            var query = _context.CgstMasters
                        .AsQueryable();

            switch (cgstParams.OrderBy)
            {
                case "cgstDetail":
                    query = query.OrderBy(c => c.CgstDetail);
                    break;
                case "cgstRate":
                    query = query.OrderBy(c => c.CgstRate);
                    break;
                default:
                    query = query.OrderBy(c => c.CgstId);
                    break;
            }

            return await PagedList<CgstMaster>.CreateAsync(query, cgstParams.PageNumber, cgstParams.PageSize);
        }


        public async Task<ServiceResponseModel<IEnumerable<CgstMasterModel>>> GetAll()
        {
            ServiceResponseModel<IEnumerable<CgstMasterModel>> serviceResponse = new ServiceResponseModel<IEnumerable<CgstMasterModel>>();
            IEnumerable<CgstMaster> dbCgstMaster = await UnitOfWork.CgstMasters.GetAllAsync();
            IEnumerable<CgstMasterModel> sgstMasterModel = _mapper.Map<IEnumerable<CgstMasterModel>>(dbCgstMaster);
            serviceResponse.Data = sgstMasterModel.ToList();
            return serviceResponse;
        }



        public async Task<ServiceResponseModel<CgstMasterModel>> GetCgstByCode(int cgstId)
        {
            CgstMaster dbCgstMaster = await UnitOfWork.CgstMasters.GetCgstByCode(cgstId);
            CgstMasterModel cgstMasterModel = _mapper.Map<CgstMasterModel>(dbCgstMaster);
            ServiceResponseModel<CgstMasterModel> serviceResponse = new ServiceResponseModel<CgstMasterModel> { Data = cgstMasterModel };
            return serviceResponse;
        }

        public UnitOfWork UnitOfWork
        {
            get { return new UnitOfWork(_context); }
        }
    }
}
