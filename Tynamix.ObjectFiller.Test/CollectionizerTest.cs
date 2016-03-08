using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tynamix.ObjectFiller;
using Xunit;

namespace ObjectFiller.Test
{
    public class CollectionizerPoco
    {
        public IEnumerable<string> MnemonicStrings { get; set; }

        public List<int> IntRange { get; set; }

        public ArrayList ArrayList { get; set; }
    }

    public class CollectionizerTest
    {
        [Fact]
        public void TestCityNames()
        {
            var filler = new Filler<CollectionizerPoco>();

            filler.Setup()
                .OnProperty(x => x.ArrayList)
                .Use(new Collectionizer<string, MnemonicString>(new MnemonicString(1, 20, 25), 3, 10));

            var arrayList = filler.Create();
            Assert.True(arrayList.ArrayList.Count >= 3 && arrayList.ArrayList.Count <= 10);
            Assert.True(arrayList.ArrayList.ToArray().Cast<string>().All(x => x.Length >= 20 && x.Length <= 25));
        }

        [Fact]
        public void TestMnemonicStringPlugin()
        {
            var filler = new Filler<CollectionizerPoco>();

            filler.Setup()
                .OnProperty(x => x.MnemonicStrings)
                .Use(new Collectionizer<string, MnemonicString>(new MnemonicString(1, 20, 25), 3, 10));

            var collection = filler.Create();
            Assert.True(collection.MnemonicStrings.Count() >= 3 && collection.MnemonicStrings.Count() <= 10);
            Assert.True(collection.MnemonicStrings.All(x => x.Length >= 20 && x.Length <= 25));
        }

        [Fact]
        public void TestIntRangePlugin()
        {
            var filler = new Filler<CollectionizerPoco>();

            filler.Setup()
                .OnProperty(x => x.IntRange)
                .Use(new Collectionizer<int, IntRange>(new IntRange(10, 15), 3, 10));

            var collection = filler.Create();
            Assert.True(collection.IntRange.Count >= 3 && collection.IntRange.Count() <= 10);
            Assert.True(collection.IntRange.All(x => x >= 10 && x <= 15));
        }
    }
}
