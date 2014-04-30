using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller.Test.TestPoco.Person;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    [TestClass]
    public class PersonFillingTest
    {
        [TestMethod]
        public void TestFillPerson()
        {
            Filler<Person> pFiller = new Filler<Person>();

            pFiller.Setup().RegisterInterface<IAddress, Address>();

            Person filledPerson = pFiller.Fill();

            Assert.IsNotNull(filledPerson.Address);
            Assert.IsNotNull(filledPerson.Addresses);
            Assert.IsNotNull(filledPerson.StringToIAddress);
            Assert.IsNotNull(filledPerson.SureNames);

        }

        [TestMethod]
        public void TestNameListStringRandomizer()
        {
            Filler<Person> pFiller = new Filler<Person>();

            pFiller.Setup().RegisterInterface<IAddress, Address>()
                .SetProperty(p => p.FirstName,new RealNames(true, false))
                .SetProperty(p => p.LastName, new RealNames(false, true));

            Person filledPerson = pFiller.Fill();

            Assert.IsNotNull(filledPerson.FirstName);
            Assert.IsNotNull(filledPerson.LastName);

        }

        [TestMethod]
        public void TestFirstNameAsConstantLastNameAsRealName()
        {
            Filler<Person> pFiller = new Filler<Person>();

            pFiller.Setup().RegisterInterface<IAddress, Address>()
                .SetProperty(p => p.FirstName,() => "John")
                .SetProperty(p => p.LastName, new RealNames(false, true));

            Person filledPerson = pFiller.Fill();

            Assert.IsNotNull(filledPerson.FirstName);
            Assert.AreEqual("John", filledPerson.FirstName);
            Assert.IsNotNull(filledPerson.LastName);

        }

        [TestMethod]
        public void GeneratePersonWithGivenSetOfNamesAndAges()
        {
            List<string> names = new List<string> { "Tom", "Maik", "John", "Leo" };
            List<int> ages = new List<int> { 10, 15, 18, 22, 26 };

            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .RegisterInterface<IAddress, Address>()
                .SetProperty(p => p.FirstName, new RandomListItem<string>(names))
                .SetProperty(p => p.Age, new RandomListItem<int>(ages));

            var pF = pFiller.Fill();

            Assert.IsTrue(names.Contains(pF.FirstName));
            Assert.IsTrue(ages.Contains(pF.Age));
        }


        [TestMethod]
        public void BigComplicated()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .RegisterInterface<IAddress, Address>()
                .SetProperty(p => p.LastName, new RealNames(true, false), p => p.FirstName)
                .SetProperty(p => p.Age, () => new Random().Next(10, 32))
                .For<Address>()
                .SetProperty(a => a.City, new MnemonicString(1))
                .Ignore(a => a.Street);

            var pF = pFiller.Fill();

            Assert.IsNotNull(pF);
            Assert.IsNotNull(pF.Address);
            Assert.IsNull(pF.Address.Street);

        }

        [TestMethod]
        public void TestSetupForTypeOverrideSettings()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .RegisterInterface<IAddress, Address>()
                .SetType<int>(() => 1)
                .For<Address>(true);

            Person p = pFiller.Fill();
            Assert.AreEqual(1, p.Age);
            Assert.AreNotEqual(1, p.Address.HouseNumber);
        }

        [TestMethod]
        public void TestSetupForTypeWithoutOverrideSettings()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .RegisterInterface<IAddress, Address>()
                .SetType<int>(() => 1)
                .For<Address>();

            Person p = pFiller.Fill();
            Assert.AreEqual(1, p.Age);
            Assert.AreEqual(1, p.Address.HouseNumber);
        }

        [TestMethod]
        public void TestIgnoreAllOfType()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .RegisterInterface<IAddress, Address>()
                .IgnoreAll<string>()
                ;

            Person p = pFiller.Fill();

            Assert.IsNotNull(p);
            Assert.IsNull(p.FirstName);
            Assert.IsNotNull(p.Address);
            Assert.IsNull(p.Address.City);
        }

        [TestMethod]
        public void TestIgnoreAllOfComplexType()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .RegisterInterface<IAddress, Address>()
                .IgnoreAll<Address>()
                .IgnoreAll<IAddress>()
                ;
            Person p = pFiller.Fill();

            Assert.IsNotNull(p);
            Assert.IsNull(p.Address);
        }

        [TestMethod]
        public void TestIgnoreAllOfTypeDictionary()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .RegisterInterface<IAddress, Address>()
                .IgnoreAll<Address>()
                .IgnoreAll<IAddress>()
                .IgnoreAll<Dictionary<string, IAddress>>();
            Person p = pFiller.Fill();

            Assert.IsNotNull(p);
            Assert.IsNull(p.Address);
            Assert.IsNull(p.StringToIAddress);
        }
    }
}