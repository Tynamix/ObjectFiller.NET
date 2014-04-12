using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller.FillerPlugins;
using ObjectFiller.FillerPlugins.String;
using ObjectFiller.Test.TestPoco.Person;

namespace ObjectFiller.Test
{
    [TestClass]
    public class PersonFillingTest
    {
        [TestMethod]
        public void TestFillPerson()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();

            pFiller.Setup().RegisterInterface<IAddress, Address>();

            Person filledPerson = pFiller.Fill();

            Assert.IsNotNull(filledPerson.Address);
            Assert.IsNotNull(filledPerson.Addresses);
            Assert.IsNotNull(filledPerson.BlaToBla);
            Assert.IsNotNull(filledPerson.SureNames);

        }

        [TestMethod]
        public void TestNameListStringRandomizer()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();

            pFiller.Setup().RegisterInterface<IAddress, Address>()
                .RandomizerForProperty(new RealNamePlugin(true, false), p => p.FirstName)
                .RandomizerForProperty(new RealNamePlugin(false, true), p => p.LastName);

            Person filledPerson = pFiller.Fill();

            Assert.IsNotNull(filledPerson.FirstName);
            Assert.IsNotNull(filledPerson.LastName);

        }

        [TestMethod]
        public void TestFirstNameAsConstantLastNameAsRealName()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();

            pFiller.Setup().RegisterInterface<IAddress, Address>()
                .RandomizerForProperty(() => "John", p => p.FirstName)
                .RandomizerForProperty(new RealNamePlugin(false, true), p => p.LastName);

            Person filledPerson = pFiller.Fill();

            Assert.IsNotNull(filledPerson.FirstName);
            Assert.AreEqual("John", filledPerson.FirstName);
            Assert.IsNotNull(filledPerson.LastName);

        }

        [TestMethod]
        public void GeneratePersonWithGivenSetOfNamesAndAges()
        {
            List<string> names = new List<string>() { "Tom", "Maik", "John", "Leo" };
            List<int> ages = new List<int>() { 10, 15, 18, 22, 26 };

            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .RegisterInterface<IAddress, Address>()
                .RandomizerForProperty(new RandomListItem<string>(names), p => p.FirstName)
                .RandomizerForProperty(new RandomListItem<int>(ages), p => p.Age);

            var pF = pFiller.Fill();

            Assert.IsTrue(names.Contains(pF.FirstName));
            Assert.IsTrue(ages.Contains(pF.Age));
        }


        [TestMethod]
        public void BigComplicated()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .RegisterInterface<IAddress, Address>()
                .RandomizerForProperty(new RealNamePlugin(true, false), p => p.LastName, p => p.FirstName)
                .RandomizerForProperty(() => new Random().Next(10, 32), p => p.Age)
                .SetupFor<Address>()
                .RandomizerForProperty(new MnemonicStringPlugin(1), a => a.City)
                .IgnoreProperties(a => a.Street);

            var pF = pFiller.Fill();
        }

        [TestMethod]
        public void TestSetupForTypeOverrideSettings()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .RegisterInterface<IAddress, Address>()
                .RandomizerForType<int>(() => 1)
                .SetupFor<Address>(true);

            Person p = pFiller.Fill();
            Assert.AreEqual(1, p.Age);
            Assert.AreNotEqual(1, p.Address.HouseNumber);
        }
        [TestMethod]
        public void TestSetupForTypeWithoutOverrideSettings()
        {
            ObjectFiller<Person> pFiller = new ObjectFiller<Person>();
            pFiller.Setup()
                .RegisterInterface<IAddress, Address>()
                .RandomizerForType<int>(() => 1)
                .SetupFor<Address>();

            Person p = pFiller.Fill();
            Assert.AreEqual(1, p.Age);
            Assert.AreEqual(1, p.Address.HouseNumber);
        }
    }
}