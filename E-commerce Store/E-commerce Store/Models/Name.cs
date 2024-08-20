namespace E_Commerce_Store.Models
{
    public class Name
    {
        public int NameId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User User { get; set; }
    }
}
