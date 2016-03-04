// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StreetName.cs" company="Tynamix">
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


    /// <summary>
    /// The city of which the street names  shall come from
    /// </summary>
    public enum City
    {
        /// <summary>
        /// Dresden is a city in germany
        /// </summary>
        Dresden,

        /// <summary>
        /// New York is a city in the USA
        /// </summary>
        NewYork,

        /// <summary>
        /// London is a city in Great Britain
        /// </summary>
        London,

        /// <summary>
        /// Paris is a city in France
        /// </summary>
        Paris,

        /// <summary>
        /// Tokyo is a city in Japan
        /// </summary>
        Tokyo,

        /// <summary>
        /// Moscow is a city in russia
        /// </summary>
        Moscow
    }

    /// <summary>
    /// Generate street names for type <see cref="string"/>
    /// </summary>
    public class StreetName : IRandomizerPlugin<string>
    {
        /// <summary>
        /// The names.
        /// </summary>
        protected readonly List<string> AllStreetNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreetName"/> class.
        /// It will use streets of London by default
        /// </summary>
        public StreetName()
            : this(City.London)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreetName"/> class.
        /// </summary>
        /// <param name="targetCity">
        /// The city for which the street names will be get from
        /// </param>
        public StreetName(City targetCity)
        {
            switch (targetCity)
            {
                case City.Dresden:
                    this.AllStreetNames = Resources.GermanStreetNames.Split(';').ToList();
                    break;
                case City.Moscow:
                    this.AllStreetNames = Resources.MoscowStreetNames.Split(';').ToList();
                    break;
                case City.NewYork:
                    this.AllStreetNames = Resources.NewYorkStreetNames.Split(';').ToList();
                    break;
                case City.Tokyo:
                    this.AllStreetNames = Resources.TokyoStreetNames.Split(';').ToList();
                    break;
                case City.Paris:
                    this.AllStreetNames = Resources.ParisStreetNames.Split(';').ToList();
                    break;
                case City.London:
                    this.AllStreetNames = Resources.LondonStreetNames.Split(';').ToList();
                    break;
            }
        }

        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        public string GetValue()
        {
            var index = Random.Next(this.AllStreetNames.Count - 1);
            return this.AllStreetNames[index];
        }
    }
}