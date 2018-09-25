// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRandomizerPlugin.cs" company="Tynamix">
//   © 2015 by Roman Köhler
// </copyright>
// <summary>
//   Implement this interface to create a custom randomizer of type <see cref="T" />
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// Implement this interface to create a custom randomizer of type <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">Type for which the randomizer will generate data</typeparam>
    public interface IRandomizerPlugin<out T>
    {
        /// <summary>
        /// Gets random data for type <typeparamref name="T"/>
        /// </summary>
        /// <returns>Random data for type <typeparamref name="T"/></returns>
        T GetValue();
    }
}