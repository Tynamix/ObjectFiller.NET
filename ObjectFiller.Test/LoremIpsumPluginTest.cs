using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller.Test.TestPoco.Library;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    [TestClass]
    public class LoremIpsumPluginTest
    {

        [TestMethod]
        public void Test_With_Many_MinWords_And_Many_MinSentences()
        {
            Filler<Book> book = new Filler<Book>();
            book.Setup()
                .OnProperty(x => x.ISBN).Use(new Lipsum(LipsumFlavor.InDerFremde, 3, 9, minWords: 51));

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
                .OnProperty(x => x.ISBN).Use(new Lipsum(LipsumFlavor.LoremIpsum, seed: 1234));

            var b = book.Create();
            var b1 = book.Create();

            Assert.IsNotNull(b);
            Assert.IsNotNull(b1);
            Assert.AreEqual(b.ISBN, b1.ISBN);
        }
    }
}
