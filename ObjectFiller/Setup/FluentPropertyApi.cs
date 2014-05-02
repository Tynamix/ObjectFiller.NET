using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// This fluent API is responsible for the property specific configuration.
    /// </summary>
    /// <typeparam name="TTargetObject">Type of the object for which this setup is related to</typeparam>
    /// <typeparam name="TTargetType">Type of the property which will be setup</typeparam>
    public class FluentPropertyApi<TTargetObject, TTargetType>
        : IFluentApi<TTargetObject, TTargetType> where TTargetObject : class
    {
        private readonly IEnumerable<PropertyInfo> _affectedProps;
        private readonly FluentFillerApi<TTargetObject> _callback;

        internal FluentPropertyApi(IEnumerable<PropertyInfo> affectedProps, FluentFillerApi<TTargetObject> callback)
        {
            _affectedProps = affectedProps;
            _callback = callback;
        }

        /// <summary>
        /// Specify when the objectfiller will fill the property.
        /// At the end or the beginning. This is usefull if the properties are related to another.
        /// </summary>
        /// <param name="propertyOrder">Defines if the property will be filled at "TheBegin" or at "TheEnd"</param>
        /// <returns>FluentPropertyAPI to define which </returns>
        public FluentPropertyApi<TTargetObject, TTargetType> DoIt(At propertyOrder)
        {
            foreach (PropertyInfo propertyInfo in _affectedProps)
            {
                SetupManager.GetFor<TTargetObject>().PropertyOrder[propertyInfo] = propertyOrder;
            }
            return this;
        }

        /// <summary>
        /// If a property is not writable because of  private setter you can allow the objectfiller
        /// to write it anyway
        /// </summary>
        /// <returns></returns>
        public FluentPropertyApi<TTargetObject, TTargetType> WritePrivateSetter()
        {
            SetupManager.GetFor<TTargetObject>().PropertiesWritePrivateSetter.AddRange(_affectedProps);

            return this;
        }

        /// <summary>
        /// Use the default random generator method for the given type.
        /// Its usefull when you want to define the order of the property with <see cref="DoIt"/>, but you
        /// don't want to define the random generator.
        /// </summary>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> UseDefault()
        {
            return _callback;
        }

        /// <summary>
        /// Defines which <see cref="Func{TResult}"/> will be used to generate a value for the given <see cref="TTargetType"/>
        /// </summary>
        /// <param name="randomizerFunc">Func which will be used to generate a value of the <see cref="TTargetType"/></param>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> Use(Func<TTargetType> randomizerFunc)
        {
            foreach (PropertyInfo pInfo in _affectedProps)
            {
                SetupManager.GetFor<TTargetObject>().PropertyToRandomFunc[pInfo] = () => randomizerFunc();
            }
            return _callback;
        }

        /// <summary>
        /// Defines which implementation of the <see cref="IRandomizerPlugin{T}"/> interface will be used to generate a value for the given <see cref="TTargetType"/>
        /// </summary>
        /// <param name="randomizerPlugin">Func which will be used to generate a value of the <see cref="TTargetType"/></param>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> Use(IRandomizerPlugin<TTargetType> randomizerPlugin)
        {
            return Use(randomizerPlugin.GetValue);
        }

        /// <summary>
        /// Ignores the entity for which the fluent setup is made (Type, Property)
        /// </summary>
        /// <returns>Main FluentFiller API</returns>
        public FluentFillerApi<TTargetObject> IgnoreIt()
        {
            foreach (PropertyInfo pInfo in _affectedProps)
            {
                SetupManager.GetFor<TTargetObject>().PropertiesToIgnore.Add(pInfo);
            }

            return _callback;
        }
    }
}
