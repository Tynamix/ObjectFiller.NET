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

        private static int RandomSeed;
        /// <summary>
        /// Initializes static members of the <see cref="Random"/> class.
        /// A instance of <see cref="Random"/>
        /// </summary>
        static Random()
        {
            RandomSeed = Environment.TickCount;
        }

#if NET3X
        /// <summary>
        /// A instance of <see cref="Random"/>
        /// </summary>
        [ThreadStatic]
        private static System.Random RndStorage;

        private static System.Random Rnd
        {
            get
            {
                if (RndStorage == null)
                {
                    RndStorage = new System.Random(Interlocked.Increment(ref RandomSeed));
                }
                return RndStorage;
            }
        }
#else
        private static readonly ThreadLocal<System.Random> RndStorage = new ThreadLocal<System.Random>(() =>
            new System.Random(Interlocked.Increment(ref RandomSeed)));

        /// <summary>
        /// A instance of <see cref="Random"/>
        /// </summary>
        private static System.Random Rnd
        {
            get { return RndStorage.Value; }
        }
#endif

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