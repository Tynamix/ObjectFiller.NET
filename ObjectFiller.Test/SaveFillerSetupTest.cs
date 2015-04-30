using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller.Test.TestPoco.Person;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    [TestClass]
    public class SaveFillerSetupTest
    {
        private FillerSetup _fillerSetup;

        [TestInitialize]
        public void GetFillerSetup()
        {

            Filler<Person> filler = new Filler<Person>();
            _fillerSetup = filler.Setup()
                 .OnType<IAddress>().CreateInstanceOf<Address>()
                 .OnProperty(x => x.Age).Use(new IntRange(18, 35))
                 .OnProperty(x => x.FirstName).Use(new RealNames(NameStyle.FirstName))
                 .OnProperty(x => x.LastName).Use(new RealNames(NameStyle.LastName))
                 .SetupFor<Address>()
                 .OnProperty(x => x.HouseNumber).Use(new IntRange(1, 100))
                 .Result;

        }

        [TestMethod]
        public void UseSavedFillerDefaultSetup()
        {
            Filler<Person> filler = new Filler<Person>();
            filler.Setup(_fillerSetup);

            Person p = filler.Create();

            Assert.IsTrue(p.Age < 35 && p.Age >= 18);
            Assert.IsTrue(p.Address.HouseNumber < 100 && p.Age >= 1);
        }


        [TestMethod]
        public void UseSavedFillerSetupWithExtensions()
        {
            var dateNow = DateTime.Now;
            Filler<Person> filler = new Filler<Person>();
            filler.Setup(_fillerSetup)
                .OnProperty(x => x.Birthdate).Use(() => dateNow);

            Person p = filler.Create();

            Assert.IsTrue(p.Age < 35 && p.Age >= 18);
            Assert.IsTrue(p.Address.HouseNumber < 100 && p.Age >= 1);
            Assert.AreEqual(p.Birthdate, dateNow);

        }

        [TestMethod]
        public void UseSavedFillerSetupWithOverrides()
        {
            Filler<Person> filler = new Filler<Person>();
            filler.Setup(_fillerSetup)
                .OnProperty(x => x.Age).Use(() => 1000)
                .SetupFor<Address>()
                .OnProperty(x => x.HouseNumber).Use(() => 9999);

            Person p = filler.Create();

            Assert.AreEqual(p.Age, 1000);
            Assert.AreEqual(p.Address.HouseNumber, 9999);

        }



    }
}
