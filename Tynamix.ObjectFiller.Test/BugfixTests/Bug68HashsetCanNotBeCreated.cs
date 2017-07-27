using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectFiller.Test.BugfixTests
{
    using Tynamix.ObjectFiller;

    

    public class Bug68HashsetCanNotBeCreated
    {
        [TestMethod]
        public void AHashsetShouldBeGenerated()
        {
            Filler<HashSet<string>> filler = new Filler<HashSet<string>>();

            var hashset = filler.Create();

            Assert.IsNotNull(hashset);
            Assert.IsTrue(hashset.Any());
        }
    }
}
