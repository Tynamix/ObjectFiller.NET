    
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    [TestClass]
    public class RealNamePluginTest
    {
        [TestMethod]
        public void TestRealNameFirstNameOnly()
        {
            Filler<LibraryFillingTest.Person> filler = new Filler<LibraryFillingTest.Person>();
            filler.Setup()
                .OnProperty(x => x.Name).Use(new RealNames(NameStyle.FirstName));

            LibraryFillingTest.Person p = filler.Create();

            Assert.IsNotNull(p);
            Assert.IsNotNull(p.Name);
            Assert.IsFalse(p.Name.Contains(" "));
        }

        [TestMethod]
        public void TestRealNameLastNameOnly()
        {
            Filler<LibraryFillingTest.Person> filler = new Filler<LibraryFillingTest.Person>();
            filler.Setup()
                .OnProperty(x => x.Name).Use(new RealNames(NameStyle.LastName));

            LibraryFillingTest.Person p = filler.Create();

            Assert.IsNotNull(p);
            Assert.IsNotNull(p.Name);
            Assert.IsFalse(p.Name.Contains(" "));
        }

        [TestMethod]
        public void TestRealNameFirstNameLastName()
        {
            Filler<LibraryFillingTest.Person> filler = new Filler<LibraryFillingTest.Person>();
            filler.Setup()
                .OnProperty(x => x.Name).Use(new RealNames(NameStyle.FirstNameLastName));

            LibraryFillingTest.Person p = filler.Create();

            Assert.IsNotNull(p);
            Assert.IsNotNull(p.Name);
            Assert.IsTrue(p.Name.Contains(" "));
            Assert.AreEqual(2, p.Name.Split(' ').Length);
        }

        [TestMethod]
        public void TestRealNameLastNameFirstName()
        {
            Filler<LibraryFillingTest.Person> filler = new Filler<LibraryFillingTest.Person>();
            filler.Setup()
                .OnProperty(x => x.Name).Use(new RealNames(NameStyle.LastNameFirstName));

            LibraryFillingTest.Person p = filler.Create();

            Assert.IsNotNull(p);
            Assert.IsNotNull(p.Name);
            Assert.IsTrue(p.Name.Contains(" "));
            Assert.AreEqual(2, p.Name.Split(' ').Length);
        }
    }
}