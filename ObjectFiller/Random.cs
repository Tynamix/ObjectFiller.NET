// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Random.cs" company="Tynamix">
//   © 2015 by Roman Köhler
// </copyright>
// <summary>
//   Class wraps the <see cref="System.Random" /> class.
//   This is to have a static instance of the random class
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// Class wraps the <see cref="System.Random"/> class. 
    /// This is to have a static instance of the random class
    /// </summary>
    internal static class Random
    {
        /// <summary>
        /// A instance of <see cref="Random"/>
        /// </summary>
        private static readonly System.Random Rnd;

        /// <summary>
        /// Initializes static members of the <see cref="Random"/> class.
        /// </summary>
        static Random()
        {
            Rnd = new System.Random();
        }

        /// <summary>
        /// Returns a nonnegative number
        /// </summary>
        /// <returns>
        /// A nonnegative number
        /// </returns>
        internal static int Next()
        {
            return Rnd.Next();
        }

        /// <summary>
        /// Returns a nonnegative number less than specified <see cref="maxValue"/>
        /// </summary>
        /// <param name="maxValue">
        /// The maximum value.
        /// </param>
        /// <returns>
        /// A nonnegative number less than specified <see cref="maxValue"/>
        /// </returns>
        internal static int Next(int maxValue)
        {
            return Rnd.Next(maxValue);
        }

        /// <summary>
        /// Returns a random number within the given range
        /// </summary>
        /// <param name="minValue">
        /// The min value.
        /// </param>
        /// <param name="maxValue">
        /// The max value.
        /// </param>
        /// <returns>
        /// A random number within the given range
        /// </returns>
        internal static int Next(int minValue, int maxValue)
        {
            return Rnd.Next(minValue, maxValue);
        }

        /// <summary>
        /// Returns a random number between 0.0 and 1.0
        /// </summary>
        /// <returns>
        /// A random number between 0.0 and 1.0
        /// </returns>
        internal static double NextDouble()
        {
            return Rnd.NextDouble();
        }

        /// <summary>
        /// Fills the elements of a specified array of bytes  with random numbers
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        internal static void NextByte(byte[] buffer)
        {
            Rnd.NextBytes(buffer);
        }
    }
}