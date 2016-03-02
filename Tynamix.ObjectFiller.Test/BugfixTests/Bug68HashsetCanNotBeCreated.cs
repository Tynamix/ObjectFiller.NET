using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectFiller.Test.BugfixTests
{
    using Tynamix.ObjectFiller;

    using Xunit;

    public class Bug68HashsetCanNotBeCreated
    {
        [Fact]
        public void AHashsetShouldBeGenerated()
        {
            Filler<HashSet<string>> filler = new Filler<HashSet<string>>();

            var hashset = filler.Create();

            Assert.NotNull(hashset);
            Assert.True(hashset.Any());
        }
    }
}
