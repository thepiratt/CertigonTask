using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace CertigonTask_API_V3.Entities
{
    [Table("KorisnickiNalog")]
    public class KorisnickiNalog
    {
        [Key]
        public int id { get; set; }
        public string korisnickoIme { get; set; }

        public string email { get; set; }
        public DateTime created_time { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public bool isAdmin { get; set; }
        public bool isManager { get; set; }

    }
}
