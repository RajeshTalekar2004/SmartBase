using System.Collections.Generic;

namespace SmartBase.BusinessLayer.Core.Domain
{
    public class TransactionMaster
    {
        public TransactionMaster()
        {
            TypeMasters = new HashSet<TypeMaster>();
        }

        public string TrxId { get; set; }
        public string DrCr { get; set; }
        public string TrxDetail { get; set; }
        public string AccountId1 { get; set; }
        public string AccountId2 { get; set; }
        public string AccountId3 { get; set; }

        public virtual ICollection<TypeMaster> TypeMasters { get; set; }
    }
}
