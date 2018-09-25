namespace Tynamix.ObjectFiller.Test.TestPoco.Person
{
    public class Address : IAddress
    {
        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int HouseNumber { get; set; }
    }
}