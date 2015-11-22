using System;
using System.Linq;
using Xunit;
using ObjectFiller.Test.TestPoco.Library;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{

    public class LibraryFillingTest
    {
        [Fact]
        public void TestFillLibraryWithSimpleTypes()
        {
            Filler<LibraryConstructorWithSimple> lib = new Filler<LibraryConstructorWithSimple>();
            lib.Setup()
                .OnProperty(x => x.Books).IgnoreIt();
            LibraryConstructorWithSimple filledLib = lib.Create();

            Assert.Null(filledLib.Books);
            Assert.NotNull(filledLib);
            Assert.NotNull(filledLib.City);
            Assert.NotNull(filledLib.Name);
        }

        [Fact]
        public void TestFillLibraryWithListOfBooks()
        {
            Filler<LibraryConstructorList> lib = new Filler<LibraryConstructorList>();
            lib.Setup()
                .OnProperty(x => x.Books).IgnoreIt()
                .OnProperty(x => x.Name).IgnoreIt();

            LibraryConstructorList filledLib = lib.Create();

            Assert.NotNull(filledLib);
            Assert.NotNull(filledLib.Books);
            Assert.NotNull(filledLib.Name);
        }

        [Fact]
        public void TestFillLibraryWithListOfIBooks()
        {
            Filler<LibraryConstructorList> lib = new Filler<LibraryConstructorList>();
            lib.Setup()
                .OnProperty(x => x.Books).IgnoreIt()
                .OnType<IBook>().CreateInstanceOf<Book>();

            LibraryConstructorList filledLib = lib.Create();

            Assert.NotNull(filledLib.Books);
        }

        [Fact]
        public void TestFillLibraryWithPocoOfABook()
        {
            Filler<LibraryConstructorPoco> lib = new Filler<LibraryConstructorPoco>();
            lib.Setup()
                .OnProperty(x => x.Books).IgnoreIt();

            LibraryConstructorPoco filledLib = lib.Create();
            Assert.NotNull(filledLib.Books);
            Assert.Equal(1, filledLib.Books.Count);
        }

        [Fact]
        public void TestFillLibraryWithConfiguredPocoOfABook()
        {
            Filler<LibraryConstructorPoco> lib = new Filler<LibraryConstructorPoco>();
            lib.Setup()
                .OnProperty(x => x.Books).IgnoreIt()
                .SetupFor<Book>()
                .OnProperty(x => x.Name).Use(() => "ABook");
            

            var l = lib.Create();

            Assert.Equal("ABook", ((Book)l.Books.ToList()[0]).Name);

        }

        [Fact]
        public void TestFillLibraryWithDictionary()
        {
            Filler<LibraryConstructorDictionary> lib = new Filler<LibraryConstructorDictionary>();
            lib.Setup()
                .OnType<IBook>().CreateInstanceOf<Book>()
                .OnProperty(x => x.Books).IgnoreIt();

            LibraryConstructorDictionary filledLib = lib.Create();
            Assert.NotNull(filledLib.Books);
        }

        [Fact]
        public void TestFillLibraryWithDictionaryAndPoco()
        {
            Filler<LibraryConstructorDictionary> lib = new Filler<LibraryConstructorDictionary>();
            lib.Setup()
                .OnProperty(x => x.Books).IgnoreIt()
                .OnProperty(x => x.Name).IgnoreIt();


            LibraryConstructorDictionary filledLib = lib.Create();
            Assert.NotNull(filledLib.Books);
            Assert.NotNull(filledLib.Name);

        }


        public class Person
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public DateTime Birthday { get; set; }
        }

        public class HelloFiller
        {
            public void FillPerson()
            {
                Person person = new Person();

                Filler<Person> pFiller = new Filler<Person>();
                Person p = pFiller.Fill(person);
            }
        }

    }
}
