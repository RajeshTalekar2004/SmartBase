using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Persistence;
using SmartBase.BusinessLayer.Persistence.Models;
using SmartBase.BusinessLayer.Persistence.PageParams;
using SmartBase.BusinessLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Services
{
    public class TypeMasterService : ITypeMasterService
    {
        public TypeMasterService(SmartAccountContext context, IMapper mapper)
        {
            _context = context;
             _mapper = mapper;
        }

        public SmartAccountContext _context { get; }
        private readonly IMapper _mapper;
 
        public async Task<ServiceResponseModel<TypeMasterModel>> Add(TypeMasterModel newTypeMasterModel)
        {
            ServiceResponseModel<TypeMasterModel> serviceResponse = new ServiceResponseModel<TypeMasterModel>();
            TypeMaster typeMaster = _mapper.Map<TypeMaster>(newTypeMasterModel);
            await UnitOfWork.TypeMasters.AddAsync(typeMaster);
            await UnitOfWork.Complete();
            serviceResponse.Data = newTypeMasterModel;
            return serviceResponse;
        }

        public async Task<ServiceResponseModel<TypeMasterModel>> Delete(TypeMasterModel delTypeMasterModel)
        {
            ServiceResponseModel<TypeMasterModel> serviceResponse = new ServiceResponseModel<TypeMasterModel>();
            TypeMaster delTypeMaster = await UnitOfWork.TypeMasters.SingleOrDefaultAsync(t => 
                                        t.CompCode == delTypeMasterModel.CompCode &&
                                        t.AccYear == delTypeMasterModel.AccYear &&
                                        t.TrxCd == delTypeMasterModel.TrxCd);
            UnitOfWork.TypeMasters.Remove(delTypeMaster);
            await UnitOfWork.Complete();
            TypeMasterModel delCgstMasterModel = _mapper.Map<TypeMasterModel>(delTypeMaster);
            serviceResponse.Data = delCgstMasterModel;

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<TypeMasterModel>> Edit(TypeMasterModel editTypeMasterModel)
        {
            ServiceResponseModel<TypeMasterModel> serviceResponse = new ServiceResponseModel<TypeMasterModel>();

            TypeMaster editTypeMaster = await UnitOfWork.TypeMasters.SingleOrDefaultAsync(t =>
                                        t.CompCode == editTypeMasterModel.CompCode &&
                                        t.AccYear == editTypeMasterModel.AccYear &&
                                        t.TrxCd == editTypeMasterModel.TrxCd);
            _mapper.Map<TypeMasterModel, TypeMaster>(editTypeMasterModel, editTypeMaster);
            UnitOfWork.TypeMasters.Update(editTypeMaster);
            await UnitOfWork.Complete();
            serviceResponse.Data = editTypeMasterModel;

            return serviceResponse;
        }

        public async Task<ServiceResponseModel<IEnumerable<TypeMasterModel>>> GetAll(TypeMasterModel editTypeMasterModel)
        {
            ServiceResponseModel<IEnumerable<TypeMasterModel>> serviceResponse = new ServiceResponseModel<IEnumerable<TypeMasterModel>>();

            IEnumerable<TypeMaster> dbTypeMaster = await UnitOfWork.TypeMasters.FindAsync(t =>
                                        t.CompCode == editTypeMasterModel.CompCode &&
                                        t.AccYear == editTypeMasterModel.AccYear);

            IEnumerable<TypeMasterModel> typeMasterModel = _mapper.Map<IEnumerable<TypeMasterModel>>(dbTypeMaster);
            serviceResponse.Data = typeMasterModel;
            return serviceResponse;
        }


        public async Task<PagedList<TypeMaster>> GetAll(TypeParams typeParams)
        {
            var query = _context.TypeMasters
                        .Where(a=>a.CompCode==typeParams.CompCode && a.AccYear == typeParams.AccYear)
                        .AsQueryable();

            switch (typeParams.OrderBy)
            {
                case "trxCd":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c => c.AccYear).ThenBy(c=>c.TrxCd);
                    break;
                case "trxDetail":
                    query = query.OrderBy(c => c.CompCode).ThenBy(c => c.AccYear).ThenBy(c=>c.TrxDetail);
                    break;
                default:
                    query = query.OrderBy(c => c.CompCode).ThenBy(c => c.AccYear).ThenBy(c=>c.TrxCd);
                    break;
            }

            return await PagedList<TypeMaster>.CreateAsync(query, typeParams.PageNumber, typeParams.PageSize);
        }



        public async Task<ServiceResponseModel<TypeMasterModel>> GetTypeByCode(TypeMasterModel editTypeMasterModel)
        {
            ServiceResponseModel<TypeMasterModel> serviceResponse = new ServiceResponseModel<TypeMasterModel>();
            TypeMaster editTypeMaster = await UnitOfWork.TypeMasters.SingleOrDefaultAsync(t =>
                                        t.CompCode == editTypeMasterModel.CompCode &&
                                        t.AccYear == editTypeMasterModel.AccYear &&
                                        t.TrxCd == editTypeMasterModel.TrxCd);
            TypeMasterModel typeMasterModelall = _mapper.Map<TypeMasterModel>(editTypeMaster);
            serviceResponse.Data = typeMasterModelall;
            return serviceResponse;
        }

        public UnitOfWork UnitOfWork
        {
            get { return new UnitOfWork(_context); }
        }
    }
}
