using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBase.BusinessLayer.Persistence.Models
{
    public class ReportModel
    {
        public string CompCode { get; set; }
        public string AccYear { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string StartAccount { get; set; }
        public string FinishAccount { get; set; }
        public string SaleOrPurchaseType { get; set; }
    }
}
