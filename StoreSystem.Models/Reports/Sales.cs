using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Models.Reports
{
    public class Sales
    {
        public int Id { get; set; }
        public int MobileId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public DateTime Date { get; set; }
        public Mobile Mobile { get; set; }
        public User User { get; set; }
        public List<SaleItem> SaleItems { get; set; }

    }
}
