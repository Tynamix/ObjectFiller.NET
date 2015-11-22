namespace ObjectFiller.Test
{
        using Xunit;

    using Tynamix.ObjectFiller;


    public class CountryNamesPlugin
    {
        [Fact]
        public void RandomNameIsReturned()
        {
            var sut = new CountryName();
            var value = sut.GetValue();

            Assert.False(string.IsNullOrEmpty(value));
        }
    }
}