namespace ObjectFiller.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

        using Xunit;

    using ObjectFiller.Test.TestPoco.Library;
    using ObjectFiller.Test.TestPoco.Person;

    using Tynamix.ObjectFiller;


    public class RandomizerTest
    {
        [Fact]
        public void GetRandomInt()
        {
            var number = Randomizer<int>.Create(new IntRange(1, 2));

            Assert.True(number == 1 || number == 2);
        }

        [Fact]
        public void FillAllAddressProperties()
        {
            var a = Randomizer<Address>.Create();
            Assert.NotNull(a.City);
            Assert.NotNull(a.Country);
            Assert.NotEqual(0, a.HouseNumber);
            Assert.NotNull(a.PostalCode);
            Assert.NotNull(a.Street);
        }

        [Fact]
        public void TryingToCreateAnObjectWithAnInterfaceShallFailAndHaveAnInnerexception()
        {
            try
            {
                Randomizer<Library>.Create();
            }
            catch (InvalidOperationException ex)
            {
                Assert.NotNull(ex.InnerException);
                return;
            }

            ///Should not come here!
            Assert.False(true);
        }

        [Fact]
        public void RandomizerCreatesAListOfRandomItemsIfNeeded()
        {
            int amount = 5;

            IEnumerable<int> result = Randomizer<int>.Create(amount);

            Assert.Equal(amount, result.Count());
        }

    }
}