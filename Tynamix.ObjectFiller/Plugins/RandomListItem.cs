// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomListItem.cs" company="Tynamix">
//   © by Roman Köhler
// </copyright>
// <summary>
//   Plugin to use a random value from a given list
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Plugin to use a random value from a given list
    /// </summary>
    /// <typeparam name="T">Type of the items in the list</typeparam>
    public class RandomListItem<T> : IRandomizerPlugin<T>
    {
        /// <summary>
        /// All available values which will be used to select a random value
        /// </summary>
        private readonly T[] allAvailableValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomListItem{T}"/> class.
        /// </summary>
        /// <param name="allAvailableValues">
        /// All available values from which a random value will be selected
        /// </param>
        /// <exception cref="ArgumentException">
        /// Throws exception if no parameter will added
        /// </exception>
        public RandomListItem(params T[] allAvailableValues)
        {
            if (allAvailableValues == null || allAvailableValues.Length == 0)
            {
                const string Message = "List in RandomListItem ranomizer can not be empty!";
                throw new ArgumentException(Message);
            }

            this.allAvailableValues = allAvailableValues;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomListItem{T}"/> class.
        /// </summary>
        /// <param name="allAvailableValues">
        /// All available values from which a random value will be selected
        /// </param>
        public RandomListItem(IEnumerable<T> allAvailableValues)
            : this(allAvailableValues.ToArray())
        {
        }

        /// <summary>
        /// Gets random data for type <typeparamref name="T"/>
        /// </summary>
        /// <returns>Random data for type <typeparamref name="T"/></returns>
        public T GetValue()
        {
            int rndmListIndex = Random.Next(0, this.allAvailableValues.Length);
            return this.allAvailableValues[rndmListIndex];
        }
    }
}
