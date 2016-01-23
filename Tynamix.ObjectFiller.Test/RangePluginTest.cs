using System.Linq;
    using Xunit;
using ObjectFiller.Test.TestPoco;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{

    public class RangePluginTest
    {
        [Fact]
        public void TestRangeWithMaxValue()
        {
            int max = 100;
            Filler<SimpleList> filler = new Filler<SimpleList>();

            filler.Setup().OnType<int>().Use(new IntRange(max));
            var sl = filler.Create();

            Assert.NotNull(sl);
            Assert.NotNull(sl.IntegerList);
            Assert.True(sl.IntegerList.All(x => x < 100));
            Assert.False(sl.IntegerList.All(x => x == sl.IntegerList[0]));
        }

        [Fact]
        public void TestRangeWithMinMaxValue()
        {
            int max = 100;
            int min = 50;
            Filler<SimpleList> filler = new Filler<SimpleList>();

            filler.Setup().OnType<int>().Use(new IntRange(min, max));
            var sl = filler.Create();

            Assert.NotNull(sl);
            Assert.NotNull(sl.IntegerList);
            Assert.True(sl.IntegerList.All(x => x >= min && x <= max));
        }
    }
}
