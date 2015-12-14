using System;
using Xunit;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{

    public class HashStackTests
    {
        [Fact]
        public void HashStack_PushSameItem_ReturnsFalse()
        {
            var s = new HashStack<int>();
            s.Push(1);
            var added = s.Push(1);

            Assert.False(added);
        }

        [Fact]
        public void HashStack_ContainsTest()
        {
            var s = new HashStack<int>();
            s.Push(5);
            Assert.Equal(true, s.Contains(5));
        }

        [Fact]
        public void HashStack_PopWithNoElements_Throws()
        {
            var s = new HashStack<int>();
            Assert.Throws<InvalidOperationException>(()=> s.Pop());
        }
    }
}