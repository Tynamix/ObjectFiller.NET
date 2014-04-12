using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller.Test.TestPoco.Library;

namespace ObjectFiller.Test
{
    [TestClass]
    public class LibraryFillingTest
    {
        [TestMethod]
        public void TestFillLibraryWithSimpleTypes()
        {
            ObjectFiller<LibraryConstructorWithSimple> lib = new ObjectFiller<LibraryConstructorWithSimple>();
            lib.Setup()
                .IgnoreProperties(x => x.Books);
            LibraryConstructorWithSimple filledLib = lib.Fill();

            Assert.IsNull(filledLib.Books);
            Assert.IsNotNull(filledLib);
            Assert.IsNotNull(filledLib.City);
            Assert.IsNotNull(filledLib.Name);
        }

        [TestMethod]
        public void TestFillLibraryWithListOfBooks()
        {
            ObjectFiller<LibraryConstructorList> lib = new ObjectFiller<LibraryConstructorList>();
            lib.Setup()
                .IgnoreProperties(x => x.Books, x => x.Name);


            LibraryConstructorList filledLib = lib.Fill();

            Assert.IsNotNull(filledLib);
            Assert.IsNotNull(filledLib.Books);
            Assert.IsNotNull(filledLib.Name);
        }

        [TestMethod]
        public void TestFillLibraryWithListOfIBooks()
        {
            ObjectFiller<LibraryConstructorList> lib = new ObjectFiller<LibraryConstructorList>();
            lib.Setup()
                .IgnoreProperties(x => x.Books)
                .RegisterInterface<IBook, Book>();

            LibraryConstructorList filledLib = lib.Fill();

            Assert.IsNotNull(filledLib.Books);
        }

        [TestMethod]
        public void TestFillLibraryWithPocoOfABook()
        {
            ObjectFiller<LibraryConstructorPoco> lib = new ObjectFiller<LibraryConstructorPoco>();
            lib.Setup()
               .IgnoreProperties(x => x.Books);


            LibraryConstructorPoco filledLib = lib.Fill();
            Assert.IsNotNull(filledLib.Books);
            Assert.AreEqual(1, filledLib.Books.Count);
        }

        [TestMethod]
        public void TestFillLibraryWithDictionary()
        {
            ObjectFiller<LibraryConstructorDictionary> lib = new ObjectFiller<LibraryConstructorDictionary>();
            lib.Setup()
                .RegisterInterface<IBook, Book>()
                .IgnoreProperties(x => x.Books);


            LibraryConstructorDictionary filledLib = lib.Fill();
            Assert.IsNotNull(filledLib.Books);
        }

        [TestMethod]
        public void TestFillLibraryWithDictionaryAndPoco()
        {
            ObjectFiller<LibraryConstructorDictionary> lib = new ObjectFiller<LibraryConstructorDictionary>();
            lib.Setup()
               .IgnoreProperties(x => x.Books, x => x.Name);


            LibraryConstructorDictionary filledLib = lib.Fill();
            Assert.IsNotNull(filledLib.Books);
            Assert.IsNotNull(filledLib.Name);

        }
    }
}