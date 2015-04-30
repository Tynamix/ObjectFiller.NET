// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GermanStreetNames.cs" company="Tynamix">
//   © 2015 by Hendrik Lösch and Roman Köhler
// </copyright>
// <summary>
//   Generate streetnames for type <see cref="string" />
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    using System.Collections.Generic;
    using System.Linq;

    using Tynamix.ObjectFiller.Properties;

    /// <summary>
    /// Generate street names for type <see cref="string"/>
    /// </summary>
    public class GermanStreetNames : IRandomizerPlugin<string>
    {
        /// <summary>
        /// The names.
        /// </summary>
        protected readonly List<string> StreetNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="GermanStreetNames"/> class.
        /// </summary>
        public GermanStreetNames()
        {
            this.StreetNames = Resources.germanStreetNames.Split(';').ToList();
        }

        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        public string GetValue()
        {
            var index = Random.Next(this.StreetNames.Count - 1);
            return this.StreetNames[index];
        }
    }
}