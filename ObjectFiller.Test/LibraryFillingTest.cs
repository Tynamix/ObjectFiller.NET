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
                .Ignore(x => x.Books);
            LibraryConstructorWithSimple filledLib = lib.Fill();

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
                .Ignore(x => x.Books, x => x.Name);


            LibraryConstructorList filledLib = lib.Fill();

            Assert.IsNotNull(filledLib);
            Assert.IsNotNull(filledLib.Books);
            Assert.IsNotNull(filledLib.Name);
        }

        [TestMethod]
        public void TestFillLibraryWithListOfIBooks()
        {
            Filler<LibraryConstructorList> lib = new Filler<LibraryConstructorList>();
            lib.Setup()
                .Ignore(x => x.Books)
                .RegisterInterface<IBook, Book>();

            LibraryConstructorList filledLib = lib.Fill();

            Assert.IsNotNull(filledLib.Books);
        }

        [TestMethod]
        public void TestFillLibraryWithPocoOfABook()
        {
            Filler<LibraryConstructorPoco> lib = new Filler<LibraryConstructorPoco>();
            lib.Setup()
               .Ignore(x => x.Books);


            LibraryConstructorPoco filledLib = lib.Fill();
            Assert.IsNotNull(filledLib.Books);
            Assert.AreEqual(1, filledLib.Books.Count);
        }

        [TestMethod]
        public void TestFillLibraryWithDictionary()
        {
            Filler<LibraryConstructorDictionary> lib = new Filler<LibraryConstructorDictionary>();
            lib.Setup()
                .RegisterInterface<IBook, Book>()
                .Ignore(x => x.Books);


            LibraryConstructorDictionary filledLib = lib.Fill();
            Assert.IsNotNull(filledLib.Books);
        }

        [TestMethod]
        public void TestFillLibraryWithDictionaryAndPoco()
        {
            Filler<LibraryConstructorDictionary> lib = new Filler<LibraryConstructorDictionary>();
            lib.Setup()
               .Ignore(x => x.Books, x => x.Name);


            LibraryConstructorDictionary filledLib = lib.Fill();
            Assert.IsNotNull(filledLib.Books);
            Assert.IsNotNull(filledLib.Name);

        }
    }
}