using Xunit;
using ObjectFiller.Test.TestPoco.Person;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    using System.Linq;


    public class AddressFillingTest
    {
        [Fact]
        public void FillAllAddressProperties()
        {
            Filler<Address> addressFiller = new Filler<Address>();
            Address a = addressFiller.Create();

            Assert.NotNull(a.City);
            Assert.NotNull(a.Country);
            Assert.NotEqual(0, a.HouseNumber);
            Assert.NotNull(a.PostalCode);
            Assert.NotNull(a.Street);
        }

        [Fact]
        public void IgnoreCountry()
        {
            Filler<Address> addressFiller = new Filler<Address>();
            addressFiller.Setup()
                    .OnProperty(x => x.Country).IgnoreIt();
            Address a = addressFiller.Create();

            Assert.NotNull(a.City);
            Assert.Null(a.Country);
            Assert.NotEqual(0, a.HouseNumber);
            Assert.NotNull(a.PostalCode);
            Assert.NotNull(a.Street);
        }

        [Fact]
        public void IgnoreCountryAndCity()
        {
            Filler<Address> addressFiller = new Filler<Address>();
            addressFiller.Setup()
                .OnProperty(x => x.Country, x => x.City).IgnoreIt();
            Address a = addressFiller.Create();

            Assert.Null(a.City);
            Assert.Null(a.Country);
            Assert.NotEqual(0, a.HouseNumber);
            Assert.NotNull(a.PostalCode);
            Assert.NotNull(a.Street);
        }

        [Fact]
        public void SetupCityPropertyWithConstantValue()
        {
            Filler<Address> addressFiller = new Filler<Address>();
            addressFiller.Setup()
                .OnProperty(ad => ad.City).Use(() => "City");
            Address a = addressFiller.Create();

            Assert.Equal("City", a.City);
            Assert.NotNull(a.Country);
            Assert.NotEqual(0, a.HouseNumber);
            Assert.NotNull(a.PostalCode);
            Assert.NotNull(a.Street);
        }

        [Fact]
        public void SetupCityAndCountryPropertyWithConstantValue()
        {
            Filler<Address> addressFiller = new Filler<Address>();
            addressFiller.Setup()
                .OnProperty(ad => ad.City, ad => ad.Country).Use(() => "CityCountry");
            Address a = addressFiller.Create();

            Assert.Equal("CityCountry", a.City);
            Assert.NotNull(a.Country);
            Assert.NotEqual(0, a.HouseNumber);
            Assert.NotNull(a.PostalCode);
            Assert.NotNull(a.Street);
        }

        [Fact]
        public void SetupTheAdressWithStaticValues()
        {
            Filler<Address> addressFiller = new Filler<Address>();
            addressFiller.Setup()
                .OnType<int>().Use(10)
                .OnProperty(x => x.City).Use("Dresden")
                .OnProperty(x => x.Country).Use("Germany")
                .OnProperty(x => x.PostalCode).Use(() => "0011100");

            var address = addressFiller.Create();
            Assert.Equal("Dresden", address.City);
            Assert.Equal("Germany", address.Country);
            Assert.Equal("0011100", address.PostalCode);
            Assert.Equal(10, address.HouseNumber);
        }

        [Fact]
        public void RandomListPluginShallReturnDifferentValues()
        {
            Filler<Address> addressFiller = new Filler<Address>();

            addressFiller.Setup().OnProperty(x => x.City).Use(new RandomListItem<string>("Test1", "Test2"));

            var addresses = addressFiller.Create(1000);

            Assert.True(addresses.Any(x => x.City == "Test2"));

            addressFiller = new Filler<Address>();

            addressFiller.Setup().OnProperty(x => x.City).Use(new RandomListItem<string>("Test1"));

            addresses = addressFiller.Create(1000);

            Assert.True(addresses.All(x => x.City == "Test1"));

        }


    }
}
