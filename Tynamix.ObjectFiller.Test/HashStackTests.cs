using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tynamix.ObjectFiller.Test
{
    [TestClass]
    public class HashStackTests
    {
        [TestMethod]
        public void HashStack_PushSameItem_ReturnsFalse()
        {
            var s = new HashStack<int>();
            s.Push(1);
            var added = s.Push(1);

            Assert.IsFalse(added);
        }

        [TestMethod]
        public void HashStack_ContainsTest()
        {
            var s = new HashStack<int>();
            s.Push(5);
            Assert.AreEqual(true, s.Contains(5));
        }

        [TestMethod]
        public void HashStack_PopWithNoElements_Throws()
        {
            var s = new HashStack<int>();
            Assert.ThrowsException<InvalidOperationException>(()=> s.Pop());
        }
    }
}