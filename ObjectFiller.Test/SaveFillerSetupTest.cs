using System;
    using Xunit;
using ObjectFiller.Test.TestPoco.Person;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{

    public class SaveFillerSetupTest
    {

        public FillerSetup GetFillerSetup()
        {

            Filler<Person> filler = new Filler<Person>();
            return filler.Setup()
                 .OnType<IAddress>().CreateInstanceOf<Address>()
                 .OnProperty(x => x.Age).Use(new IntRange(18, 35))
                 .OnProperty(x => x.FirstName).Use(new RealNames(NameStyle.FirstName))
                 .OnProperty(x => x.LastName).Use(new RealNames(NameStyle.LastName))
                 .SetupFor<Address>()
                 .OnProperty(x => x.HouseNumber).Use(new IntRange(1, 100))
                 .Result;

        }

        [Fact]
        public void UseSavedFillerDefaultSetup()
        {
            Filler<Person> filler = new Filler<Person>();
            filler.Setup(GetFillerSetup());

            Person p = filler.Create();

            Assert.True(p.Age < 35 && p.Age >= 18);
            Assert.True(p.Address.HouseNumber < 100 && p.Age >= 1);
        }


        [Fact]
        public void UseSavedFillerSetupWithExtensions()
        {
            var dateNow = DateTime.Now;
            Filler<Person> filler = new Filler<Person>();
            filler.Setup(GetFillerSetup())
                .OnProperty(x => x.Birthdate).Use(() => dateNow);

            Person p = filler.Create();

            Assert.True(p.Age < 35 && p.Age >= 18);
            Assert.True(p.Address.HouseNumber < 100 && p.Age >= 1);
            Assert.Equal(p.Birthdate, dateNow);

        }

        [Fact]
        public void UseSavedFillerSetupWithOverrides()
        {
            Filler<Person> filler = new Filler<Person>();
            filler.Setup(GetFillerSetup())
                .OnProperty(x => x.Age).Use(() => 1000)
                .SetupFor<Address>()
                .OnProperty(x => x.HouseNumber).Use(() => 9999);

            Person p = filler.Create();

            Assert.Equal(p.Age, 1000);
            Assert.Equal(p.Address.HouseNumber, 9999);

        }



    }
}
