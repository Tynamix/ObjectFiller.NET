// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FluentPropertyApi.cs" company="Tynamix">
//   © 2015 by Roman Köhler
// </copyright>
// <summary>
//   This fluent API is responsible for the property specific configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// This fluent API is responsible for the property specific configuration.
    /// </summary>
    /// <typeparam name="TTargetObject">Type of the object for which this setup is related to</typeparam>
    /// <typeparam name="TTargetType">Type of the property which will be setup</typeparam>
    public class FluentPropertyApi<TTargetObject, TTargetType>
        : IFluentApi<TTargetObject, TTargetType> where TTargetObject : class
    {
        /// <summary>
        /// The affected properties
        /// </summary>
        private readonly IEnumerable<PropertyInfo> affectedProperties;

        /// <summary>
        /// The callback function
        /// </summary>
        private readonly FluentFillerApi<TTargetObject> callback;

        /// <summary>
        /// The object filler setup manager
        /// </summary>
        private readonly SetupManager setupManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentPropertyApi{TTargetObject,TTargetType}"/> class.
        /// </summary>
        /// <param name="affectedProperties">
        /// The affected properties.
        /// </param>
        /// <param name="callback">
        /// The callback.
        /// </param>
        /// <param name="setupManager">
        /// The object filler setup manager
        /// </param>
        internal FluentPropertyApi(IEnumerable<PropertyInfo> affectedProperties, FluentFillerApi<TTargetObject> callback, SetupManager setupManager)
        {
            this.affectedProperties = affectedProperties;
            this.callback = callback;
            this.setupManager = setupManager;
        }

        /// <summary>
        /// Specify when the object filler will fill the property.
        /// At the end or the beginning. This is useful if the properties are related to another.
        /// </summary>
        /// <param name="propertyOrder">Defines if the property will be filled at "TheBegin" or at "TheEnd"</param>
        /// <returns>FluentPropertyAPI to define which </returns>
        public FluentPropertyApi<TTargetObject, TTargetType> DoIt(At propertyOrder)
        {
            foreach (PropertyInfo propertyInfo in this.affectedProperties)
            {
                this.setupManager.GetFor<TTargetObject>().PropertyOrder[propertyInfo] = propertyOrder;
            }

            return this;
        }

        /// <summary>
        /// Use the default random generator method for the given type.
        /// Its useful when you want to define the order of the property with <see cref="DoIt"/>, but you
        /// don't want to define the random generator.
        /// </summary>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> UseDefault()
        {
            return this.callback;
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
            foreach (PropertyInfo propertyInfo in this.affectedProperties)
            {
                this.setupManager.GetFor<TTargetObject>().PropertyToRandomFunc[propertyInfo] = () => randomizerFunc();
            }

            return this.callback;
        }

        /// <summary>
        /// Defines which <see cref="FillerSetup"/> is used to generate a value for the given <typeparamref name="TTargetType"/>
        /// </summary>
        /// <param name="setup">The setup which is used for configuration.</param>
        public FluentFillerApi<TTargetObject> Use(FillerSetup setup)
        {
            foreach (PropertyInfo propertyInfo in this.affectedProperties)
            {
                var factoryMethod = Randomizer<TTargetType>.CreateFactoryMethod(setup);
                this.setupManager.GetFor<TTargetObject>().PropertyToRandomFunc[propertyInfo] = () => factoryMethod();
            }

            return this.callback;
        }

        /// <summary>
        /// Defines which implementation of the <see cref="IRandomizerPlugin{T}"/> interface will be used to generate a value for the given <typeparamref name="TTargetType"/>
        /// </summary>
        /// <param name="randomizerPlugin">A <see cref="IRandomizerPlugin{TTargetType}"/> which will be used to generate a value of the <typeparamref name="TTargetType"/></param>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> Use(IRandomizerPlugin<TTargetType> randomizerPlugin)
        {
            return this.Use(randomizerPlugin.GetValue);
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
        /// Ignores the entity for which the fluent setup is made (Type, Property)
        /// </summary>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> IgnoreIt()
        {
            foreach (PropertyInfo propertyInfo in this.affectedProperties)
            {
                this.setupManager.GetFor<TTargetObject>().PropertiesToIgnore.Add(propertyInfo);
            }

            return this.callback;
        }
    }
}
