using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tynamix.ObjectFiller.Test.BugfixTests
{
    [TestClass]
    public class Bug87ErrorWhenNameInParentIsSameAsParent
    {
        public class Parent
        {
            public int MakeTheError { get; set; }

            public Child Child { get; set; }

        }

        public class Child
        {
            public string MakeTheError { get; set; }
        }

        [TestMethod]
        public void ParentShallGetFilledWithourError()
        {
            Filler<Parent> filler = new Filler<Parent>();
            filler.Setup()
                  .OnProperty(x => x.MakeTheError).Use(12345)
                  .SetupFor<Child>()
                  .OnProperty(x => x.MakeTheError).Use("TEST");

            var filledObject = filler.Create();
            Assert.IsNotNull(filledObject);
            Assert.AreEqual(12345, filledObject.MakeTheError);
            Assert.IsFalse(string.IsNullOrWhiteSpace(filledObject.Child.MakeTheError));
            Assert.AreEqual("TEST", filledObject.Child.MakeTheError);
        }
    }
}

