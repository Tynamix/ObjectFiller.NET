using System;
using System.Collections.Generic;
using System.Linq;
    using Xunit;
using ObjectFiller.Test.TestPoco.Person;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{

    public class ObjectFillerTest
    {
        [Fact]
        public void TestFillPerson()
        {
            Person p = new Person();
            Filler<Person> filler = new Filler<Person>();
            filler.Setup()
                .OnType<IAddress>().CreateInstanceOf<Address>()
                .OnType<string>().Use(new MnemonicString(10))
                .OnProperty(person => person.FirstName).Use(new MnemonicString(1))
                .OnProperty(person => person.LastName).Use(new RandomListItem<string>("Maik", "Tom", "Anton"))
                .OnProperty(person => person.Age).Use(() => Tynamix.ObjectFiller.Random.Next(12, 83))
                .SetupFor<Address>()
                .OnProperty(x => x.City, x => x.Country).IgnoreIt();

            Person pFilled = filler.Fill(p);

            Assert.True(new List<string>() { "Maik", "Tom", "Anton" }.Contains(pFilled.LastName));
        }




        [Fact]
        public void CreateMultipleInstances()
        {
            Filler<LibraryFillingTest.Person> filler = new Filler<LibraryFillingTest.Person>();
            IEnumerable<LibraryFillingTest.Person> pList = filler.Create(10);

            Assert.NotNull(pList);
            Assert.Equal(10, pList.Count());
        }
    }
}
