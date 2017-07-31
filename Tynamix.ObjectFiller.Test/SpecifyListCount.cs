using System.Collections.Generic;

namespace Tynamix.ObjectFiller.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class SimpleList
    {
        public List<string> Strings10 { get; set; }
        public ICollection<string> Strings2050 { get; set; }
        public string[] Strings50100 { get; set; }

        public int Item { get; set; }
    }

    [TestClass]
    public class TestSpecifyListCount
    {

        [TestMethod]
        public void Generate10Strings()
        {
            Filler<SimpleList> filler = new Filler<SimpleList>();

            filler.Setup()
                .OnProperty(x => x.Strings10).Use(new Collectionizer<string, CityName>(1,10))
                .OnProperty(x => x.Strings2050).Use(new Collectionizer<string, StreetName>(20, 50))
                .OnProperty(x => x.Strings50100).Use(new Collectionizer<string, RealNames>(50, 100))
                .OnProperty(x => x.Item).Use(1);

            var result = filler.Create();

            Assert.IsTrue(result.Strings10.Count > 0 && result.Strings10.Count <= 10);
            Assert.IsTrue(result.Strings2050.Count >= 20 && result.Strings2050.Count <= 50);
            Assert.IsTrue(result.Strings50100.Length >= 50 && result.Strings50100.Length <= 100);
        }
    }
}
