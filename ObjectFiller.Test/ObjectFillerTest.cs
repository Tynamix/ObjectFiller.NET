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
                .RegisterInterface<IAddress, Address>()
                .SetType(new MnemonicString(10))
                .SetProperty(person => person.FirstName, new MnemonicString(1) )
                .SetProperty(person => person.LastName,new RandomListItem<string>(new List<string>() { "Maik", "Tom", "Anton" }))
                .SetProperty(person => person.Age,() => new Random().Next(12, 83))
                .For<Address>()
                .Ignore(a => a.City, a => a.Country);

            Person pFilled = filler.Fill(p);
        }
    }
}
