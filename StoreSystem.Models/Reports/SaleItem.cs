using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Models.Reports
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int SalesId { get; set; }
        public int MobileId { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public Sales Sales { get; set; }
        public Mobile Mobile { get; set; }
    }
}
