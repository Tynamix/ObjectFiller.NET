// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumeratorPlugin.cs" company="Tynamix">
//   © by Roman Köhler
// </copyright>
// <summary>
//   Enumerator plugin is used to always select the next value of an IEnumerable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Enumerator plugin is used to always select the next value of an IEnumerable.
    /// </summary>
    /// <typeparam name="T">Type for which the randomizer will generate data</typeparam>
    internal class EnumeratorPlugin<T> : IRandomizerPlugin<T>
    {
        /// <summary>
        /// The enumerable.
        /// </summary>
        private readonly IEnumerable<T> enumerable;

        /// <summary>
        /// The enumerator to move thru the the <see cref="enumerable"/>
        /// </summary>
        private IEnumerator<T> enumerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumeratorPlugin{T}"/> class.
        /// </summary>
        /// <param name="enumerable">
        /// The enumerable to select one value after another.
        /// </param>
        public EnumeratorPlugin(IEnumerable<T> enumerable)
        {
            this.enumerable = enumerable;
        }

        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        public T GetValue()
        {
            // First time?
            if (this.enumerator == null)
            {
                this.enumerator = this.enumerable.GetEnumerator();
            }

            // Advance, try to recover if we hit end-of-enumeration
            var hasNext = this.enumerator.MoveNext();
            if (!hasNext)
            {
                this.enumerator = this.enumerable.GetEnumerator();
                hasNext = this.enumerator.MoveNext();

                if (!hasNext)
                {
                    string message = string.Format("Unable to get next value from enumeration {0}", this.enumerable);
                    throw new Exception(message);
                }
            }

            return this.enumerator.Current;
        }
    }
}
