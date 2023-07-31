using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public List<Mobile> Mobiles { get; set; }
    }
}
