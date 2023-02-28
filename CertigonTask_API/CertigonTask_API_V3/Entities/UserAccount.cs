using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace CertigonTask_API_V3.Entities
{
    [Table("UserAccount")]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }
        public DateTime Created_time { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public bool isAdmin { get; set; }
        public bool isManager { get; set; }

    }
}
