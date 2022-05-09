using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tynamix.ObjectFiller.Test.BugfixTests
{

    [TestClass]
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
