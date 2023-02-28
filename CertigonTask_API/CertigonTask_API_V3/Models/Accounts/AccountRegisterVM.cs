using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertigonTask_API_V3.Models.Accounts
{
    public class AccountRegisterVM
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
