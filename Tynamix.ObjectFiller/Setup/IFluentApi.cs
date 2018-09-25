// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFluentApi.cs" company="Tynamix">
//   © Roman Köhler
// </copyright>
// <summary>
//   This interface is implemented by the <see cref="FluentTypeApi{TTargetObject,TTargetType}" />
//   and <see cref="FluentPropertyApi{TTargetObject,TTargetType}" />. It provides the common methods for both.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This interface is implemented by the <see cref="FluentTypeApi{TTargetObject,TTargetType}"/>
    /// and <see cref="FluentPropertyApi{TTargetObject,TTargetType}"/>. It provides the common methods for both.
    /// </summary>
    /// <typeparam name="TTargetObject">Type of the object for which this setup is related to</typeparam>
    /// <typeparam name="TTargetType">Type which will be setup</typeparam>
    internal interface IFluentApi<TTargetObject, TTargetType> where TTargetObject : class
    {
        /// <summary>
        /// Defines which static value will be used for the given <typeparamref name="TTargetType"/>
        /// </summary>
        /// <param name="valueToUse">Value which will be used</param>
        /// <returns>Main FluentFiller API</returns>
        FluentFillerApi<TTargetObject> Use(TTargetType valueToUse);

        /// <summary>
        /// Defines which <see cref="Func{TResult}"/> will be used to generate a value for the given <typeparamref name="TTargetType"/>
        /// </summary>
        /// <param name="randomizerFunc"><see cref="Func{TTargetType}"/> which will be used to generate a value of the <typeparamref name="TTargetType"/></param>
        /// <returns>Main FluentFiller API</returns>
        FluentFillerApi<TTargetObject> Use(Func<TTargetType> randomizerFunc);

        /// <summary>
        /// Defines which implementation of the <see cref="IRandomizerPlugin{T}"/> interface will be used to generate a value for the given <typeparamref name="TTargetType"/>
        /// </summary>
        /// <param name="randomizerPlugin">A <see cref="IRandomizerPlugin{TTargetType}"/> which will be used to generate a value of the <typeparamref name="TTargetType"/></param>
        /// <returns>Main FluentFiller API</returns>
        FluentFillerApi<TTargetObject> Use(IRandomizerPlugin<TTargetType> randomizerPlugin);

        /// <summary>
        /// Use this function if you want to use an IEnumerable for the data generation.
        /// With that you can generate random data in a specific order, with include, exclude and all the other stuff
        /// what is possible with <see cref="IEnumerable{T}"/> and LINQ
        /// </summary>
        /// <param name="enumerable">An <see cref="IEnumerable{TTargetType}"/> with items of type <typeparamref name="TTargetObject"/> which will be used to fill the data.</param>
        /// <returns>Main FluentFiller API</returns>
        FluentFillerApi<TTargetObject> Use(IEnumerable<TTargetType> enumerable);

        /// <summary>
        /// Ignores the entity for which the fluent setup is made (Type, Property)
        /// </summary>
        /// <returns>Main FluentFiller API</returns>
        FluentFillerApi<TTargetObject> IgnoreIt();
    }
}