    using Xunit;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{

    public class RealNamePluginTest
    {
        [Fact]
        public void TestRealNameFirstNameOnly()
        {
            Filler<LibraryFillingTest.Person> filler = new Filler<LibraryFillingTest.Person>();
            filler.Setup()
                .OnProperty(x => x.Name).Use(new RealNames(NameStyle.FirstName));

            LibraryFillingTest.Person p = filler.Create();

            Assert.NotNull(p);
            Assert.NotNull(p.Name);
            Assert.False(p.Name.Contains(" "));
        }

        [Fact]
        public void TestRealNameLastNameOnly()
        {
            Filler<LibraryFillingTest.Person> filler = new Filler<LibraryFillingTest.Person>();
            filler.Setup()
                .OnProperty(x => x.Name).Use(new RealNames(NameStyle.LastName));

            LibraryFillingTest.Person p = filler.Create();

            Assert.NotNull(p);
            Assert.NotNull(p.Name);
            Assert.False(p.Name.Contains(" "));
        }

        [Fact]
        public void TestRealNameFirstNameLastName()
        {
            Filler<LibraryFillingTest.Person> filler = new Filler<LibraryFillingTest.Person>();
            filler.Setup()
                .OnProperty(x => x.Name).Use(new RealNames(NameStyle.FirstNameLastName));

            LibraryFillingTest.Person p = filler.Create();

            Assert.NotNull(p);
            Assert.NotNull(p.Name);
            Assert.True(p.Name.Contains(" "));
            Assert.Equal(2, p.Name.Split(' ').Length);
        }

        [Fact]
        public void TestRealNameLastNameFirstName()
        {
            Filler<LibraryFillingTest.Person> filler = new Filler<LibraryFillingTest.Person>();
            filler.Setup()
                .OnProperty(x => x.Name).Use(new RealNames(NameStyle.LastNameFirstName));

            LibraryFillingTest.Person p = filler.Create();

            Assert.NotNull(p);
            Assert.NotNull(p.Name);
            Assert.True(p.Name.Contains(" "));
            Assert.Equal(2, p.Name.Split(' ').Length);
        }
    }
}