using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ObjectFiller.Test
{
    using Tynamix.ObjectFiller;

    [TestClass]
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

        [TestMethod]
        public void ExplicitSetupShallJustFillPropertiesWhichAreSetUpEvenInSubtypes()
        {
            Filler<Parent> filler = new Filler<Parent>();
            filler.Setup(true)
                .SetupFor<Child>().OnProperty(x => x.IntValue).Use(new IntRange(1, 20));

            var parent = filler.Create();

            Assert.IsNotNull(parent);
            Assert.IsNotNull(parent.Child);
            Assert.IsNull(parent.SomeId);
            Assert.IsTrue(parent.Child.IntValue > 1 && parent.Child.IntValue < 20);
            Assert.IsNull(parent.Child.StringValue);
        }

        [TestMethod]
        public void ExplicitSetupShallJustFillPropertiesWhichAreSetUpAndNoInstanceShallCreateForSubTypesIfNotSetup()
        {
            Filler<Parent> filler = new Filler<Parent>();
            filler.Setup(true)
               .OnProperty(x => x.SomeId).Use(new IntRange(1, 20));

            var parent = filler.Create();

            Assert.IsNotNull(parent);
            Assert.IsNull(parent.Child);
            Assert.IsNotNull(parent.SomeId);
        }



        [TestMethod]
        public void RandomizerCreatesObjectsBasedOnPreviouseSetups()
        {
            int givenValue = Randomizer<int>.Create();

            var childFiller = new Filler<Child>();
            var childSetup = childFiller.Setup().OnProperty(x => x.IntValue).Use(givenValue).Result;

            var child = Randomizer<Child>.Create(childSetup);
            Assert.AreEqual(givenValue, child.IntValue);
        }

        [TestMethod]
        public void UseSetupsAgainForPropertyConfigurations()
        {
            int givenValue = Randomizer<int>.Create();

            var childFiller = new Filler<Child>();
            var childSetup = childFiller.Setup().OnProperty(x => x.IntValue).Use(givenValue).Result;

            var parentFiller = new Filler<Parent>();
            parentFiller.Setup().OnProperty(x => x.Child).Use(childSetup);

            var parent = parentFiller.Create();
            Assert.AreEqual(givenValue, parent.Child.IntValue);
        }

        [TestMethod]
        public void UseSetupsAgainForTypeConfigurations()
        {
            int givenValue = Randomizer<int>.Create();

            var childFiller = new Filler<Child>();
            var childSetup = childFiller.Setup().OnProperty(x => x.IntValue).Use(givenValue).Result;

            var parentFiller = new Filler<Parent>();
            parentFiller.Setup().OnType<Child>().Use(childSetup);

            var parent = parentFiller.Create();
            Assert.AreEqual(givenValue, parent.Child.IntValue);
        }

        [TestMethod]
        public void UseSetupsAgain()
        {
            int givenValue = Randomizer<int>.Create();

            var firstChildFiller = new Filler<Child>();
            var childSetup = firstChildFiller.Setup().OnProperty(x => x.IntValue).Use(givenValue).Result;

            var secondChildFiller = new Filler<Child>();
            secondChildFiller.Setup(childSetup);

            var child = secondChildFiller.Create();

            Assert.AreEqual(givenValue, child.IntValue);
        }

        [TestMethod]
        public void SetupsCanBeCreatedWithFactoryMethod()
        {
            var childSetup = FillerSetup.Create<Child>().OnProperty(x => x.IntValue).Use(42).Result;

            var child = Randomizer<Child>.Create(childSetup);
            Assert.AreEqual(42, child.IntValue);
        }

        [TestMethod]
        public void SetupsCanBeCreatedWithFactoryMethodBasedOnExistingSetupManager()
        {
            var childSetup = FillerSetup.Create<Child>().OnProperty(x => x.IntValue).Use(42).Result;
            childSetup = FillerSetup.Create<Child>(childSetup).OnProperty(x => x.StringValue).Use("Juchu").Result;

            var child = Randomizer<Child>.Create(childSetup);
            Assert.AreEqual(42, child.IntValue);
            Assert.AreEqual("Juchu", child.StringValue);
        }
    }
}
