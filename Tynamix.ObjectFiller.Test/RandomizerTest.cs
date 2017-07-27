using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectFiller.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

        

    using ObjectFiller.Test.TestPoco.Library;
    using ObjectFiller.Test.TestPoco.Person;

    using Tynamix.ObjectFiller;

    [TestClass]
    public class RandomizerTest
    {
        [TestMethod]
        public void GetRandomInt()
        {
            var number = Randomizer<int>.Create(new IntRange(1, 2));

            Assert.IsTrue(number == 1 || number == 2);
        }

        [TestMethod]
        public void FillAllAddressProperties()
        {
            var a = Randomizer<Address>.Create();
            Assert.IsNotNull(a.City);
            Assert.IsNotNull(a.Country);
            Assert.AreNotEqual(0, a.HouseNumber);
            Assert.IsNotNull(a.PostalCode);
            Assert.IsNotNull(a.Street);
        }

        [TestMethod]
        public void TryingToCreateAnObjectWithAnInterfaceShallFailAndHaveAnInnerexception()
        {
            try
            {
                Randomizer<Library>.Create();
            }
            catch (InvalidOperationException ex)
            {
                Assert.IsNotNull(ex.InnerException);
                return;
            }

            // Should not reach this!
            Assert.IsFalse(true);
        }

        [TestMethod]
        public void RandomizerCreatesAListOfRandomItemsIfNeeded()
        {
            int amount = 5;

            IEnumerable<int> result = Randomizer<int>.Create(amount);

            Assert.AreEqual(amount, result.Count());
        }

        [TestMethod]
        public void RandomizerCreatesAListOfRandomItemsWithAPlugin()
        {
            int amount = 5;

            IEnumerable<int> result = Randomizer<int>.Create(new IntRange(1,1), amount);

            Assert.AreEqual(amount, result.Count());
            Assert.IsTrue(result.Count(x => x == 1) == amount);
        }

        [TestMethod]
        public void RandomizerCreatesAListOfItemBasedOnAFactory()
        {
            int amount = 5;

            IEnumerable<int> result = Randomizer<int>.Create(amount, () => 1);

            Assert.AreEqual(amount, result.Count());
            Assert.IsTrue(result.Count(x => x == 1) == amount);
        }
        
        [TestMethod]
        public void RandomizerCreatesAListOfItemBasedOnASetup()
        {
            int amount = 5;

            var setup = FillerSetup.Create<Address>().OnType<int>().Use(1).Result;

            IEnumerable<Address> result = Randomizer<Address>.Create(setup, amount);

            Assert.AreEqual(amount, result.Count());
            Assert.IsTrue(result.Count(x => x.HouseNumber == 1) == amount);
        }
    }
}