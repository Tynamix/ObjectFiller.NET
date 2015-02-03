using System;
using System.Collections.Generic;

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// This API is just for configurating types.
    /// </summary>
    /// <typeparam name="TTargetObject">Type of the object for which this setup is related to</typeparam>
    /// <typeparam name="TTargetType">Type which will be setup</typeparam>
    public class FluentTypeApi<TTargetObject, TTargetType> : IFluentApi<TTargetObject, TTargetType>
        where TTargetObject : class
    {
        private readonly FluentFillerApi<TTargetObject> _callback;
        private readonly SetupManager _setupManager;

        internal FluentTypeApi(FluentFillerApi<TTargetObject> callback, SetupManager setupManager)
        {
            _callback = callback;
            _setupManager = setupManager;
        }

        /// <summary>
        /// Defines which static value will be used for the given <see cref="TTargetType"/>
        /// </summary>
        /// <param name="valueToUse">Value which will be used</param>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> Use(TTargetType valueToUse)
        {
            return Use(() => valueToUse);
        }

        /// <summary>
        /// Defines which <see cref="Func{TResult}"/> will be used to generate a value for the given <see cref="TTargetType"/>
        /// </summary>
        /// <param name="randomizerFunc">Func which will be used to generate a value of the <see cref="TTargetType"/></param>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> Use(Func<TTargetType> randomizerFunc)
        {
            _setupManager.GetFor<TTargetObject>().TypeToRandomFunc[typeof(TTargetType)] = () => randomizerFunc();
            return _callback;
        }

        /// <summary>
        /// Defines which implementation of the <see cref="IRandomizerPlugin{T}"/> interface will be used to generate a value for the given <see cref="TTargetType"/>
        /// </summary>
        /// <param name="randomizerPlugin">Func which will be used to generate a value of the <see cref="TTargetType"/></param>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> Use(IRandomizerPlugin<TTargetType> randomizerPlugin)
        {
            Use(randomizerPlugin.GetValue);
            return _callback;
        }

        /// <summary>
        /// Use this function if you want to use an IEnumerable for the datageneration.
        /// With that you can generate random data in a specific order, with include, exclude and all the other stuff
        /// what is possible with IEnumerables and LINQ
        /// </summary>
        /// <param name="enumerable">An IEnumerable with items of type <typeparam name="TTargetObject"/> which will be used to fill the data.</param>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> Use(IEnumerable<TTargetType> enumerable)
        {
            return Use(new EnumeratorPlugin<TTargetType>(enumerable));
        }

        /// <summary>
        /// Ignores the entity for which the fluent setup is made (Type, Property)
        /// </summary>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> IgnoreIt()
        {
            _setupManager.GetFor<TTargetObject>().TypesToIgnore.Add(typeof(TTargetType));
            return _callback;
        }

        /// <summary>
        /// Registers a implementation  <typeparam name="TImplementation"/> of an interface <typeparam name="TTargetType"/>
        /// The implementation must derive from the interface.
        /// </summary>
        /// <typeparam name="TImplementation"/>Type of the implementation which will be used to create a instance of the interface of type <typeparam name="TTargetType"/>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> CreateInstanceOf<TImplementation>()
            where TImplementation : class,TTargetType
        {
            _setupManager.GetFor<TTargetObject>().InterfaceToImplementation.Add(typeof(TTargetType), typeof(TImplementation));

            return _callback;
        }

    }
}
