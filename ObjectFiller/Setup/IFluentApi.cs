using System;

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// This interface is implemented by the <see cref="FluentTypeApi{TTargetObject,TTargetType}"/>
    /// and <see cref="FluentPropertyApi{TTargetObject,TTargetType}"/>. It provides the common methods for both.
    /// </summary>
    /// <typeparam name="TTargetObject">Type of the object for which this setup is related to</typeparam>
    /// <typeparam name="TTargetType">Type which will be setup</typeparam>
    internal interface IFluentApi<TTargetObject, TTargetType> where TTargetObject : class
    {
        /// <summary>
        /// Defines which <see cref="Func{TResult}"/> will be used to generate a value for the given <see cref="TTargetType"/>
        /// </summary>
        /// <param name="randomizerFunc">Func which will be used to generate a value of the <see cref="TTargetType"/></param>
        /// <returns>Main FluentFiller API</returns>
        FluentFillerApi<TTargetObject> Use(Func<TTargetType> randomizerFunc);

        /// <summary>
        /// Defines which implementation of the <see cref="IRandomizerPlugin{T}"/> interface will be used to generate a value for the given <see cref="TTargetType"/>
        /// </summary>
        /// <param name="randomizerPlugin">Func which will be used to generate a value of the <see cref="TTargetType"/></param>
        /// <returns>Main FluentFiller API</returns>
        FluentFillerApi<TTargetObject> Use(IRandomizerPlugin<TTargetType> randomizerPlugin);

        /// <summary>
        /// Ignores the entity for which the fluent setup is made (Type, Property)
        /// </summary>
        /// <returns>Main FluentFiller API</returns>
        FluentFillerApi<TTargetObject> IgnoreIt();
    }
}