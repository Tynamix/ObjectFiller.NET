using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tynamix.ObjectFiller.Test
{
    [TestClass]
    public class EmailAddressesPluginTests
    {
        public string StandardAssertMessage = "Given value does not match e-mail address standard.";

        /// <summary>
        /// Regex for EMail addresses based on RFC 5322. Unfortunately, it does not find whitespace and yes I am to dumb to fix this issue...
        /// </summary>
        /// <seealso cref="http://www.regular-expressions.info/email.html"/>
        private static Regex RFC5322RegEx =
          new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", RegexOptions.IgnoreCase);

        [TestMethod]
        public void DefaultModeShouldReturnValidEmailAdress()
        {
            var value = new EmailAddresses().GetValue();
            Assert.IsTrue(RFC5322RegEx.IsMatch(value));
        }

        [TestMethod]
        public void TwoCallsCreateTwoDifferentEMailAddresses()
        {
            var sut = new EmailAddresses();
            var firstValue = sut.GetValue();
            var secondValue = sut.GetValue();

            Assert.AreNotEqual(firstValue, secondValue);
        }

        [TestMethod]
        public void EMailAddressMustBeValidWithRealNames()
        {
            var sut = new EmailAddresses();

            var value = sut.GetValue();

            Assert.IsTrue(RFC5322RegEx.IsMatch(value));
        }

        [TestMethod]
        public void DomainNamesAreUsedFromRandomData()
        {
            var referenceValue = "google.com";
            var fake = new FakeRandomizerPlugin<string>(referenceValue);

            var sut = new EmailAddresses(fake, fake);

            var result = sut.GetValue();

            Assert.IsTrue(result.EndsWith(referenceValue));
            Assert.IsTrue(RFC5322RegEx.IsMatch(result));
        }

        [TestMethod]
        public void PluginMustEnsureValidAddressesEvenAnInvalidDomainNameIsProvided()
        {
            var referenceValue = "googlecom";
            var fake = new FakeRandomizerPlugin<string>(referenceValue);

            var sut = new EmailAddresses(fake, fake);

            var result = sut.GetValue();
            Assert.IsTrue(RFC5322RegEx.IsMatch(result));
        }

        [TestMethod]
        public void LocalPathMustBeUsedFromRandomData()
        {
            var referenceValue = "karl";
            var fake = new FakeRandomizerPlugin<string>(referenceValue);

            var sut = new EmailAddresses(fake);

            var result = sut.GetValue();

            Assert.IsTrue(result.StartsWith(referenceValue));
            Assert.IsTrue(RFC5322RegEx.IsMatch(result));
        }

        [TestMethod]
        public void PluginMustEnsureValidAddressesEvenAnInvalidLocalPartIsProvided()
        {
            var referenceValue = "ka rl";
            var fake = new FakeRandomizerPlugin<string>(referenceValue);

            var sut = new EmailAddresses(fake);

            var result = sut.GetValue();

            Assert.IsTrue(RFC5322RegEx.IsMatch(result));
        }

        [TestMethod]
        public void GivenDomainRootIsAttachedToGeneratedEmailAddress()
        {
            var domainRoot = ".de";
            var sut = new EmailAddresses(domainRoot);

            var result = sut.GetValue();

            Assert.IsTrue(result.EndsWith(domainRoot));
            Assert.IsTrue(RFC5322RegEx.IsMatch(result));
        }

        [TestMethod]
        public void EmailAddressesWorksInCombinationWithRealNamesPlugin()
        {
            var realNames = new RealNames(NameStyle.FirstNameLastName);

            var sut = new EmailAddresses(realNames);
            var result = sut.GetValue();

            Assert.IsTrue(RFC5322RegEx.IsMatch(result));
        }
    }
}
