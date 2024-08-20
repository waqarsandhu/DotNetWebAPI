using E_Commerce_Store.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_Store.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Zipcode { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Geolocation Geolocation { get; set; }
        [ForeignKey("GeolocationId")]
        public int GeolocationId { get; set; }
    }
}
