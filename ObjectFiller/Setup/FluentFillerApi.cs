using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Tynamix.ObjectFiller.Setup;

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// Fluent API to configure the objectfiller.
    /// </summary>
    /// <typeparam name="TTargetObject">Type which will be configured for the ObjectFiller.NET</typeparam>
    public class FluentFillerApi<TTargetObject>
        where TTargetObject : class
    {
        private readonly SetupManager _setupManager;

        internal FluentFillerApi(SetupManager setupManager)
        {
            _setupManager = setupManager;
        }

        public FillerSetup Result
        {
            get { return _setupManager.FillerSetup; }
        }

        /// <summary>
        /// Start to configure a type for objectfiller. The follow up methods will be found in the <see cref="FluentTypeApi{TTargetObject,TTargetType}"/>
        /// </summary>
        /// <typeparam name="TTargetType">Type which will be configured. For example string, int, etc...</typeparam>
        public FluentTypeApi<TTargetObject, TTargetType> OnType<TTargetType>()
        {
            return new FluentTypeApi<TTargetObject, TTargetType>(this, _setupManager);
        }


        /// <summary>
        /// Starts to configure the behaviour of the ObjectFiller when a circular reference in your model occurs
        /// </summary>
        public FluentCircularApi<TTargetObject> OnCircularReference()
        {
            return new FluentCircularApi<TTargetObject>(this, _setupManager);
        }

        /// <summary>
        /// Starts to configure a property of the <see cref="TTargetObject"/> for objectfiller.
        /// So you can setup a custom randomizer to a specific property within a class.
        /// </summary>
        /// <typeparam name="TTargetObject">The type of object where the target properties are located</typeparam>
        /// <typeparam name="TTargetType">The type of the target properties</typeparam>
        /// <param name="property">The target property which will be setted up.</param>
        /// <param name="additionalProperties">Some more properties which should be get the same configuration</param>
        public FluentPropertyApi<TTargetObject, TTargetType> OnProperty<TTargetType>(Expression<Func<TTargetObject, TTargetType>> property, params Expression<Func<TTargetObject, TTargetType>>[] additionalProperties)
        {
            var properties = new List<Expression<Func<TTargetObject, TTargetType>>> { property };


            if (additionalProperties.Length > 0)
            {
                properties.AddRange(additionalProperties);
            }

            List<PropertyInfo> propInfos = new List<PropertyInfo>();
            foreach (Expression<Func<TTargetObject, TTargetType>> propertyExp in properties)
            {
                var body = propertyExp.Body as MemberExpression;
                if (body != null)
                {
                    PropertyInfo pInfo = body.Member as PropertyInfo;
                    if (pInfo != null)
                    {
                        propInfos.Add(pInfo);
                    }
                }
            }

            return new FluentPropertyApi<TTargetObject, TTargetType>(propInfos, this, _setupManager);
        }

        /// <summary>
        /// Setup the maximum item count for lists. The ObjectFiller will not generate more listitems then this.
        /// The default value is 25.
        /// </summary>
        /// <param name="maxCount">Max items count in a list. Default: 25</param>
        public FluentFillerApi<TTargetObject> ListItemCount(int maxCount)
        {
            _setupManager.GetFor<TTargetObject>().ListMaxCount = maxCount;
            return this;
        }

        /// <summary>
        /// Call this if the ObjectFiller should ignore all unknown types which can not filled automatically by the ObjectFiller.
        /// When you not call this method, the ObjectFiller raises an exception when it is not possible to generate a random value for that type!
        /// </summary>
        /// <returns></returns>
        public FluentFillerApi<TTargetObject> IgnoreAllUnknownTypes()
        {
            _setupManager.GetFor<TTargetObject>().IgnoreAllUnknownTypes = true;

            return this;
        }

        /// <summary>
        /// Setup the minimum and maximum item count for lists. The ObjectFiller will not generate more or less listitems then this limits.
        /// The default value for <see cref="minCount"/> is 1. The default value for <see cref="maxCount"/> is 25.
        /// </summary>
        /// <param name="minCount">Minimum item in a list. Default: 1</param>
        /// <param name="maxCount">Maximum item in a list. Default: 25</param>
        public FluentFillerApi<TTargetObject> ListItemCount(int minCount, int maxCount)
        {
            _setupManager.GetFor<TTargetObject>().ListMinCount = minCount;
            _setupManager.GetFor<TTargetObject>().ListMaxCount = maxCount;
            return this;
        }

        /// <summary>
        /// Setup the maximum count of keys in dictionaries. The ObjectFiller will not generate more listitems then this.
        /// The default value is 10.
        /// </summary>
        /// <param name="maxCount">Max items count of keys in a dictionary. Default: 10</param>
        public FluentFillerApi<TTargetObject> DictionaryItemCount(int maxCount)
        {
            _setupManager.GetFor<TTargetObject>().DictionaryKeyMaxCount = maxCount;
            return this;
        }

        /// <summary>
        /// Setup the minimum and maximum count of keys in dictionaries. The ObjectFiller will not generate more or less listitems then this limits.
        /// The default value for <see cref="minCount"/> is 1. The default value for <see cref="maxCount"/> is 25.
        /// </summary>
        /// <param name="minCount">Minimum items count of keys in a dictionary. Default: 1</param>
        /// <param name="maxCount">Max items count of keys in a dictionary. Default: 10</param>
        public FluentFillerApi<TTargetObject> DictionaryItemCount(int minCount, int maxCount)
        {
            _setupManager.GetFor<TTargetObject>().DictionaryKeyMinCount = minCount;
            _setupManager.GetFor<TTargetObject>().DictionaryKeyMaxCount = maxCount;
            return this;
        }

        /// <summary>
        /// Register a IInterfaceMocker which will Mock your interfaces which are not registered with the <see cref="IFluentFillerApi{TTargetObject}.RegisterInterface{TInterface,TImplementation}"/> method.
        /// To use a Mocker like Moq or RhinoMocks, it is necessary to implement the <see cref="IInterfaceMocker"/> for this Mockingframework.
        /// </summary>
        /// <param name="mocker">Mocker which will be used to mock interfaces</param>
        /// <returns></returns>
        public FluentFillerApi<TTargetObject> SetInterfaceMocker(IInterfaceMocker mocker)
        {
            if (_setupManager.GetFor<TTargetObject>().InterfaceMocker != null)
            {
                const string message = "You can not set a interface mocker more than once!";
                Debug.WriteLine("ObjectFiller: " + message);
                throw new ArgumentException(message);
            }
            _setupManager.GetFor<TTargetObject>().InterfaceMocker = mocker;
            return this;
        }

        /// <summary>
        /// Create a setup for another type
        /// </summary>
        /// <typeparam name="TNewType">Type for which the setup will be created</typeparam>
        /// <returns></returns>
        public FluentFillerApi<TNewType> SetupFor<TNewType>(bool useDefaultSettings = false) where TNewType : class
        {
            _setupManager.SetNewFor<TNewType>(useDefaultSettings);

            return new FluentFillerApi<TNewType>(_setupManager);
        }


    }
}
