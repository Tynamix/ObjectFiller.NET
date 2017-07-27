using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller.Test.TestPoco.Library;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    [TestClass]
    public class LibraryFillingTest
    {
        [TestMethod]
        public void TestFillLibraryWithSimpleTypes()
        {
            Filler<LibraryConstructorWithSimple> lib = new Filler<LibraryConstructorWithSimple>();
            lib.Setup()
                .OnProperty(x => x.Books).IgnoreIt();
            LibraryConstructorWithSimple filledLib = lib.Create();

            Assert.IsNull(filledLib.Books);
            Assert.IsNotNull(filledLib);
            Assert.IsNotNull(filledLib.City);
            Assert.IsNotNull(filledLib.Name);
        }

        [TestMethod]
        public void TestFillLibraryWithListOfBooks()
        {
            Filler<LibraryConstructorList> lib = new Filler<LibraryConstructorList>();
            lib.Setup()
                .OnProperty(x => x.Books).IgnoreIt()
                .OnProperty(x => x.Name).IgnoreIt();

            LibraryConstructorList filledLib = lib.Create();

            Assert.IsNotNull(filledLib);
            Assert.IsNotNull(filledLib.Books);
            Assert.IsNotNull(filledLib.Name);
        }

        [TestMethod]
        public void TestFillLibraryWithListOfIBooks()
        {
            Filler<LibraryConstructorList> lib = new Filler<LibraryConstructorList>();
            lib.Setup()
                .OnProperty(x => x.Books).IgnoreIt()
                .OnType<IBook>().CreateInstanceOf<Book>();

            LibraryConstructorList filledLib = lib.Create();

            Assert.IsNotNull(filledLib.Books);
        }

        [TestMethod]
        public void TestFillLibraryWithPocoOfABook()
        {
            Filler<LibraryConstructorPoco> lib = new Filler<LibraryConstructorPoco>();
            lib.Setup()
                .OnProperty(x => x.Books).IgnoreIt();

            LibraryConstructorPoco filledLib = lib.Create();
            Assert.IsNotNull(filledLib.Books);
            Assert.AreEqual(1, filledLib.Books.Count);
        }

        [TestMethod]
        public void TestFillLibraryWithConfiguredPocoOfABook()
        {
            Filler<LibraryConstructorPoco> lib = new Filler<LibraryConstructorPoco>();
            lib.Setup()
                .OnProperty(x => x.Books).IgnoreIt()
                .SetupFor<Book>()
                .OnProperty(x => x.Name).Use(() => "ABook");
            

            var l = lib.Create();

            Assert.AreEqual("ABook", ((Book)l.Books.ToList()[0]).Name);

        }

        [TestMethod]
        public void TestFillLibraryWithDictionary()
        {
            Filler<LibraryConstructorDictionary> lib = new Filler<LibraryConstructorDictionary>();
            lib.Setup()
                .OnType<IBook>().CreateInstanceOf<Book>()
                .OnProperty(x => x.Books).IgnoreIt();

            LibraryConstructorDictionary filledLib = lib.Create();
            Assert.IsNotNull(filledLib.Books);
        }

        [TestMethod]
        public void TestFillLibraryWithDictionaryAndPoco()
        {
            Filler<LibraryConstructorDictionary> lib = new Filler<LibraryConstructorDictionary>();
            lib.Setup()
                .OnProperty(x => x.Books).IgnoreIt()
                .OnProperty(x => x.Name).IgnoreIt();


            LibraryConstructorDictionary filledLib = lib.Create();
            Assert.IsNotNull(filledLib.Books);
            Assert.IsNotNull(filledLib.Name);

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
