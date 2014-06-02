using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller.Test.TestPoco;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    [TestClass]
    public class RangePluginTest
    {
        [TestMethod]
        public void TestRangeWithMaxValue()
        {
            int max = 100;
            Filler<SimpleList> filler = new Filler<SimpleList>();

            filler.Setup().OnType<int>().Use(new IntRange(max));
            var sl = filler.Create();

            Assert.IsNotNull(sl);
            Assert.IsNotNull(sl.IntegerList);
            Assert.IsTrue(sl.IntegerList.All(x => x <= 100));
            Assert.IsFalse(sl.IntegerList.All(x => x == sl.IntegerList[0]));
        }

        [TestMethod]
        public void TestRangeWithMinMaxValue()
        {
            int max = 100;
            int min = 50;
            Filler<SimpleList> filler = new Filler<SimpleList>();

            filler.Setup().OnType<int>().Use(new IntRange(min, max));
            var sl = filler.Create();

            Assert.IsNotNull(sl);
            Assert.IsNotNull(sl.IntegerList);
            Assert.IsTrue(sl.IntegerList.All(x => x >= min && x <= max));
        }
    }
}
