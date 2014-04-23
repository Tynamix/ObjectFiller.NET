using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller.Test.TestPoco.Library;
using Tynamix.ObjectFiller;
using Tynamix.ObjectFiller.Plugins;

namespace ObjectFiller.Test
{
    [TestClass]
    public class LoremIpsumPluginTest
    {
        [TestMethod]
        public void TestPlugin()
        {
            int isbnWordCount = 1000;
            int nameWordCount = 500;
            ObjectFiller<Book> bookFill = new ObjectFiller<Book>();
            bookFill.Setup()
                .RandomizerForProperty(new LoremIpsumPlugin(isbnWordCount), x => x.ISBN)
                .RandomizerForProperty(new LoremIpsumPlugin(nameWordCount), x => x.Name);

            Book book = bookFill.Fill();

            Assert.IsNotNull(book);
            Assert.IsNotNull(book.ISBN);
            Assert.IsNotNull(book.Name);
            Assert.AreEqual(isbnWordCount, book.ISBN.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length);
            Assert.AreEqual(nameWordCount, book.Name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length);
        }
    }
}
