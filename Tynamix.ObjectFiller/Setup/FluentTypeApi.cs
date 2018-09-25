// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FluentTypeApi.cs" company="Tynamix">
//   © 2015 Roman Köhler
// </copyright>
// <summary>
//   This API is just for type configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This API is just for type configuration.
    /// </summary>
    /// <typeparam name="TTargetObject">Type of the object for which this setup is related to</typeparam>
    /// <typeparam name="TTargetType">Type which will be setup</typeparam>
    public class FluentTypeApi<TTargetObject, TTargetType> : IFluentApi<TTargetObject, TTargetType>
        where TTargetObject : class
    {
        /// <summary>
        /// The callback method
        /// </summary>
        private readonly FluentFillerApi<TTargetObject> callback;

        /// <summary>
        /// The object filler setup manager.
        /// </summary>
        private readonly SetupManager setupManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentTypeApi{TTargetObject,TTargetType}"/> class.
        /// </summary>
        /// <param name="callback">
        /// The callback function
        /// </param>
        /// <param name="setupManager">
        /// The object filler setup manager.
        /// </param>
        internal FluentTypeApi(FluentFillerApi<TTargetObject> callback, SetupManager setupManager)
        {
            this.callback = callback;
            this.setupManager = setupManager;
        }

        /// <summary>
        /// Defines which static value will be used for the given <typeparamref name="TTargetType"/>
        /// </summary>
        /// <param name="valueToUse">Value which will be used</param>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> Use(TTargetType valueToUse)
        {
            return this.Use(() => valueToUse);
        }

        /// <summary>
        /// Defines which <see cref="Func{TResult}"/> will be used to generate a value for the given <typeparamref name="TTargetType"/>
        /// </summary>
        /// <param name="randomizerFunc"><see cref="Func{TTargetType}"/> which will be used to generate a value of the <typeparamref name="TTargetType"/></param>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> Use(Func<TTargetType> randomizerFunc)
        {
            this.setupManager.GetFor<TTargetObject>().TypeToRandomFunc[typeof(TTargetType)] = () => randomizerFunc();
            return this.callback;
        }

        /// <summary>
        /// Defines which implementation of the <see cref="IRandomizerPlugin{T}"/> interface will be used to generate a value for the given <typeparamref name="TTargetType"/>
        /// </summary>
        /// <param name="randomizerPlugin">A <see cref="IRandomizerPlugin{TTargetType}"/> which will be used to generate a value of the <typeparamref name="TTargetType"/></param>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> Use(IRandomizerPlugin<TTargetType> randomizerPlugin)
        {
            this.Use(randomizerPlugin.GetValue);
            return this.callback;
        }

        /// <summary>
        /// Defines which implementation of the <see cref="IRandomizerPlugin{T}"/> interface will be used to generate a value for the given <typeparamref name="TTargetType"/>
        /// </summary>
        /// <typeparam name="TRandomizerPlugin">A <see cref="IRandomizerPlugin{TTargetType}"/> which will be used to generate a value of the <typeparamref name="TTargetType"/></typeparam>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> Use<TRandomizerPlugin>()
            where TRandomizerPlugin : IRandomizerPlugin<TTargetType>, new()
        {
            this.Use(new TRandomizerPlugin());
            return this.callback;
        }

        /// <summary>
        /// Use this function if you want to use an IEnumerable for the data generation.
        /// With that you can generate random data in a specific order, with include, exclude and all the other stuff
        /// what is possible with <see cref="IEnumerable{T}"/> and LINQ
        /// </summary>
        /// <param name="enumerable">An <see cref="IEnumerable{TTargetType}"/> with items of type <typeparamref name="TTargetObject"/> which will be used to fill the data.</param>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> Use(IEnumerable<TTargetType> enumerable)
        {
            return this.Use(new EnumeratorPlugin<TTargetType>(enumerable));
        }

        /// <summary>
        /// With this method you can use a previously defined setup for specific types.
        /// </summary>
        /// <param name="setup">The setup for the type.</param>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> Use(FillerSetup setup)
        {
            var factoryMethod = Randomizer<TTargetType>.CreateFactoryMethod(setup);
            this.setupManager.GetFor<TTargetObject>().TypeToRandomFunc[typeof(TTargetType)] = () => factoryMethod();

            return this.callback;
        }

        /// <summary>
        /// Ignores the entity for which the fluent setup is made (Type, Property)
        /// </summary>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> IgnoreIt()
        {
            this.setupManager.GetFor<TTargetObject>().TypesToIgnore.Add(typeof(TTargetType));
            return this.callback;
        }

        /// <summary>
        /// Registers a implementation  <typeparamref name="TImplementation"/> of an interface <typeparamref name="TTargetType"/>
        /// The implementation must derive from the interface.
        /// </summary>
        /// <typeparam name="TImplementation">Type of the implementation which will be used to create a instance of the interface of type <typeparamref name="TTargetType"/></typeparam>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> CreateInstanceOf<TImplementation>()
            where TImplementation : class, TTargetType
        {
            this.setupManager.GetFor<TTargetObject>()
                .InterfaceToImplementation.Add(typeof(TTargetType), typeof(TImplementation));

            return this.callback;
        }
    }
}
