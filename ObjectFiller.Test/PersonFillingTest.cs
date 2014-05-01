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

            pFiller.Setup()
                .OnType<IAddress>().Register<Address>();

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

            pFiller.Setup().OnType<IAddress>().Register<Address>()
                .OnProperty(p => p.FirstName).Use(new RealNames(true, false))
                .OnProperty(p => p.LastName).Use(new RealNames(false, true));

            Person filledPerson = pFiller.Fill();

            Assert.IsNotNull(filledPerson.FirstName);
            Assert.IsNotNull(filledPerson.LastName);

        }

        [TestMethod]
        public void TestFirstNameAsConstantLastNameAsRealName()
        {
            Filler<Person> pFiller = new Filler<Person>();

            pFiller.Setup()
                .OnType<IAddress>().Register<Address>()
                .OnProperty(p => p.FirstName).Use(() => "John")
                .OnProperty(p => p.LastName).Use(new RealNames(false, true));

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
                .OnType<IAddress>().Register<Address>()
                .OnProperty(p => p.FirstName).Use(new RandomListItem<string>(names))
                .OnProperty(p => p.Age).Use(new RandomListItem<int>(ages));

            var pF = pFiller.Fill();

            Assert.IsTrue(names.Contains(pF.FirstName));
            Assert.IsTrue(ages.Contains(pF.Age));
        }


        [TestMethod]
        public void BigComplicated()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().Register<Address>()
                .OnProperty(p => p.LastName, p => p.FirstName).DoIt(At.TheEnd).Use(new RealNames(true, false))
                .OnProperty(p => p.Age).Use(() => new Random().Next(10, 32))
                .SetupFor<Address>()
                .OnProperty(a => a.City).Use(new MnemonicString(1))
                .OnProperty(a => a.Street).IgnoreIt();

            var pF = pFiller.Fill();

            Assert.IsNotNull(pF);
            Assert.IsNotNull(pF.Address);
            Assert.IsNull(pF.Address.Street);

        }

        [TestMethod]
        public void FluentTest()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnProperty(x => x.Age).Use(() => 18)
                .OnType<IAddress>().Register<Address>();

            Person p = pFiller.Fill();
            Assert.IsNotNull(p);
            Assert.AreEqual(18, p.Age);

        }

        [TestMethod]
        public void TestSetupForTypeOverrideSettings()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().Register<Address>()
                .OnType<int>().Use(() => 1)
                .SetupFor<Address>(true);

            Person p = pFiller.Fill();
            Assert.AreEqual(1, p.Age);
            Assert.AreNotEqual(1, p.Address.HouseNumber);
        }

        [TestMethod]
        public void TestSetupForTypeWithoutOverrideSettings()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().Register<Address>()
                .OnType<int>().Use(() => 1)
                .SetupFor<Address>();

            Person p = pFiller.Fill();
            Assert.AreEqual(1, p.Age);
            Assert.AreEqual(1, p.Address.HouseNumber);
        }

        [TestMethod]
        public void TestIgnoreAllOfType()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().Register<Address>()
                .OnType<string>().IgnoreIt()
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
                .OnType<IAddress>().Register<Address>()
                .OnType<Address>().IgnoreIt()
                .OnType<IAddress>().IgnoreIt();

            Person p = pFiller.Fill();

            Assert.IsNotNull(p);
            Assert.IsNull(p.Address);
        }

        [TestMethod]
        public void TestIgnoreAllOfTypeDictionary()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().Register<Address>()
                .OnType<Address>().IgnoreIt()
                .OnType<IAddress>().IgnoreIt()
                .OnType<Dictionary<string, IAddress>>().IgnoreIt();

            Person p = pFiller.Fill();

            Assert.IsNotNull(p);
            Assert.IsNull(p.Address);
            Assert.IsNull(p.StringToIAddress);
        }

        [TestMethod]
        public void TestPropertyOrderDoNameLast()
        {
            Filler<OrderedPersonProperties> filler = new Filler<OrderedPersonProperties>();
            filler.Setup()
                .OnProperty(x => x.Name).DoIt(At.TheEnd).UseDefault();

            var p = filler.Fill();

            Assert.IsNotNull(p);
            Assert.AreEqual(3, p.NameCount);
        }

        [TestMethod]
        public void TestPropertyOrderDoNameFirst()
        {
            Filler<OrderedPersonProperties> filler = new Filler<OrderedPersonProperties>();
            filler.Setup()
                .OnProperty(x => x.Name).DoIt(At.TheBegin).UseDefault();

            var p = filler.Fill();

            Assert.IsNotNull(p);
            Assert.AreEqual(1, p.NameCount);
        }
    }
}