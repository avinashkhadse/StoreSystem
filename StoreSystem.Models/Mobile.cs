using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreSystem.Models.Reports;

namespace StoreSystem.Models
{
    public class Mobile
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public DateTime Date { get; set; }
        public Brand Brand { get; set; }
        public List<SaleItem> SaleItems { get; set; }
    }
}
