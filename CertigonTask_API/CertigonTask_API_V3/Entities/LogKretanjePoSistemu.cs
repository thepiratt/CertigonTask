using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CertigonTask_API_V3.Entities
{
    public class LogKretanjePoSistemu
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public string userAccountId { get; set; }
        public UserAccount User { get; set; }
        public string queryPath { get; set; }
        public string postData { get; set; }
        public DateTime Time { get; set; }
        public string IpAdress { get; set; }
        public string ExceptionMessage { get; set; }
        public bool isException { get; set; }
    }
}
