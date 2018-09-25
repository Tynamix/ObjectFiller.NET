using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tynamix.ObjectFiller.Test
{
    [TestClass]
    public class CountryNamesPlugin
    {
        [TestMethod]
        public void RandomNameIsReturned()
        {
            var sut = new CountryName();
            var value = sut.GetValue();

            Assert.IsFalse(string.IsNullOrEmpty(value));
        }
    }
}