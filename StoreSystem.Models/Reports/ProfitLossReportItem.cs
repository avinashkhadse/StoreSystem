using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Models.Reports
{
    public class ProfitLossReportItem
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal ProfitLoss { get; set; }
    }
}
