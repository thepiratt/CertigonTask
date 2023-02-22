using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertigonTask_API_V3.Models.Accounts
{
    public class KorisnickiNalogUpdateVM
    {
        public string email { get; set; }
        public string korisnickoIme { get; set; }
        public DateTime created_time { get; set; }
        public bool isAdmin { get; set; }
        public bool isManager { get; set; }
    }
}
