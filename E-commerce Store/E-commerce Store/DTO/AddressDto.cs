namespace E_Commerce_Store.DTO
{
    public class AddressDto
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Zipcode { get; set; }
        public GeolocationDto Geolocation { get; set; }
    }
}
