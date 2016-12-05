using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectFiller.Test.BugfixTests
{
    using Tynamix.ObjectFiller;

    using Xunit;

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

        [Fact]
        public void ParentShallGetFilledWithourError()
        {
            Filler<Parent> filler = new Filler<Parent>();

            var filledObject = filler.Create();
            Assert.NotNull(filledObject);
            Assert.NotNull(filledObject.MakeTheError);
            Assert.False(string.IsNullOrWhiteSpace(filledObject.Child.MakeTheError));
        }
    }
}

