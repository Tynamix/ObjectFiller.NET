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
                .OnProperty(x => x.Books).IgnoreIt()
                .OnProperty(x => x.Name).IgnoreIt();

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
                .OnProperty(x => x.Books).IgnoreIt()
                .OnType<IBook>().Register<Book>();

            LibraryConstructorList filledLib = lib.Fill();

            Assert.IsNotNull(filledLib.Books);
        }

        [TestMethod]
        public void TestFillLibraryWithPocoOfABook()
        {
            Filler<LibraryConstructorPoco> lib = new Filler<LibraryConstructorPoco>();
            lib.Setup()
                .OnProperty(x=>x.Books).IgnoreIt();

            LibraryConstructorPoco filledLib = lib.Fill();
            Assert.IsNotNull(filledLib.Books);
            Assert.AreEqual(1, filledLib.Books.Count);
        }

        [TestMethod]
        public void TestFillLibraryWithDictionary()
        {
            Filler<LibraryConstructorDictionary> lib = new Filler<LibraryConstructorDictionary>();
            lib.Setup()
                .OnType<IBook>().Register<Book>()
                .OnProperty(x=>x.Books).IgnoreIt();

            LibraryConstructorDictionary filledLib = lib.Fill();
            Assert.IsNotNull(filledLib.Books);
        }

        [TestMethod]
        public void TestFillLibraryWithDictionaryAndPoco()
        {
            Filler<LibraryConstructorDictionary> lib = new Filler<LibraryConstructorDictionary>();
            lib.Setup()
                .OnProperty(x => x.Books).IgnoreIt()
                .OnProperty(x => x.Name).IgnoreIt();


            LibraryConstructorDictionary filledLib = lib.Fill();
            Assert.IsNotNull(filledLib.Books);
            Assert.IsNotNull(filledLib.Name);

        }
    }
}