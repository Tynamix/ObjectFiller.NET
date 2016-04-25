namespace ObjectFiller.Test
{
    using System.Security.Policy;
    using Tynamix.ObjectFiller;
    using Xunit;

    public class IpAddressPluginTest
    {

        internal class IpAddressClass
        {
            public string IpAddress { get; set; }
        }

        [Fact]
        public void TestIpAddressGenerator()
        {
            Filler<IpAddressClass> filler = new Filler<IpAddressClass>();

            filler.Setup()
                .OnProperty(x => x.IpAddress).Use<IpAddress>();

            var result = filler.Create();

            var ipAddresses = result.IpAddress.Split('.');

            Assert.Collection(ipAddresses,
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 255),
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 255),
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 255),
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 255));
        }

        [Fact]
        public void TestIpAddressGeneratorWithWrongNumbersAndFirstSegmentMaximumOf10()
        {
            Filler<IpAddressClass> filler = new Filler<IpAddressClass>();

            filler.Setup().OnProperty(x => x.IpAddress).Use(new IpAddress(10, 300, 300, 300));

            var result = filler.Create();

            var ipAddresses = result.IpAddress.Split('.');

            Assert.Collection(ipAddresses,
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 10),
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 255),
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 255),
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 255));
        }

        [Fact]
        public void TestIpAddressGeneratorWithSegmentSetup()
        {
            Filler<IpAddressClass> filler = new Filler<IpAddressClass>();

            filler.Setup().OnProperty(x => x.IpAddress).Use(new IpAddress(10, 10, 10, 10));

            var result = filler.Create();

            var ipAddresses = result.IpAddress.Split('.');

            Assert.Collection(ipAddresses,
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 10),
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 10),
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 10),
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 10));
        }

        [Fact]
        public void TestIpAddressGeneratorWithFirstTwoSegmentSetup()
        {
            Filler<IpAddressClass> filler = new Filler<IpAddressClass>();

            filler.Setup().OnProperty(x => x.IpAddress).Use(new IpAddress(10, 10));

            var result = filler.Create();

            var ipAddresses = result.IpAddress.Split('.');

            Assert.Collection(ipAddresses,
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 10),
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 10),
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 255),
                              (s) => Assert.True(int.Parse(s) > 0 && int.Parse(s) <= 255));
        }
    }
}