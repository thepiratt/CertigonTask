using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertigonTask_API_V3.Models.Items
{
    public class ItemUpdateVM
    {
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string category { get; set; }
    }
}
