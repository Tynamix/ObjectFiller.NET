    using Xunit;

namespace ObjectFiller.Test
{
    using Tynamix.ObjectFiller;


    public class StreetNamesPluginTest
    {
        [Fact]
        public void RandomNameIsReturned()
        {
            var sut = new StreetName(City.Dresden);
            var value = sut.GetValue();
            Assert.False(string.IsNullOrEmpty(value));


            sut = new StreetName(City.NewYork);
            value = sut.GetValue();
            Assert.False(string.IsNullOrEmpty(value));

            sut = new StreetName(City.London);
            value = sut.GetValue();
            Assert.False(string.IsNullOrEmpty(value));

            sut = new StreetName(City.Moscow);
            value = sut.GetValue();
            Assert.False(string.IsNullOrEmpty(value));

            sut = new StreetName(City.Paris);
            value = sut.GetValue();
            Assert.False(string.IsNullOrEmpty(value));

            sut = new StreetName(City.Tokyo);
            value = sut.GetValue();
            Assert.False(string.IsNullOrEmpty(value));
        }
    }
}
