using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tynamix.ObjectFiller.Test.TestPoco;

namespace Tynamix.ObjectFiller.Test
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

        [TestMethod]
        public void TestLongRangeWithMaxValue()
        {
            long max = int.MaxValue * 10L;
            Filler<SimpleList<long>> filler = new Filler<SimpleList<long>>();

            filler.Setup().OnType<long>().Use(new LongRange(max));
            var sl = filler.Create();

            Assert.IsNotNull(sl);
            Assert.IsNotNull(sl.ChildList);
            Assert.IsTrue(sl.ChildList.All(x => x < max));
            Assert.IsFalse(sl.ChildList.All(x => x == sl.ChildList[0]));
        }

        [TestMethod]
        public void TestLongRangeWithMinMaxValue()
        {
            long min = int.MinValue * 10L;
            long max = int.MaxValue * 10L;
            Filler<SimpleList<long>> filler = new Filler<SimpleList<long>>();

            filler.Setup().OnType<long>().Use(new LongRange(min, max));
            var sl = filler.Create();

            Assert.IsNotNull(sl);
            Assert.IsNotNull(sl.ChildList);
            Assert.IsTrue(sl.ChildList.All(x => x >= min && x <= max));
            Assert.IsFalse(sl.ChildList.All(x => x == sl.ChildList[0]));
        }

        [TestMethod]
        public void TestLongRangeWithMinMaxValueLowSmallRange()
        {
            long min = long.MinValue;
            long max = long.MinValue + 10;
            Filler<SimpleList<long>> filler = new Filler<SimpleList<long>>();

            filler.Setup().OnType<long>().Use(new LongRange(min, max));
            var sl = filler.Create();

            Assert.IsNotNull(sl);
            Assert.IsNotNull(sl.ChildList);
            Assert.IsTrue(sl.ChildList.All(x => x >= min && x <= max));
            Assert.IsFalse(sl.ChildList.All(x => x == sl.ChildList[0]));
        }

        [TestMethod]
        public void TestLongRangeWithMinMaxValueHighSmallRange()
        {
            long min = long.MaxValue - 10;
            long max = long.MaxValue;
            Filler<SimpleList<long>> filler = new Filler<SimpleList<long>>();

            filler.Setup().OnType<long>().Use(new LongRange(min, max));
            var sl = filler.Create();

            Assert.IsNotNull(sl);
            Assert.IsNotNull(sl.ChildList);
            Assert.IsTrue(sl.ChildList.All(x => x >= min && x <= max));
            Assert.IsFalse(sl.ChildList.All(x => x == sl.ChildList[0]));
        }

    }
}
