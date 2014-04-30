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
            Address a = addressFiller.Fill();

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
            addressFiller.Setup().Ignore(x => x.Country);
            Address a = addressFiller.Fill();

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
            addressFiller.Setup().Ignore(x => x.Country, x => x.City);
            Address a = addressFiller.Fill();

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
            addressFiller.Setup().SetProperty(ad => ad.City, () => "City");
            Address a = addressFiller.Fill();

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
                .SetProperty(ad => ad.City, () => "CityCountry", ad => ad.Country);
            Address a = addressFiller.Fill();

            Assert.AreEqual("CityCountry", a.City);
            Assert.IsNotNull(a.Country);
            Assert.AreNotEqual(0, a.HouseNumber);
            Assert.IsNotNull(a.PostalCode);
            Assert.IsNotNull(a.Street);
        }


    }
}
