using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    [TestClass]
    public class CopyConstructorTest
    {

        [TestMethod]
        public void WhenClassWithCopyConstructorIsCreatedNoExceptionShallBeThrown()
        {
            var f = new Filler<ClassWithCopyConstructorAndNormalConstructor>();
            var cc = f.Create();

            Assert.IsNotNull(cc);
        }

        [TestMethod]
        public void WhenClassWithJustCopyConstructorIsCreatedAExceptionShallBeThrown()
        {
            var f = new Filler<ClassWithCopyConstructor>();
            Assert.ThrowsException<InvalidOperationException>(() => f.Create());

        }
    }

    public class ClassWithCopyConstructor
    {

        public ClassWithCopyConstructor(ClassWithCopyConstructor constructor)
        {
            Count1 = constructor.Count1;
            Count2 = constructor.Count2;
        }

        public int Count1 { get; set; }

        public int Count2 { get; set; }
    }


    public class ClassWithCopyConstructorAndNormalConstructor
    {
        public ClassWithCopyConstructorAndNormalConstructor(int count1, int count2)
        {
            Count1 = count1;
            Count2 = count2;
        }

        public ClassWithCopyConstructorAndNormalConstructor(ClassWithCopyConstructorAndNormalConstructor constructor)
            : this(constructor.Count1, constructor.Count2)
        {

        }

        public int Count1 { get; set; }

        public int Count2 { get; set; }
    }
}