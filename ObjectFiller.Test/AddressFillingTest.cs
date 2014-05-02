using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller.Test.TestPoco.Person;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
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
                    .OnProperty(x=>x.Country).IgnoreIt();
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


    }
}
