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
    using System;
    using System.Threading;

    /// <summary>
    /// Class wraps the <see cref="System.Random"/> class. 
    /// This is to have a static instance of the random class
    /// </summary>
    internal static class Random
    {
        /// <summary>
        /// A instance of <see cref="Random"/>
        /// </summary>
        [ThreadStatic]
        private static readonly System.Random Rnd;

        /// <summary>
        /// Initializes static members of the <see cref="Random"/> class.
        /// </summary>
        static Random()
        {
#if NETSTD
            Rnd = new System.Random();
#else
            int seed = Environment.TickCount;
            Rnd = new System.Random(Interlocked.Increment(ref seed));
#endif
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

        /// <summary>
        /// Gets a random value between to source long values
        /// </summary>
        /// <param name="min">Min long value</param>
        /// <param name="max">Max long value</param>
        /// <returns>A Long between <see cref="min"/> and <see cref="max"/></returns>
        internal static long NextLong(long min, long max)
        {
            long longRand = NextLong();
            return (Math.Abs(longRand % (max - min)) + min);
        }

        /// <summary>
        /// Gets a random value between to source long values
        /// </summary>
        /// <param name="min">Min long value</param>
        /// <param name="max">Max long value</param>
        /// <returns>A Long between <see cref="min"/> and <see cref="max"/></returns>
        internal static long NextLong()
        {
            byte[] buf = new byte[8];
            Rnd.NextBytes(buf);
            return BitConverter.ToInt64(buf, 0);
        }
    }
}