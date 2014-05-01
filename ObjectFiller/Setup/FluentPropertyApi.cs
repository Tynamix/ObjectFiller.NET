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

        public FluentPropertyApi<TTargetObject, TTargetType> DoIt(At propertyOrder)
        {
            foreach (PropertyInfo propertyInfo in _affectedProps)
            {
                SetupManager.GetFor<TTargetObject>().PropertyOrder[propertyInfo] = propertyOrder;
            }
            return this;
        }

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