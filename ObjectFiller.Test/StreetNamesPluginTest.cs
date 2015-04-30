using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tynamix.ObjectFiller.Plugins.String;

namespace ObjectFiller.Test
{
    using Tynamix.ObjectFiller;

    [TestClass]
    public class StreetNamesPluginTest
    {
        [TestMethod]
        public void RandomNameIsReturned()
        {
            var sut = new StreetName(City.Dresden);
            var value = sut.GetValue();
            Assert.IsFalse(string.IsNullOrEmpty(value));


            sut = new StreetName(City.NewYork);
            value = sut.GetValue();
            Assert.IsFalse(string.IsNullOrEmpty(value));

            sut = new StreetName(City.London);
            value = sut.GetValue();
            Assert.IsFalse(string.IsNullOrEmpty(value));

            sut = new StreetName(City.Moscow);
            value = sut.GetValue();
            Assert.IsFalse(string.IsNullOrEmpty(value));

            sut = new StreetName(City.Paris);
            value = sut.GetValue();
            Assert.IsFalse(string.IsNullOrEmpty(value));

            sut = new StreetName(City.Tokyo);
            value = sut.GetValue();
            Assert.IsFalse(string.IsNullOrEmpty(value));
        }
    }
}
