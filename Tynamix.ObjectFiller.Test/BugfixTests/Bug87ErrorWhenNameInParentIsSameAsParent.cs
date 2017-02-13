using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectFiller.Test.BugfixTests
{
    using Tynamix.ObjectFiller;

    

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

            var filledObject = filler.Create();
            Assert.IsNotNull(filledObject);
            Assert.IsNotNull(filledObject.MakeTheError);
            Assert.IsFalse(string.IsNullOrWhiteSpace(filledObject.Child.MakeTheError));
        }
    }
}

