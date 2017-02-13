using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectFiller.Test
{
        

    using Tynamix.ObjectFiller;

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