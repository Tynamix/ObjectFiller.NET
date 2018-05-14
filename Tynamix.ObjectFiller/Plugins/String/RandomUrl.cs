using System;
using System.Collections.Generic;
using System.Text;
using static Tynamix.ObjectFiller.RandomUri;

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// Creates a random Url
    /// </summary>
    public class RandomUrl : IRandomizerPlugin<string>
    {
        private readonly RandomUri _uri;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomUrl"/> class.
        /// </summary>
        public RandomUrl()
        {
            _uri = new RandomUri();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomUrl"/> class.
        /// </summary>
        /// <param name="schemeType">Type of the scheme.</param>
        public RandomUrl(SchemeType schemeType)
        {
            _uri = new RandomUri(schemeType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomUrl" /> class.
        /// </summary>
        /// <param name="schemeType">Type of the scheme.</param>
        /// <param name="domainNames">The possible domain names.</param>
        public RandomUrl(SchemeType schemeType, params string[] domainNames)
        {
            _uri = new RandomUri(schemeType, domainNames);
        }

        /// <summary>
        /// Gets random data for type <see cref="string" />
        /// </summary>
        /// <returns>Random data for type <see cref="string" /></returns>
        public string GetValue()
        {
            return _uri.GetValue().ToString();
        }
    }
}
