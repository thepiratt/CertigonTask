using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertigonTask_API_V3.Models.Accounts
{
    public class UserAccountUpdateVM
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime Created_time { get; set; }
        public bool isAdmin { get; set; }
        public bool isManager { get; set; }
    }
}
