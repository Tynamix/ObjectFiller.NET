using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller.Test.TestPoco.Person;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    using System.Linq;

    [TestClass]
    public class AddressFillingTest
    {
        [TestMethod]
        public void FillAllAddressProperties()
        {
            Filler<Address> addressFiller = new Filler<Address>();
            Address a = addressFiller.Create();

            Assert.IsNotNull(a.City);
            Assert.IsNotNull(a.Country);
            Assert.AreNotEqual(0, a.HouseNumber);
            Assert.IsNotNull(a.PostalCode);
            Assert.IsNotNull(a.Street);
        }

        [TestMethod]
        public void IgnoreCountry()
        {
            Filler<Address> addressFiller = new Filler<Address>();
            addressFiller.Setup()
                    .OnProperty(x => x.Country).IgnoreIt();
            Address a = addressFiller.Create();

            Assert.IsNotNull(a.City);
            Assert.IsNull(a.Country);
            Assert.AreNotEqual(0, a.HouseNumber);
            Assert.IsNotNull(a.PostalCode);
            Assert.IsNotNull(a.Street);
        }

        [TestMethod]
        public void IgnoreCountryAndCity()
        {
            Filler<Address> addressFiller = new Filler<Address>();
            addressFiller.Setup()
                .OnProperty(x => x.Country, x => x.City).IgnoreIt();
            Address a = addressFiller.Create();

            Assert.IsNull(a.City);
            Assert.IsNull(a.Country);
            Assert.AreNotEqual(0, a.HouseNumber);
            Assert.IsNotNull(a.PostalCode);
            Assert.IsNotNull(a.Street);
        }

        [TestMethod]
        public void SetupCityPropertyWithConstantValue()
        {
            Filler<Address> addressFiller = new Filler<Address>();
            addressFiller.Setup()
                .OnProperty(ad => ad.City).Use(() => "City");
            Address a = addressFiller.Create();

            Assert.AreEqual("City", a.City);
            Assert.IsNotNull(a.Country);
            Assert.AreNotEqual(0, a.HouseNumber);
            Assert.IsNotNull(a.PostalCode);
            Assert.IsNotNull(a.Street);
        }

        [TestMethod]
        public void SetupCityAndCountryPropertyWithConstantValue()
        {
            Filler<Address> addressFiller = new Filler<Address>();
            addressFiller.Setup()
                .OnProperty(ad => ad.City, ad => ad.Country).Use(() => "CityCountry");
            Address a = addressFiller.Create();

            Assert.AreEqual("CityCountry", a.City);
            Assert.IsNotNull(a.Country);
            Assert.AreNotEqual(0, a.HouseNumber);
            Assert.IsNotNull(a.PostalCode);
            Assert.IsNotNull(a.Street);
        }

        [TestMethod]
        public void SetupTheAdressWithStaticValues()
        {
            Filler<Address> addressFiller = new Filler<Address>();
            addressFiller.Setup()
                .OnType<int>().Use(10)
                .OnProperty(x => x.City).Use("Dresden")
                .OnProperty(x => x.Country).Use("Germany")
                .OnProperty(x => x.PostalCode).Use(() => "0011100");

            var address = addressFiller.Create();
            Assert.AreEqual("Dresden", address.City);
            Assert.AreEqual("Germany", address.Country);
            Assert.AreEqual("0011100", address.PostalCode);
            Assert.AreEqual(10, address.HouseNumber);
        }

        [TestMethod]
        public void RandomListPluginShallReturnDifferentValues()
        {
            Filler<Address> addressFiller = new Filler<Address>();

            addressFiller.Setup().OnProperty(x => x.City).Use(new RandomListItem<string>("Test1", "Test2"));

            var addresses = addressFiller.Create(1000);

            Assert.IsTrue(addresses.Any(x => x.City == "Test2"));

            addressFiller = new Filler<Address>();

            addressFiller.Setup().OnProperty(x => x.City).Use(new RandomListItem<string>("Test1"));

            addresses = addressFiller.Create(1000);

            Assert.IsTrue(addresses.All(x => x.City == "Test1"));

        }


    }
}
