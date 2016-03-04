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

            // Should not reach this!
            Assert.False(true);
        }

        [Fact]
        public void RandomizerCreatesAListOfRandomItemsIfNeeded()
        {
            int amount = 5;

            IEnumerable<int> result = Randomizer<int>.Create(amount);

            Assert.Equal(amount, result.Count());
        }

        [Fact]
        public void RandomizerCreatesAListOfRandomItemsWithAPlugin()
        {
            int amount = 5;

            IEnumerable<int> result = Randomizer<int>.Create(new IntRange(1,1), amount);

            Assert.Equal(amount, result.Count());
            Assert.True(result.Count(x => x == 1) == amount);
        }

        [Fact]
        public void RandomizerCreatesAListOfItemBasedOnAFactory()
        {
            int amount = 5;

            IEnumerable<int> result = Randomizer<int>.Create(amount, () => 1);

            Assert.Equal(amount, result.Count());
            Assert.True(result.Count(x => x == 1) == amount);
        }
        
        [Fact]
        public void RandomizerCreatesAListOfItemBasedOnASetup()
        {
            int amount = 5;

            var setup = FillerSetup.Create<Address>().OnType<int>().Use(1).Result;

            IEnumerable<Address> result = Randomizer<Address>.Create(setup, amount);

            Assert.Equal(amount, result.Count());
            Assert.True(result.Count(x => x.HouseNumber == 1) == amount);
        }
    }
}