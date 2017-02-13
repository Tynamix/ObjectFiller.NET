using System;
using Xunit;

namespace ObjectFiller.Test
{
    using Tynamix.ObjectFiller;


    public class SetupTests
    {
        public class Parent
        {
            public Child Child { get; set; }

            public int? SomeId { get; set; }
        }

        public class Child
        {
            public int IntValue { get; set; }

            public string StringValue { get; set; }
        }

        [Fact]
        public void ExplicitSetupShallJustFillPropertiesWhichAreSetUpEvenInSubtypes()
        {
            Filler<Parent> filler = new Filler<Parent>();
            filler.Setup(true)
                .SetupFor<Child>().OnProperty(x => x.IntValue).Use(new IntRange(1, 20));

            var parent = filler.Create();

            Assert.NotNull(parent);
            Assert.NotNull(parent.Child);
            Assert.Null(parent.SomeId);
            Assert.InRange(parent.Child.IntValue, 1, 20);
            Assert.Null(parent.Child.StringValue);
        }

        [Fact]
        public void ExplicitSetupShallJustFillPropertiesWhichAreSetUpAndNoInstanceShallCreateForSubTypesIfNotSetup()
        {
            Filler<Parent> filler = new Filler<Parent>();
            filler.Setup(true)
               .OnProperty(x => x.SomeId).Use(new IntRange(1, 20));

            var parent = filler.Create();

            Assert.NotNull(parent);
            Assert.Null(parent.Child);
            Assert.NotNull(parent.SomeId);
        }



        [Fact]
        public void RandomizerCreatesObjectsBasedOnPreviouseSetups()
        {
            int givenValue = Randomizer<int>.Create();

            var childFiller = new Filler<Child>();
            var childSetup = childFiller.Setup().OnProperty(x => x.IntValue).Use(givenValue).Result;

            var child = Randomizer<Child>.Create(childSetup);
            Assert.Equal(givenValue, child.IntValue);
        }

        [Fact]
        public void UseSetupsAgainForPropertyConfigurations()
        {
            int givenValue = Randomizer<int>.Create();

            var childFiller = new Filler<Child>();
            var childSetup = childFiller.Setup().OnProperty(x => x.IntValue).Use(givenValue).Result;

            var parentFiller = new Filler<Parent>();
            parentFiller.Setup().OnProperty(x => x.Child).Use(childSetup);

            var parent = parentFiller.Create();
            Assert.Equal(givenValue, parent.Child.IntValue);
        }

        [Fact]
        public void UseSetupsAgainForTypeConfigurations()
        {
            int givenValue = Randomizer<int>.Create();

            var childFiller = new Filler<Child>();
            var childSetup = childFiller.Setup().OnProperty(x => x.IntValue).Use(givenValue).Result;

            var parentFiller = new Filler<Parent>();
            parentFiller.Setup().OnType<Child>().Use(childSetup);

            var parent = parentFiller.Create();
            Assert.Equal(givenValue, parent.Child.IntValue);
        }

        [Fact]
        public void UseSetupsAgain()
        {
            int givenValue = Randomizer<int>.Create();

            var firstChildFiller = new Filler<Child>();
            var childSetup = firstChildFiller.Setup().OnProperty(x => x.IntValue).Use(givenValue).Result;

            var secondChildFiller = new Filler<Child>();
            secondChildFiller.Setup(childSetup);

            var child = secondChildFiller.Create();

            Assert.Equal(givenValue, child.IntValue);
        }

        [Fact]
        public void SetupsCanBeCreatedWithFactoryMethod()
        {
            var childSetup = FillerSetup.Create<Child>().OnProperty(x => x.IntValue).Use(42).Result;

            var child = Randomizer<Child>.Create(childSetup);
            Assert.Equal(42, child.IntValue);
        }

        [Fact]
        public void SetupsCanBeCreatedWithFactoryMethodBasedOnExistingSetupManager()
        {
            var childSetup = FillerSetup.Create<Child>().OnProperty(x => x.IntValue).Use(42).Result;
            childSetup = FillerSetup.Create<Child>(childSetup).OnProperty(x => x.StringValue).Use("Juchu").Result;

            var child = Randomizer<Child>.Create(childSetup);
            Assert.Equal(42, child.IntValue);
            Assert.Equal("Juchu", child.StringValue);
        }
    }
}
