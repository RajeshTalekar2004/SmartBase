using SmartBase.BusinessLayer.Core.Domain;
using SmartBase.BusinessLayer.Core.Repositories;

namespace SmartBase.BusinessLayer.Persistence.Repositories
{
    public class TypeMasterRepository : Repository<TypeMaster>, ITypeMasterRepository
    {
        public TypeMasterRepository(SmartAccountContext context) : base(context)
        {
        }
        public SmartAccountContext SmartAccountContext
        {
            get { return Context as SmartAccountContext; }
        }
    }
}
