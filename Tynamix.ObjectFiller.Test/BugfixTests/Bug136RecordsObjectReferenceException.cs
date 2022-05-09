using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if NET6_0_OR_GREATER
namespace Tynamix.ObjectFiller.Test.BugfixTests
{
    public record Product
    {
        public string Name { get; init; }
        public int CategoryId { get; set; }
    }

    public record Person(string Name, string Username);

    [TestClass]
    public class Bug136RecordsObjectReferenceException
    {
        [TestMethod]
        public void RecordsShouldBeFilled()
        {
            Filler<Product> filler = new Filler<Product>();
            var product = filler.Create();
            Assert.IsNotNull(product);
            Assert.IsFalse(string.IsNullOrWhiteSpace(product.Name));

            var filler2 = new Filler<Person>();
            var person = filler2.Create();
            Assert.IsNotNull(person);
            Assert.IsFalse(string.IsNullOrWhiteSpace(person.Name));
            Assert.IsFalse(string.IsNullOrWhiteSpace(person.Username));
        }
    }
}
#endif
