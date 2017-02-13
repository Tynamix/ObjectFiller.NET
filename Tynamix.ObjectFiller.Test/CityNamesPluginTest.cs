using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectFiller.Test
{
    

    using Tynamix.ObjectFiller;

    [TestClass]
    public class CityNamesPluginTest
    {
        [TestMethod]
        public void RandomNameIsReturned()
        {
            var sut = new CityName();
            var value = sut.GetValue();

            Assert.IsFalse(string.IsNullOrEmpty(value));
        }
    }
}