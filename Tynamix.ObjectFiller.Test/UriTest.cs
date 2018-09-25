using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tynamix.ObjectFiller.Test
{
    [TestClass]
    public class UriTest
    {
        [TestMethod]
        public void RandomUriIsReturned()
        {
            var sut = new RandomUri();
            var value = sut.GetValue();
            Assert.IsNotNull(value);

            Assert.IsFalse(string.IsNullOrEmpty(value.Host));
            Assert.IsFalse(string.IsNullOrEmpty(value.Scheme));
            Assert.IsFalse(string.IsNullOrEmpty(value.PathAndQuery));
        }

        [TestMethod]
        public void Returns_Http_When_Set()
        {
            var sut = new RandomUri(RandomUri.SchemeType.Http);
            var value = sut.GetValue();
            Assert.IsNotNull(value);

            Assert.AreEqual("http", value.Scheme);
        }

        [TestMethod]
        public void Returns_Https_When_Set()
        {
            var sut = new RandomUri(RandomUri.SchemeType.Https);
            var value = sut.GetValue();
            Assert.IsNotNull(value);

            Assert.AreEqual("https", value.Scheme);
        }

        [TestMethod]
        public void Returns_Configured_Domain_When_Set()
        {
            var sut = new RandomUri(RandomUri.SchemeType.Https, "net");
            var value = sut.GetValue();
            Assert.IsNotNull(value);

            Assert.AreEqual("https", value.Scheme);
            Assert.IsTrue(value.Host.EndsWith(".net"));
        }

        internal class UriTestClass
        {
            public Uri RemoteUri { get; set; }
        }

        [TestMethod]
        public void Set_Value_With_Filler_With_Setup()
        {
            Filler<UriTestClass> filler = new Filler<UriTestClass>();

            filler.Setup()
                .OnProperty(x => x.RemoteUri).Use<RandomUri>();

            var result = filler.Create();

            var url = result.RemoteUri.ToString();

            Assert.IsFalse(string.IsNullOrEmpty(url));           
        }


        [TestMethod]
        public void Set_Value_With_Filler_Without_Setup()
        {
            Filler<UriTestClass> filler = new Filler<UriTestClass>();
           
            var result = filler.Create();

            var url = result.RemoteUri.ToString();

            Assert.IsFalse(string.IsNullOrEmpty(url));
        }
    }
}
