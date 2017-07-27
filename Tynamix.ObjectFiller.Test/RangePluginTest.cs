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
        public void TestIntRangeWithMaxValue()
        {
            int max = 100;
            Filler<SimpleList<int>> filler = new Filler<SimpleList<int>>();

            filler.Setup().OnType<int>().Use(new IntRange(max));
            var sl = filler.Create();

            Assert.IsNotNull(sl);
            Assert.IsNotNull(sl.ChildList);
            Assert.IsTrue(sl.ChildList.All(x => x < 100));
            Assert.IsFalse(sl.ChildList.All(x => x == sl.ChildList[0]));
        }

        [TestMethod]
        public void TestIntRangeWithMinMaxValue()
        {
            int max = 100;
            int min = 50;
            Filler<SimpleList<int>> filler = new Filler<SimpleList<int>>();

            filler.Setup().OnType<int>().Use(new IntRange(min, max));
            var sl = filler.Create();

            Assert.IsNotNull(sl);
            Assert.IsNotNull(sl.ChildList);
            Assert.IsTrue(sl.ChildList.All(x => x >= min && x <= max));
        }

        [TestMethod]
        public void TestFloateRangeWithMinMaxValue()
        {
            int max = 100;
            int min = 50;
            Filler<SimpleList<float>> filler = new Filler<SimpleList<float>>();

            filler.Setup().OnType<float>().Use(new FloatRange(min, max));
            var sl = filler.Create();

            Assert.IsNotNull(sl);
            Assert.IsNotNull(sl.ChildList);
            Assert.IsTrue(sl.ChildList.All(x => x >= min && x <= max));
        }
    }
}
