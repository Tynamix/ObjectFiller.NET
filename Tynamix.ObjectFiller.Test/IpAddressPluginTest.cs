using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectFiller.Test
{
    using Tynamix.ObjectFiller;

    [TestClass]
    public class IpAddressPluginTest
    {

        internal class IpAddressClass
        {
            public string IpAddress { get; set; }
        }

        [TestMethod]
        public void TestIpAddressGenerator()
        {
            Filler<IpAddressClass> filler = new Filler<IpAddressClass>();

            filler.Setup()
                .OnProperty(x => x.IpAddress).Use<IpAddress>();

            var result = filler.Create();

            var ipAddresses = result.IpAddress.Split('.');


            Assert.IsTrue(int.Parse(ipAddresses[0]) > 0 && int.Parse(ipAddresses[0]) <= 255);
            Assert.IsTrue(int.Parse(ipAddresses[1]) > 0 && int.Parse(ipAddresses[1]) <= 255);
            Assert.IsTrue(int.Parse(ipAddresses[2]) > 0 && int.Parse(ipAddresses[2]) <= 255);
            Assert.IsTrue(int.Parse(ipAddresses[3]) > 0 && int.Parse(ipAddresses[3]) <= 255);
        }

        [TestMethod]
        public void TestIpAddressGeneratorWithWrongNumbersAndFirstSegmentMaximumOf10()
        {
            Filler<IpAddressClass> filler = new Filler<IpAddressClass>();

            filler.Setup().OnProperty(x => x.IpAddress).Use(new IpAddress(10, 300, 300, 300));

            var result = filler.Create();

            var ipAddresses = result.IpAddress.Split('.');

            Assert.IsTrue(int.Parse(ipAddresses[0]) > 0 && int.Parse(ipAddresses[0]) <= 10);
            Assert.IsTrue(int.Parse(ipAddresses[1]) > 0 && int.Parse(ipAddresses[1]) <= 255);
            Assert.IsTrue(int.Parse(ipAddresses[2]) > 0 && int.Parse(ipAddresses[2]) <= 255);
            Assert.IsTrue(int.Parse(ipAddresses[3]) > 0 && int.Parse(ipAddresses[3]) <= 255);
        }

        [TestMethod]
        public void TestIpAddressGeneratorWithSegmentSetup()
        {
            Filler<IpAddressClass> filler = new Filler<IpAddressClass>();

            filler.Setup().OnProperty(x => x.IpAddress).Use(new IpAddress(10, 10, 10, 10));

            var result = filler.Create();

            var ipAddresses = result.IpAddress.Split('.');

            Assert.IsTrue(int.Parse(ipAddresses[0]) > 0 && int.Parse(ipAddresses[0]) <= 10);
            Assert.IsTrue(int.Parse(ipAddresses[1]) > 0 && int.Parse(ipAddresses[1]) <= 10);
            Assert.IsTrue(int.Parse(ipAddresses[2]) > 0 && int.Parse(ipAddresses[2]) <= 10);
            Assert.IsTrue(int.Parse(ipAddresses[3]) > 0 && int.Parse(ipAddresses[3]) <= 10);
        }

        [TestMethod]
        public void TestIpAddressGeneratorWithFirstTwoSegmentSetup()
        {
            Filler<IpAddressClass> filler = new Filler<IpAddressClass>();

            filler.Setup().OnProperty(x => x.IpAddress).Use(new IpAddress(10, 10));

            var result = filler.Create();

            var ipAddresses = result.IpAddress.Split('.');

            Assert.IsTrue(int.Parse(ipAddresses[0]) > 0 && int.Parse(ipAddresses[0]) <= 10);
            Assert.IsTrue(int.Parse(ipAddresses[1]) > 0 && int.Parse(ipAddresses[1]) <= 10);
            Assert.IsTrue(int.Parse(ipAddresses[2]) > 0 && int.Parse(ipAddresses[2]) <= 255);
            Assert.IsTrue(int.Parse(ipAddresses[3]) > 0 && int.Parse(ipAddresses[3]) <= 255);
        }
    }
}