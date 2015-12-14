using System;
using System.Collections.Generic;
using System.Linq;
    using Xunit;
using ObjectFiller.Test.TestPoco.Person;
using Tynamix.ObjectFiller;
using Random = Tynamix.ObjectFiller.Random;

namespace ObjectFiller.Test
{

    public class PersonFillingTest
    {
        [Fact]
        public void TestFillPerson()
        {
            Filler<Person> pFiller = new Filler<Person>();

            pFiller.Setup()
                .OnType<IAddress>().CreateInstanceOf<Address>();

            Person filledPerson = pFiller.Create();

            Assert.NotNull(filledPerson.Address);
            Assert.NotNull(filledPerson.Addresses);
            Assert.NotNull(filledPerson.StringToIAddress);
            Assert.NotNull(filledPerson.SureNames);

        }

        [Fact]
        public void TestFillPersonWithEnumerable()
        {
            Filler<Person> pFiller = new Filler<Person>();

            pFiller.Setup()
                .OnType<IAddress>().CreateInstanceOf<Address>()
                .OnProperty(x => x.Age).Use(Enumerable.Range(18, 60));
                

            Person filledPerson = pFiller.Create();

            Assert.NotNull(filledPerson.Address);
            Assert.NotNull(filledPerson.Addresses);
            Assert.NotNull(filledPerson.StringToIAddress);
            Assert.NotNull(filledPerson.SureNames);

        }

        [Fact]
        public void TestNameListStringRandomizer()
        {
            Filler<Person> pFiller = new Filler<Person>();

            pFiller.Setup().OnType<IAddress>().CreateInstanceOf<Address>()
                .OnProperty(p => p.FirstName).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.LastName).Use(new RealNames(NameStyle.LastName));

            Person filledPerson = pFiller.Create();

            Assert.NotNull(filledPerson.FirstName);
            Assert.NotNull(filledPerson.LastName);

        }

        [Fact]
        public void TestFirstNameAsConstantLastNameAsRealName()
        {
            Filler<Person> pFiller = new Filler<Person>();

            pFiller.Setup()
                .OnType<IAddress>().CreateInstanceOf<Address>()
                .OnProperty(p => p.FirstName).Use(() => "John")
                .OnProperty(p => p.LastName).Use(new RealNames(NameStyle.LastName));

            Person filledPerson = pFiller.Create();

            Assert.NotNull(filledPerson.FirstName);
            Assert.Equal("John", filledPerson.FirstName);
            Assert.NotNull(filledPerson.LastName);

        }

        [Fact]
        public void GeneratePersonWithGivenSetOfNamesAndAges()
        {
            List<string> names = new List<string> { "Tom", "Maik", "John", "Leo" };
            List<int> ages = new List<int> { 10, 15, 18, 22, 26 };

            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().CreateInstanceOf<Address>()
                .OnProperty(p => p.FirstName).Use(new RandomListItem<string>(names))
                .OnProperty(p => p.Age).Use(new RandomListItem<int>(ages));

            var pF = pFiller.Create();

            Assert.True(names.Contains(pF.FirstName));
            Assert.True(ages.Contains(pF.Age));
        }


        [Fact]
        public void BigComplicated()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().CreateInstanceOf<Address>()
                .OnProperty(p => p.LastName, p => p.FirstName).DoIt(At.TheEnd).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(p => p.Age).Use(() =>Random.Next(10, 32))
                .SetupFor<Address>()
                .OnProperty(a => a.City).Use(new MnemonicString(1))
                .OnProperty(a => a.Street).IgnoreIt();

            var pF = pFiller.Create();

            Assert.NotNull(pF);
            Assert.NotNull(pF.Address);
            Assert.Null(pF.Address.Street);

        }

        [Fact]
        public void FluentTest()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnProperty(x => x.Age).Use(() => 18)
                .OnType<IAddress>().CreateInstanceOf<Address>();

            Person p = pFiller.Create();
            Assert.NotNull(p);
            Assert.Equal(18, p.Age);

        }

        [Fact]
        public void TestSetupForTypeOverrideSettings()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().CreateInstanceOf<Address>()
                .OnType<int>().Use(() => 1)
                .SetupFor<Address>(true);

            Person p = pFiller.Create();
            Assert.Equal(1, p.Age);
            Assert.NotEqual(1, p.Address.HouseNumber);
        }

        [Fact]
        public void TestSetupForTypeWithoutOverrideSettings()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().CreateInstanceOf<Address>()
                .OnType<int>().Use(() => 1)
                .SetupFor<Address>();

            Person p = pFiller.Create();
            Assert.Equal(1, p.Age);
            Assert.Equal(1, p.Address.HouseNumber);
        }

        [Fact]
        public void TestIgnoreAllOfType()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().CreateInstanceOf<Address>()
                .OnType<string>().IgnoreIt()
                ;

            Person p = pFiller.Create();

            Assert.NotNull(p);
            Assert.Null(p.FirstName);
            Assert.NotNull(p.Address);
            Assert.Null(p.Address.City);
        }

        [Fact]
        public void TestIgnoreAllOfComplexType()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().CreateInstanceOf<Address>()
                .OnType<Address>().IgnoreIt()
                .OnType<IAddress>().IgnoreIt();

            Person p = pFiller.Create();

            Assert.NotNull(p);
            Assert.Null(p.Address);
        }

        [Fact]
        public void TestIgnoreAllOfTypeDictionary()
        {
            Filler<Person> pFiller = new Filler<Person>();
            pFiller.Setup()
                .OnType<IAddress>().CreateInstanceOf<Address>()
                .OnType<Address>().IgnoreIt()
                .OnType<IAddress>().IgnoreIt()
                .OnType<Dictionary<string, IAddress>>().IgnoreIt();

            Person p = pFiller.Create();

            Assert.NotNull(p);
            Assert.Null(p.Address);
            Assert.Null(p.StringToIAddress);
        }

        [Fact]
        public void TestPropertyOrderDoNameLast()
        {
            Filler<OrderedPersonProperties> filler = new Filler<OrderedPersonProperties>();
            filler.Setup()
                .OnProperty(x => x.Name).DoIt(At.TheEnd).UseDefault();

            var p = filler.Create();

            Assert.NotNull(p);
            Assert.Equal(3, p.NameCount);
        }

        [Fact]
        public void TestPropertyOrderDoNameFirst()
        {
            Filler<OrderedPersonProperties> filler = new Filler<OrderedPersonProperties>();
            filler.Setup()
                .OnProperty(x => x.Name).DoIt(At.TheBegin).UseDefault();

            var p = filler.Create();

            Assert.NotNull(p);
            Assert.Equal(1, p.NameCount);
        }

    }
}
