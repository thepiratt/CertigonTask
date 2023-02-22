using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertigonTask_API_V3.Models.Accounts
{
    public class UpdateVM
    {
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public DateTime created_time { get; set; }
        public bool isAdmin { get; set; }
        public bool isManager { get; set; }
    }
}
