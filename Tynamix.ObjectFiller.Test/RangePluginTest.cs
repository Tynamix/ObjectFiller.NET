using System.Linq;
    using Xunit;
using ObjectFiller.Test.TestPoco;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{

    public class RangePluginTest
    {
        [Fact]
        public void TestIntRangeWithMaxValue()
        {
            int max = 100;
            Filler<SimpleList<int>> filler = new Filler<SimpleList<int>>();

            filler.Setup().OnType<int>().Use(new IntRange(max));
            var sl = filler.Create();

            Assert.NotNull(sl);
            Assert.NotNull(sl.ChildList);
            Assert.True(sl.ChildList.All(x => x < 100));
            Assert.False(sl.ChildList.All(x => x == sl.ChildList[0]));
        }

        [Fact]
        public void TestIntRangeWithMinMaxValue()
        {
            int max = 100;
            int min = 50;
            Filler<SimpleList<int>> filler = new Filler<SimpleList<int>>();

            filler.Setup().OnType<int>().Use(new IntRange(min, max));
            var sl = filler.Create();

            Assert.NotNull(sl);
            Assert.NotNull(sl.ChildList);
            Assert.True(sl.ChildList.All(x => x >= min && x <= max));
        }

        [Fact]
        public void TestFloateRangeWithMinMaxValue()
        {
            int max = 100;
            int min = 50;
            Filler<SimpleList<float>> filler = new Filler<SimpleList<float>>();

            filler.Setup().OnType<float>().Use(new FloatRange(min, max));
            var sl = filler.Create();

            Assert.NotNull(sl);
            Assert.NotNull(sl.ChildList);
            Assert.True(sl.ChildList.All(x => x >= min && x <= max));
        }
    }
}
