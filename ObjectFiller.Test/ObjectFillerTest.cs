using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller.Test.TestPoco.Person;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
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
                .OnProperty(person => person.FirstName).Use(new MnemonicString(1))
                .OnProperty(person => person.LastName).Use(new RandomListItem<string>(new List<string>() { "Maik", "Tom", "Anton" }))
                .OnProperty(person => person.Age).Use(() => new Random().Next(12, 83))
                .SetupFor<Address>()
                .OnProperty(x => x.City, x => x.Country).IgnoreIt();

            Person pFilled = filler.Fill(p);
        }
    }
}
