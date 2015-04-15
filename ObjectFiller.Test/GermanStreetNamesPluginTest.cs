using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tynamix.ObjectFiller.Plugins.String;

namespace ObjectFiller.Test
{
    [TestClass]
    public class GermanStreetNamesPluginTest
    {
        [TestMethod]
        public void RandomNameIsReturned()
        {
            var sut = new GermanStreetNames();
            var value = sut.GetValue();

            Assert.IsFalse(string.IsNullOrEmpty(value));
        }
    }
}
