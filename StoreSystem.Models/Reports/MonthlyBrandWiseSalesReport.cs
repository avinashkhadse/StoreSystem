using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Models.Reports
{
    public class MonthlyBrandWiseSalesReport
    {
        public Brand Brand { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalProfitLoss { get; set; }
    }
}
