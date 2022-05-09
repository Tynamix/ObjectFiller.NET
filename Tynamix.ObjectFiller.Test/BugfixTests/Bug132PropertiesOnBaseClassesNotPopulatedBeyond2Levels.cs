using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tynamix.ObjectFiller.Test.BugfixTests
{
    [TestClass]
    public class Bug132PropertiesOnBaseClassesNotPopulatedBeyond2Levels
    {
        public class BaseId
        {
            public string Id { get; set; }
        }

        public class BaseWithAudit : BaseId
        {
            public string CreatedBy { get; set; }
            public string UpdatedBy { get; set; }
        }

        public class Person : BaseWithAudit
        {
            public string Name { get; set; }
            public string LastName { get; set; }
        }
        [TestMethod]
        public void Bug132PropertiesOnBaseClassNotPopulated()
        {
            var filler = new Filler<Person>();
            var x = filler.Create();
            Assert.IsFalse(string.IsNullOrWhiteSpace(x.Name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(x.LastName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(x.CreatedBy));
            Assert.IsFalse(string.IsNullOrWhiteSpace(x.UpdatedBy));
            Assert.IsFalse(string.IsNullOrWhiteSpace(x.Id));
        }
    }
}
