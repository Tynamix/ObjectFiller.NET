using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// Creates a random Uri
    /// </summary>
    public class RandomUri : IRandomizerPlugin<Uri>
    {
        /// <summary>
        /// Type which uri scheme should be used
        /// </summary>
        public enum SchemeType
        {
            /// <summary>
            /// The HTTP scheme
            /// </summary>
            Http,
            /// <summary>
            /// The HTTPS scheme
            /// </summary>
            Https,
            /// <summary>
            /// The HTTP and HTTPS schemes (randomized)
            /// </summary>
            HttpAndHttps
        }

        private readonly SchemeType _schemeType;
        private readonly string[] _domainNames = { "de", "com", "org", "uk", "gov", "fr", "ru" };

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomUri"/> class.
        /// </summary>
        public RandomUri()
            : this(SchemeType.HttpAndHttps)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomUri"/> class.
        /// </summary>
        /// <param name="schemeType">Type of the scheme.</param>
        public RandomUri(SchemeType schemeType)
        {
            _schemeType = schemeType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomUri" /> class.
        /// </summary>
        /// <param name="schemeType">Type of the scheme.</param>
        /// <param name="domainNames">The possible domain names.</param>
        public RandomUri(SchemeType schemeType, params string[] domainNames)
            :this(schemeType)
        {
            _domainNames = domainNames;
        }

        /// <summary>
        /// Gets random data for type <see cref="Uri" />
        /// </summary>
        /// <returns>Random data for type <see cref="Uri" /></returns>
        public Uri GetValue()
        {
            var scheme = CreateScheme();
            var www = Randomizer<bool>.Create() ? "www." : string.Empty;
            var host = Randomizer<string>.Create(new MnemonicString(1)).ToLower();
            var domain = Randomizer<string>.Create(new RandomListItem<string>(_domainNames));
            StringBuilder relativePathBuilder = new StringBuilder();
            if (Randomizer<bool>.Create())
            {
                var relativeElements = Randomizer<int>.Create(new IntRange(1, 4));
                for (int i = 0; i < relativeElements; i++)
                {
                    relativePathBuilder.Append("/");
                    relativePathBuilder.Append(Randomizer<string>.Create(new MnemonicString(1)).ToLower());
                }
            }

            var uriBuilder = new UriBuilder(scheme, $"{www}{host}.{domain}{relativePathBuilder}");
            return uriBuilder.Uri;
        }

        private string CreateScheme()
        {
            switch (_schemeType)
            {
                case SchemeType.Http:
                    return "http";
                case SchemeType.Https:
                    return "https";
                default:
                    return Randomizer<bool>.Create() ? "http" : "https";
            }
        }
    }
}
