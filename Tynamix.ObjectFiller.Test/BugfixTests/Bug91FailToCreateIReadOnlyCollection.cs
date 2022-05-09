using System;
using System.Collections.Generic;
using System.Linq;
using Tynamix.ObjectFiller.Test.TestPoco.Library;

namespace Tynamix.ObjectFiller.Test.BugfixTests
{
    using System.Collections;
    using Microsoft.VisualStudio.TestTools.UnitTesting;


    public class OrderWithObject
    {
        public IReadOnlyCollection<Book> OrderLines { get; set; }
    }

    public class Order
    {
        public IReadOnlyCollection<string> OrderLines { get; set; }
    }

    public class OrderDerivation : IReadOnlyCollection<string>
    {
        public string Bla { get; set; }

        public IEnumerator<string> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count { get; }
    }

    [TestClass]
    public class Bug91FailToCreateIReadOnlyCollection
    {
        [TestMethod]
        public void IReadOnlyCollectionShallGetFilled()
        {
            var filler = new Filler<Order>();
            var order = filler.Create();

            Assert.IsTrue(order.OrderLines.Any());
        }

        [TestMethod]
        public void IReadOnlyCollectionWithDerivationShallNotThrowAnException()
        {

            var filler = new Filler<OrderDerivation>();
            filler.Create();
        }

        [TestMethod]
        public void IReadOnlyCollectionWithComplexTypeShallGetFilled()
        {

            var filler = new Filler<OrderWithObject>();
            filler.Setup().SetupFor<Book>().OnProperty(x => x.Name).Use("TEST");

            var order = filler.Create();

            Assert.IsTrue(order.OrderLines.Any());
            Assert.IsTrue(order.OrderLines.All(x => x.Name == "TEST"));

        }
    }
}
