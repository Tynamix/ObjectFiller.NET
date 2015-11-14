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
        }

        public class Child
        {
            public int Value { get; set; }
        }

        [TestMethod]
        public void RandomizerCreatesObjectsBasedOnPreviouseSetups()
        {
            int givenValue = Randomizer<int>.Create();

            var childFiller = new Filler<Child>();
            var childSetup = childFiller.Setup().OnProperty(x => x.Value).Use(givenValue).Result;
            
            var child = Randomizer<Child>.Create(childSetup);
            Assert.AreEqual(givenValue, child.Value);
        }

        [TestMethod]
        public void UseSetupsAgainForPropertyConfigurations()
        {
            int givenValue = Randomizer<int>.Create();

            var childFiller = new Filler<Child>();
            var childSetup = childFiller.Setup().OnProperty(x => x.Value).Use(givenValue).Result;

            var parentFiller = new Filler<Parent>();
            parentFiller.Setup().OnProperty(x => x.Child).Use(childSetup);

            var parent = parentFiller.Create();
            Assert.AreEqual(givenValue, parent.Child.Value);
        }

        [TestMethod]
        public void UseSetupsAgainForTypeConfigurations()
        {
            int givenValue = Randomizer<int>.Create();

            var childFiller = new Filler<Child>();
            var childSetup = childFiller.Setup().OnProperty(x => x.Value).Use(givenValue).Result;

            var parentFiller = new Filler<Parent>();
            parentFiller.Setup().OnType<Child>().Use(childSetup);

            var parent = parentFiller.Create();
            Assert.AreEqual(givenValue, parent.Child.Value);
        }

        [TestMethod]
        public void UseSetupsAgain()
        {
            int givenValue = Randomizer<int>.Create();

            var firstChildFiller = new Filler<Child>();
            var childSetup = firstChildFiller.Setup().OnProperty(x => x.Value).Use(givenValue).Result;

            var secondChildFiller = new Filler<Child>();
            secondChildFiller.Setup(childSetup);

            var child = secondChildFiller.Create();

            Assert.AreEqual(givenValue, child.Value);
        }


    }
}
