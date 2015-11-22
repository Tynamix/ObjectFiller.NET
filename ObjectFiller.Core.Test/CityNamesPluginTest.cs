namespace ObjectFiller.Test
{
    using Xunit;

    using Tynamix.ObjectFiller;


    public class CityNamesPluginTest
    {
        [Fact]
        public void RandomNameIsReturned()
        {
            var sut = new CityName();
            var value = sut.GetValue();

            Assert.False(string.IsNullOrEmpty(value));
        }
    }
}