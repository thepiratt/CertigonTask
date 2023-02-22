using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertigonTask_API_V3.Models.Accounts
{
    public class AccountRegisterVM
    {
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
