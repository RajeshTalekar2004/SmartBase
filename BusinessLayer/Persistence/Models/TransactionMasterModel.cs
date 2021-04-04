using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class TransactionMasterModel
    {
        public TransactionMasterModel()
        {
            TypeMasters = new HashSet<TypeMasterModel>();
        }

        public string TrxId { get; set; }
        public string DrCr { get; set; }
        public string TrxDetail { get; set; }
        public string AccountId1 { get; set; }
        public string AccountId2 { get; set; }
        public string AccountId3 { get; set; }
        public string OrderBy { get; set; } = "trxId";
        public virtual ICollection<TypeMasterModel> TypeMasters { get; set; }
    }
}
