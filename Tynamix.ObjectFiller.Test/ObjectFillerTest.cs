using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tynamix.ObjectFiller.Test.TestPoco.Person;

namespace Tynamix.ObjectFiller.Test
{
    [TestClass]
    public class ObjectFillerTest
    {
        [TestMethod]
        public void TestFillPerson()
        {
            Person p = new Person();
            Filler<Person> filler = new Filler<Person>();
            filler.Setup()
                .OnType<IAddress>().CreateInstanceOf<Address>()
                .OnType<string>().Use(new MnemonicString(10))
                .OnProperty(person => person.FirstName).Use<MnemonicString>()
                .OnProperty(person => person.LastName).Use(new RandomListItem<string>("Maik", "Tom", "Anton"))
                .OnProperty(person => person.Age).Use(() => Tynamix.ObjectFiller.Random.Next(12, 83))
                .SetupFor<Address>()
                .OnProperty(x => x.City, x => x.Country).IgnoreIt();

            Person pFilled = filler.Fill(p);

            Assert.IsTrue(new List<string>() { "Maik", "Tom", "Anton" }.Contains(pFilled.LastName));
        }

        [TestMethod]
        public void CreateMultipleInstances()
        {
            Filler<LibraryFillingTest.Person> filler = new Filler<LibraryFillingTest.Person>();
            IEnumerable<LibraryFillingTest.Person> pList = filler.Create(10);

            Assert.IsNotNull(pList);
            Assert.AreEqual(10, pList.Count());
        }

        [TestMethod]
        public void SetRandomSeedShallGenerateSameData()
        {
            var filler = new Filler<Address>();

            var address1 = filler.SetRandomSeed(1234).Create();

            var filler2 = new Filler<Address>();

            var address2 = filler.SetRandomSeed(1234).Create();

            Assert.AreEqual(address1.City, address2.City);
            Assert.AreEqual(address1.Country, address2.Country);
            Assert.AreEqual(address1.HouseNumber, address2.HouseNumber);
            Assert.AreEqual(address1.PostalCode, address2.PostalCode);
            Assert.AreEqual(address1.Street, address2.Street);
        }
    }
}
