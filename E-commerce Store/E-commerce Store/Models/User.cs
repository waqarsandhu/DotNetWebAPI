using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
namespace E_Commerce_Store.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Name Name { get; set; }
        [ForeignKey("NameId")]
        public int NameId { get; set; }

        public ICollection<Address> Addresses { get; set; }

    }
}
