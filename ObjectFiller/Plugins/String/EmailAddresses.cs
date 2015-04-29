
namespace Tynamix.ObjectFiller
{
    public class EmailAddresses : IRandomizerPlugin<string>
    {
        private readonly IRandomizerPlugin<string> domainNameSource;

        private readonly IRandomizerPlugin<string> localPartSource;

        private string domainRoot;

        public EmailAddresses()
            : this(new MnemonicString(1), new MnemonicString(1), ".com")
        {
        }

        public EmailAddresses(IRandomizerPlugin<string> localPartSource)
            : this(localPartSource, new MnemonicString(1), ".com")
        {
        }

        public EmailAddresses(
            IRandomizerPlugin<string> localPartSource, 
            IRandomizerPlugin<string> domainSource, 
            string domainRoot)
        {
            this.domainRoot = domainRoot;
            this.localPartSource = localPartSource;
            this.domainNameSource = domainSource;
        }

        public EmailAddresses(string domainRoot)
            : this(new MnemonicString(1), new MnemonicString(1), domainRoot)
        {
        }

        public EmailAddresses(IRandomizerPlugin<string> localPartSource, IRandomizerPlugin<string> domainSource) : this(localPartSource, domainSource, ".com")
        {
        }

        public string GetValue()
        {
            var localPart = this.GetLocalPart();
            var domain = this.GetDomainName();

            return string.Format("{0}@{1}", localPart, domain).ToLower();
        }

        private string GetDomainName()
        {
            var domainName = this.domainNameSource.GetValue();

            if (domainName.Contains("."))
            {
                return domainName;
            }

            return string.Format("{0}{1}", domainName, this.domainRoot);
        }

        private string GetLocalPart()
        {
            var originalSample = this.localPartSource.GetValue();
            return originalSample.Replace(" ", ".");
        }
    }
}