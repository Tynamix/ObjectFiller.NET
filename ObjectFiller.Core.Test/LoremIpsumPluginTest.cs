using System;
using System.Linq;
    using Xunit;
using ObjectFiller.Test.TestPoco.Library;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    using System.Collections.Generic;


    public class LoremIpsumPluginTest
    {

        [Fact]
        public void Test_With_Many_MinWords_And_Many_MinSentences()
        {
            Filler<Book> book = new Filler<Book>();
            book.Setup()
                .OnProperty(x => x.ISBN).Use(new Lipsum(LipsumFlavor.InDerFremde, 3, 9, minWords: 51));

            var b = book.Create();

            Assert.NotNull(b);
        }

        [Fact]
        public void Test_With_German_Default_Settings()
        {
            Filler<Book> book = new Filler<Book>();
            book.Setup()
                .OnProperty(x => x.ISBN).Use(new Lipsum(LipsumFlavor.InDerFremde));

            var b = book.Create();

            Assert.NotNull(b);
        }

        [Fact]
        public void Test_With_France_High_Values_Settings()
        {
            Filler<Book> book = new Filler<Book>();
            book.Setup()
                .OnProperty(x => x.ISBN).Use(new Lipsum(LipsumFlavor.LeMasque, 20, 50, 100, 250, 500));

            var b = book.Create();

            Assert.NotNull(b);
        }

        [Fact]
        public void Test_With_English_Min_Values_Settings()
        {
            Filler<Book> book = new Filler<Book>();
            book.Setup()
                .OnProperty(x => x.ISBN).Use(new Lipsum(LipsumFlavor.ChildHarold, 1, 1, 1, 1, 1));

            var b = book.Create();

            b.ISBN = b.ISBN.Replace("\r\n\r\n", string.Empty);
            Assert.NotNull(b);
            Assert.Equal(1, b.ISBN.Split('\n').Length);
        }

        [Fact]
        public void Test_With_LoremIpsum_Seed_Settings()
        {
            Filler<Book> book = new Filler<Book>();
            book.Setup()
                .OnProperty(x => x.ISBN).Use(new Lipsum(LipsumFlavor.LoremIpsum, seed: 1234));

            var b = book.Create();
            var b1 = book.Create();

            Assert.NotNull(b);
            Assert.NotNull(b1);
            Assert.Equal(b.ISBN, b1.ISBN);
        }

        [Fact]
        public void LoremIpsum_should_provide_different_data()
        {
            var alowedDelta = 2;

            var filler = new Filler<Book>();
            filler.Setup()
                .OnProperty(foo => foo.Description)
                .Use(new Lipsum(LipsumFlavor.LoremIpsum));

            var resultElements = filler.Create(100);

            var groupedResult = resultElements.GroupBy(x => x.Description);

            Assert.Equal((double)100, groupedResult.Count(), alowedDelta);
        }
    }
}
