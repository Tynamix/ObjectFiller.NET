namespace ObjectFiller.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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