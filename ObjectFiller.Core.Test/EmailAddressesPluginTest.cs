using System.Text.RegularExpressions;
using Xunit;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{

    public class EmailAddressesPluginTests
    {
        public string StandardAssertMessage = "Given value does not match e-mail address standard.";

        /// <summary>
        /// Regex for EMail addresses based on RFC 5322. Unfortunately, it does not find whitespace and yes I am to dumb to fix this issue...
        /// </summary>
        /// <seealso cref="http://www.regular-expressions.info/email.html"/>
        private static Regex RFC5322RegEx =
          new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", RegexOptions.IgnoreCase);

        [Fact]
        public void DefaultModeShouldReturnValidEmailAdress()
        {
            var value = new EmailAddresses().GetValue();
            Assert.True(RFC5322RegEx.IsMatch(value));
        }

        [Fact]
        public void TwoCallsCreateTwoDifferentEMailAddresses()
        {
            var sut = new EmailAddresses();
            var firstValue = sut.GetValue();
            var secondValue = sut.GetValue();

            Assert.NotEqual(firstValue, secondValue);
        }

        [Fact]
        public void EMailAddressMustBeValidWithRealNames()
        {
            var sut = new EmailAddresses();

            var value = sut.GetValue();

            Assert.True(RFC5322RegEx.IsMatch(value));
        }

        [Fact]
        public void DomainNamesAreUsedFromRandomData()
        {
            var referenceValue = "google.com";
            var fake = new FakeRandomizerPlugin<string>(referenceValue);

            var sut = new EmailAddresses(fake, fake);

            var result = sut.GetValue();

            Assert.EndsWith(referenceValue, result);
            Assert.True(RFC5322RegEx.IsMatch(result));
        }

        [Fact]
        public void PluginMustEnsureValidAddressesEvenAnInvalidDomainNameIsProvided()
        {
            var referenceValue = "googlecom";
            var fake = new FakeRandomizerPlugin<string>(referenceValue);

            var sut = new EmailAddresses(fake, fake);

            var result = sut.GetValue();
            Assert.True(RFC5322RegEx.IsMatch(result));
        }

        [Fact]
        public void LocalPathMustBeUsedFromRandomData()
        {
            var referenceValue = "karl";
            var fake = new FakeRandomizerPlugin<string>(referenceValue);

            var sut = new EmailAddresses(fake);

            var result = sut.GetValue();

            Assert.StartsWith(referenceValue, result);
            Assert.True(RFC5322RegEx.IsMatch(result));
        }

        [Fact]
        public void PluginMustEnsureValidAddressesEvenAnInvalidLocalPartIsProvided()
        {
            var referenceValue = "ka rl";
            var fake = new FakeRandomizerPlugin<string>(referenceValue);

            var sut = new EmailAddresses(fake);

            var result = sut.GetValue();

            Assert.True(RFC5322RegEx.IsMatch(result));
        }

        [Fact]
        public void GivenDomainRootIsAttachedToGeneratedEmailAddress()
        {
            var domainRoot = ".de";
            var sut = new EmailAddresses(domainRoot);

            var result = sut.GetValue();

            Assert.EndsWith(domainRoot, result);
            Assert.True(RFC5322RegEx.IsMatch(result));
        }

        [Fact]
        public void EmailAddressesWorksInCombinationWithRealNamesPlugin()
        {
            var realNames = new RealNames(NameStyle.FirstNameLastName);

            var sut = new EmailAddresses(realNames);
            var result = sut.GetValue();

            Assert.True(RFC5322RegEx.IsMatch(result));
        }
    }
}
