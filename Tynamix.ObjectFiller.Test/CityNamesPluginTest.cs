using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tynamix.ObjectFiller.Test
{
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