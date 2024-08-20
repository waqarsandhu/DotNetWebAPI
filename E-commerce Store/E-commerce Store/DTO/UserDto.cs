namespace E_Commerce_Store.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string Phone { get; set; }
        public NameDto Name { get; set; }
        public List<AddressDto> Addresses { get; set; }
    }
}
