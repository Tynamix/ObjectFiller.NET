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
            ObjectFiller<Address> addressFiller = new ObjectFiller<Address>();
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
            ObjectFiller<Address> addressFiller = new ObjectFiller<Address>();
            addressFiller.Setup().IgnoreProperties(x => x.Country);
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
            ObjectFiller<Address> addressFiller = new ObjectFiller<Address>();
            addressFiller.Setup().IgnoreProperties(x => x.Country, x => x.City);
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
            ObjectFiller<Address> addressFiller = new ObjectFiller<Address>();
            addressFiller.Setup().RandomizerForProperty(() => "City", ad => ad.City);
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
            ObjectFiller<Address> addressFiller = new ObjectFiller<Address>();
            addressFiller.Setup()
                .RandomizerForProperty(() => "CityCountry", ad => ad.City, ad => ad.Country);
            Address a = addressFiller.Fill();

            Assert.AreEqual("CityCountry", a.City);
            Assert.IsNotNull(a.Country);
            Assert.AreNotEqual(0, a.HouseNumber);
            Assert.IsNotNull(a.PostalCode);
            Assert.IsNotNull(a.Street);
        }


    }
}
