using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tynamix.ObjectFiller.Test
{
    public class CollectionizerPoco
    {
        public IEnumerable<string> MnemonicStrings { get; set; }

        public List<int> IntRange { get; set; }

#if !NET6
        public ArrayList ArrayList { get; set; }
#endif
        public string[] StringArray { get; set; }
    }

    [TestClass]
    public class CollectionizerTest
    {
#if !NET6
        [TestMethod]
        public void TestCityNames()
        {
            var filler = new Filler<CollectionizerPoco>();

            filler.Setup().OnProperty(x => x.ArrayList).Use(new Collectionizer<string, MnemonicString>(new MnemonicString(1, 20, 25), 3, 10));

            var arrayList = filler.Create();
            Assert.IsTrue(arrayList.ArrayList.Count >= 3 && arrayList.ArrayList.Count <= 10);
            Assert.IsTrue(arrayList.ArrayList.ToArray().Cast<string>().All(x => x.Length >= 20 && x.Length <= 25));
        }
#endif

        [TestMethod]
        public void TestMnemonicStringPlugin()
        {
            var filler = new Filler<CollectionizerPoco>();

            filler.Setup()
                .OnProperty(x => x.MnemonicStrings).Use(new Collectionizer<string, MnemonicString>(new MnemonicString(1, 20, 25), 3, 10))
                .OnProperty(x => x.StringArray).Use(new Collectionizer<string, MnemonicString>(new MnemonicString(1, 20, 25), 3, 10));

            var collection = filler.Create();
            Assert.IsTrue(collection.MnemonicStrings.Count() >= 3 && collection.MnemonicStrings.Count() <= 10);
            Assert.IsTrue(collection.MnemonicStrings.All(x => x.Length >= 20 && x.Length <= 25));
            Assert.IsTrue(collection.StringArray.Count() >= 3 && collection.MnemonicStrings.Count() <= 10);
            Assert.IsTrue(collection.StringArray.All(x => x.Length >= 20 && x.Length <= 25));

        }

        [TestMethod]
        public void TestMnemonicStringPluginTest()
        {
            var filler = new Filler<CollectionizerPoco>();

            filler.Setup()
                .OnProperty(x => x.MnemonicStrings).Use<Collectionizer<string, MnemonicString>>();

            var collection = filler.Create();

            Assert.IsNotNull(collection);
            Assert.IsNotNull(collection.MnemonicStrings);
            Assert.IsTrue(collection.MnemonicStrings.Any());

            collection.MnemonicStrings.ToList().ForEach(s => Debug.WriteLine(s));
        }

        [TestMethod]
        public void TestIntRangePlugin()
        {
            var filler = new Filler<CollectionizerPoco>();

            filler.Setup()
                .OnProperty(x => x.IntRange)
                .Use(new Collectionizer<int, IntRange>(new IntRange(10, 15), 3, 10));

            var collection = filler.Create();
            Assert.IsTrue(collection.IntRange.Count >= 3 && collection.IntRange.Count() <= 10);
            Assert.IsTrue(collection.IntRange.All(x => x >= 10 && x <= 15));
        }
    }
}
