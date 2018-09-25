using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tynamix.ObjectFiller.Test.TestPoco.Library;

namespace Tynamix.ObjectFiller.Test
{
    [TestClass]
    public class LoremIpsumPluginTest
    {

        [TestMethod]
        public void Test_With_Many_MinWords_And_Many_MinSentences()
        {
            Filler<Book> book = new Filler<Book>();
            book.Setup()
                .OnProperty(x => x.ISBN).Use(new Lipsum(LipsumFlavor.InDerFremde, 51, 100, 100));

            var b = book.Create();

            Assert.IsNotNull(b);
        }

        [TestMethod]
        public void Test_With_German_Default_Settings()
        {
            Filler<Book> book = new Filler<Book>();
            book.Setup()
                .OnProperty(x => x.ISBN).Use(new Lipsum(LipsumFlavor.InDerFremde));

            var b = book.Create();

            Assert.IsNotNull(b);
        }

        [TestMethod]
        public void Test_With_France_High_Values_Settings()
        {
            Filler<Book> book = new Filler<Book>();
            book.Setup()
                .OnProperty(x => x.ISBN).Use(new Lipsum(LipsumFlavor.LeMasque, 20, 50, 100, 250, 500));

            var b = book.Create();

            Assert.IsNotNull(b);
        }

        [TestMethod]
        public void Test_With_English_Min_Values_Settings()
        {
            Filler<Book> book = new Filler<Book>();
            book.Setup()
                .OnProperty(x => x.ISBN).Use(new Lipsum(LipsumFlavor.ChildHarold, 1, 1, 1, 1, 1));

            var b = book.Create();

            b.ISBN = b.ISBN.Replace("\r\n\r\n", string.Empty);
            Assert.IsNotNull(b);
            Assert.AreEqual(1, b.ISBN.Split('\n').Length);
        }

        [TestMethod]
        public void Test_With_LoremIpsum_Seed_Settings()
        {
            Filler<Book> book = new Filler<Book>();
            book.Setup()
                .OnProperty(x => x.ISBN).Use(new Lipsum(LipsumFlavor.LoremIpsum, 3, 5, 1, 5, 3, 1234));

            var b = book.Create();
            var b1 = book.Create();

            Assert.IsNotNull(b);
            Assert.IsNotNull(b1);
            Assert.AreEqual(b.ISBN, b1.ISBN);
        }

        [TestMethod]
        public void LoremIpsum_should_provide_different_data()
        {
            var alowedDelta = 2;

            var filler = new Filler<Book>();
            filler.Setup()
                .OnProperty(foo => foo.Description)
                .Use(new Lipsum(LipsumFlavor.LoremIpsum));

            var resultElements = filler.Create(100);

            var groupedResult = resultElements.GroupBy(x => x.Description);

            Assert.AreEqual((double)100, groupedResult.Count(), alowedDelta);
        }
    }
}
