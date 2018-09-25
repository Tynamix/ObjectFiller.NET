using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tynamix.ObjectFiller.Test
{
    [TestClass]
    public class UrlTest
    {
        [TestMethod]
        public void RandomUrlIsReturned()
        {
            var sut = new RandomUrl();
            var value = sut.GetValue();
            Assert.IsNotNull(value);

            var uri = new Uri(value);

            Assert.IsFalse(string.IsNullOrEmpty(uri.Host));
            Assert.IsFalse(string.IsNullOrEmpty(uri.Scheme));
            Assert.IsFalse(string.IsNullOrEmpty(uri.PathAndQuery));
        }

        [TestMethod]
        public void Returns_Http_When_Set()
        {
            var sut = new RandomUrl(RandomUri.SchemeType.Http);
            var value = sut.GetValue();
            Assert.IsNotNull(value);

            var uri = new Uri(value);
            Assert.AreEqual("http", uri.Scheme);
        }

        [TestMethod]
        public void Returns_Https_When_Set()
        {
            var sut = new RandomUrl(RandomUri.SchemeType.Https);
            var value = sut.GetValue();
            Assert.IsNotNull(value);

            var uri = new Uri(value);
            Assert.AreEqual("https", uri.Scheme);
        }

        [TestMethod]
        public void Returns_Configured_Domain_When_Set()
        {
            var sut = new RandomUrl(RandomUri.SchemeType.Https, "net");
            var value = sut.GetValue();
            Assert.IsNotNull(value);

            var uri = new Uri(value);
            Assert.AreEqual("https", uri.Scheme);
            Assert.IsTrue(uri.Host.EndsWith(".net"));
        }

        internal class UriTestClass
        {
            public string RemoteUrl { get; set; }
        }

        [TestMethod]
        public void Set_Value_With_Filler_With_Setup()
        {
            Filler<UriTestClass> filler = new Filler<UriTestClass>();

            filler.Setup()
                .OnProperty(x => x.RemoteUrl).Use<RandomUrl>();

            var result = filler.Create();

            var url = result.RemoteUrl;

            Assert.IsFalse(string.IsNullOrEmpty(url));
        }
    }
}
