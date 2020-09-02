namespace Core.Models
{
    public class ShippingAddress
    {
        public ShippingAddress() { }

        public ShippingAddress(
            string firstName,
            string lastName,
            string street,
            string city,
            string province,
            string postalCode,
            string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            Province = province;
            PostalCode = postalCode;
            Country = country;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}