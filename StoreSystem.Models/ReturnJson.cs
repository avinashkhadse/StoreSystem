using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Models
{
    public class ReturnJson
    {
        public string ResponseCode { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
