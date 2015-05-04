// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CountryName.cs" company="Tynamix">
//   © 2015 by Hendrik Lösch and Roman Köhler
// </copyright>
// <summary>
//   Generate country names for type <see cref="string" />
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    using System.Collections.Generic;
    using System.Linq;

    using Tynamix.ObjectFiller.Properties;

    /// <summary>
    /// Generate country names for type <see cref="string"/>
    /// </summary>
    public class CountryName : IRandomizerPlugin<string>
    {
        /// <summary>
        /// The names.
        /// </summary>
        protected static readonly List<string> AllCountryNames;

        /// <summary>
        /// Initializes static members of the <see cref="CountryName"/> class.
        /// </summary>
        static CountryName()
        {
            AllCountryNames = Resources.countryNames.Split(';').ToList();
        }

        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        public string GetValue()
        {
            var index = Random.Next(AllCountryNames.Count - 1);
            return AllCountryNames[index];
        }
    }
}