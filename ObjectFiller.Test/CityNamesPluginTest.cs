namespace ObjectFiller.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Tynamix.ObjectFiller.Plugins.String;

    [TestClass]
    public class CityNamesPluginTest
    {
        [TestMethod]
        public void RandomNameIsReturned()
        {
            var sut = new CityNames();
            var value = sut.GetValue();

            Assert.IsFalse(string.IsNullOrEmpty(value));
        }
    }
}