// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CityName.cs" company="Tynamix">
//   © 2015 by Hendrik Lösch and Roman Köhler
// </copyright>
// <summary>
//   Generate city name for type <see cref="string" />
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    using System.Collections.Generic;
    using System.Linq;

    using Tynamix.ObjectFiller.Properties;

    /// <summary>
    /// Generate city names for type <see cref="string"/>. The Top 1000 cities with the most population will be used
    /// </summary>
    public class CityName : IRandomizerPlugin<string>
    {
        /// <summary>
        /// The names.
        /// </summary>
        protected static readonly List<string> AllCityNames;

        /// <summary>
        /// Initializes static members of the <see cref="CityName"/> class.
        /// </summary>
        static CityName()
        {
            AllCityNames = Resources.cityNames.Split(';').ToList();
        }

        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        public string GetValue()
        {
            var index = Random.Next(AllCityNames.Count - 1);
            return AllCityNames[index];
        }
    }
}