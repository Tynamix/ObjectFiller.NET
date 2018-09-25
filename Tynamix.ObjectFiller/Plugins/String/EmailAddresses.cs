// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailAddresses.cs" company="Tynamix">
//   © 2015 by Hendrik Lösch
// </copyright>
// <summary>
//   Generates e-mail adresses
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// Generates e-mail address
    /// </summary>
    public class EmailAddresses : IRandomizerPlugin<string>
    {
        /// <summary>
        /// The domain name source.
        /// </summary>
        private readonly IRandomizerPlugin<string> domainNameSource;

        /// <summary>
        /// The local part source.
        /// </summary>
        private readonly IRandomizerPlugin<string> localPartSource;

        /// <summary>
        /// The domain root.
        /// </summary>
        private readonly string topLevelDomain;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddresses"/> class.
        /// </summary>
        public EmailAddresses()
            : this(new MnemonicString(1), new MnemonicString(1), ".com")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddresses"/> class.
        /// </summary>
        /// <param name="localPartSource">
        /// Randomizer for the local part
        /// </param>
        public EmailAddresses(IRandomizerPlugin<string> localPartSource)
            : this(localPartSource, new MnemonicString(1), ".com")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddresses"/> class.
        /// </summary>
        /// <param name="localPartSource">
        /// Randomizer for the local part
        /// </param>
        /// <param name="domainNameSource">
        /// Randomizer for the domain part
        /// </param>
        /// <param name="topLevelDomain">
        /// The top level domain
        /// </param>
        public EmailAddresses(
            IRandomizerPlugin<string> localPartSource, 
            IRandomizerPlugin<string> domainNameSource, 
            string topLevelDomain)
        {
            this.topLevelDomain = topLevelDomain;
            this.localPartSource = localPartSource;
            this.domainNameSource = domainNameSource;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddresses"/> class.
        /// </summary>
        /// <param name="topLevelDomain">
        /// The top level domain
        /// </param>
        public EmailAddresses(string topLevelDomain)
            : this(new MnemonicString(1), new MnemonicString(1), topLevelDomain)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddresses"/> class.
        /// </summary>
        /// <param name="localPartSource">
        /// The local part source.
        /// </param>
        /// <param name="domainNameSource">
        /// The domain name source.
        /// </param>
        public EmailAddresses(IRandomizerPlugin<string> localPartSource, IRandomizerPlugin<string> domainNameSource) 
            : this(localPartSource, domainNameSource, ".com")
        {
        }

        /// <summary>
        /// Gets random data for type <see cref="string"/>
        /// </summary>
        /// <returns>Random data for type <see cref="string"/></returns>
        public string GetValue()
        {
            var localPart = this.GetLocalPart();
            var domain = this.GetDomainName();

            return string.Format("{0}@{1}", localPart, domain).ToLower();
        }

        /// <summary>
        /// Gets 
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetDomainName()
        {
            var domainName = this.domainNameSource.GetValue();

            if (domainName.Contains("."))
            {
                return domainName;
            }

            return string.Format("{0}{1}", domainName, this.topLevelDomain);
        }

        private string GetLocalPart()
        {
            var originalSample = this.localPartSource.GetValue();
            return originalSample.Replace(" ", ".");
        }
    }
}