using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CertigonTask_API_V3.Entities
{
    public class AuthenticationToken
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        [ForeignKey(nameof(UserAccount))]
        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
        public DateTime Created_Time { get; set; }
        public string IpAdress { get; set; }

    }
}
