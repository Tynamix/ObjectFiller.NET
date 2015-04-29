namespace ObjectFiller.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        [ExpectedException(typeof(InvalidOperationException))]
        public void TryingToCreateAnObjectWithAnInterfaceShallFailAndHaveAnInnerexception()
        {
            try
            {
                Randomizer<Library>.Create();
            }
            catch (InvalidOperationException ex)
            {
                Assert.IsNotNull(ex.InnerException);
                throw;
            }
            
        }

    }
}