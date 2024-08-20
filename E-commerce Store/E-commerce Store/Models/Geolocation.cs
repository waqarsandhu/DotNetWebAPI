namespace E_Commerce_Store.Models
{
    public class Geolocation
    {
        public int geolocationId { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }

        public Address Address { get; set; }
    }
}
