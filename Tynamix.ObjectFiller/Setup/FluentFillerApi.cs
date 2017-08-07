// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FluentFillerApi.cs" company="Tynamix">
//   © 2015 by Roman Köhler
// </copyright>
// <summary>
//   Fluent API to configure the objectfiller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Fluent API to configure the object filler.
    /// </summary>
    /// <typeparam name="TTargetObject">Type which will be configured for the object filler</typeparam>
    public class FluentFillerApi<TTargetObject>
        where TTargetObject : class
    {
        /// <summary>
        /// The object filler setup manager.
        /// </summary>
        private readonly SetupManager setupManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentFillerApi{TTargetObject}"/> class.
        /// </summary>
        /// <param name="setupManager">
        /// The object filler setup manager.
        /// </param>
        internal FluentFillerApi(SetupManager setupManager)
        {
            this.setupManager = setupManager;
        }

        /// <summary>
        /// Gets the current object filler setup
        /// </summary>
        public FillerSetup Result
        {
            get { return this.setupManager.FillerSetup; }
        }

        /// <summary>
        /// Start to configure a type for object filler. The follow up methods will be found in the <see cref="FluentTypeApi{TTargetObject,TTargetType}"/>
        /// </summary>
        /// <typeparam name="TTargetType">
        /// Type which will be configured. For example string, integer, etc...
        /// </typeparam>
        /// <returns>
        /// <see cref="FluentTypeApi{TTargetObject,TTargetType}"/>
        /// </returns>
        public FluentTypeApi<TTargetObject, TTargetType> OnType<TTargetType>()
        {
            return new FluentTypeApi<TTargetObject, TTargetType>(this, this.setupManager);
        }


        /// <summary>
        /// Starts to configure the behavior of the ObjectFiller when a circular reference in your model occurs
        /// </summary>
        /// <returns>
        /// The <see cref="FluentCircularApi{TTargetObject}"/>.
        /// </returns>
        public FluentCircularApi<TTargetObject> OnCircularReference()
        {
            return new FluentCircularApi<TTargetObject>(this, this.setupManager);
        }

        /// <summary>
        /// Starts to configure a property of the <typeparamref name="TTargetObject"/> for object filler.
        /// So you can setup a custom randomizer to a specific property within a class.
        /// </summary>
        /// <typeparam name="TTargetType">
        /// The type of the target properties
        /// </typeparam>
        /// <param name="property">
        /// The target property which will be configured
        /// </param>
        /// <param name="additionalProperties">
        /// Some more properties which should be get the same configuration
        /// </param>
        /// <returns>
        /// The <see cref="FluentPropertyApi{TTargetObject,TTargetType}"/>.
        /// </returns>
        public FluentPropertyApi<TTargetObject, TTargetType> OnProperty<TTargetType>(Expression<Func<TTargetObject, TTargetType>> property, params Expression<Func<TTargetObject, TTargetType>>[] additionalProperties)
        {
            var properties = new List<Expression<Func<TTargetObject, TTargetType>>> { property };


            if (additionalProperties.Length > 0)
            {
                properties.AddRange(additionalProperties);
            }

            var propInfos = new List<PropertyInfo>();
            foreach (Expression<Func<TTargetObject, TTargetType>> propertyExp in properties)
            {
                var body = propertyExp.Body as MemberExpression;
                if (body != null)
                {
                    var propertyInfo = body.Member as PropertyInfo;
                    if (propertyInfo != null)
                    {
                        propInfos.Add(propertyInfo);
                    }
                }
            }

            return new FluentPropertyApi<TTargetObject, TTargetType>(propInfos, this, this.setupManager);
        }

        /// <summary>
        /// Setup the maximum item count for lists. The ObjectFiller will not generate more list items then this.
        /// The default value is 25.
        /// </summary>
        /// <param name="maxCount">
        /// Max items count in a list. Default: 25
        /// </param>
        /// <returns>
        /// The <see cref="FluentFillerApi{TTargetObject}"/>.
        /// </returns>
        public FluentFillerApi<TTargetObject> ListItemCount(int maxCount)
        {
            this.setupManager.GetFor<TTargetObject>().ListMaxCount = maxCount;
            return this;
        }


        /// <summary>
        /// Call this if the ObjectFiller should ignore all unknown types which can not filled automatically by the ObjectFiller.
        /// When you not call this method, the ObjectFiller raises an exception when it is not possible to generate a random value for that type!
        /// </summary>
        /// <returns>The <see cref="FluentFillerApi{TTargetObject}"/></returns>
        public FluentFillerApi<TTargetObject> IgnoreAllUnknownTypes()
        {
            this.setupManager.GetFor<TTargetObject>().IgnoreAllUnknownTypes = true;

            return this;
        }

        /// <summary>
        /// Call this if the ObjectFiller should ignore all properties of the base class. For example you have a class
        /// 'Student' which derives from class 'Person' and the class Person has a property 'Name'. If you want to use ObjectFiller
        /// to fill/create a student and you call this method, the name will be null because it is defined in the base class 'Person'
        /// </summary>
        /// <returns>The <see cref="FluentFillerApi{TTargetObject}"/></returns>
        public FluentFillerApi<TTargetObject> IgnoreInheritance()
        {
            this.setupManager.GetFor<TTargetObject>().IgnoreInheritance = true;

            return this;
        }

        /// <summary>
        /// Setup the minimum and maximum item count for lists. The ObjectFiller will not generate more or less list items then this limits.
        /// The default value for <paramref name="minCount"/> is 1. The default value for <paramref name="maxCount"/> is 25.
        /// </summary>
        /// <param name="minCount">
        /// Minimum item in a list. Default: 1
        /// </param>
        /// <param name="maxCount">
        /// Maximum item in a list. Default: 25
        /// </param>
        /// <returns>
        /// The <see cref="FluentFillerApi{TTargetObject}"/>
        /// </returns>
        public FluentFillerApi<TTargetObject> ListItemCount(int minCount, int maxCount)
        {
            this.setupManager.GetFor<TTargetObject>().ListMinCount = minCount;
            this.setupManager.GetFor<TTargetObject>().ListMaxCount = maxCount;
            return this;
        }

        /// <summary>
        /// Setup the maximum count of keys in dictionaries. The ObjectFiller will not generate more list items then this.
        /// The default value is 10.
        /// </summary>
        /// <param name="maxCount">
        /// Max items count of keys in a dictionary. Default: 10
        /// </param>
        /// <returns>
        /// The <see cref="FluentFillerApi{TTargetObject}"/>.
        /// </returns>
        public FluentFillerApi<TTargetObject> DictionaryItemCount(int maxCount)
        {
            this.setupManager.GetFor<TTargetObject>().DictionaryKeyMaxCount = maxCount;
            return this;
        }

        /// <summary>
        /// Setup the minimum and maximum count of keys in dictionaries. The ObjectFiller will not generate more or less list items then this limits.
        /// The default value for <paramref name="minCount"/> is 1. The default value for <paramref name="maxCount"/> is 25.
        /// </summary>
        /// <param name="minCount">
        /// Minimum items count of keys in a dictionary. Default: 1
        /// </param>
        /// <param name="maxCount">
        /// Max items count of keys in a dictionary. Default: 10
        /// </param>
        /// <returns>
        /// The <see cref="FluentFillerApi{TTargetObject}"/>.
        /// </returns>
        public FluentFillerApi<TTargetObject> DictionaryItemCount(int minCount, int maxCount)
        {
            this.setupManager.GetFor<TTargetObject>().DictionaryKeyMinCount = minCount;
            this.setupManager.GetFor<TTargetObject>().DictionaryKeyMaxCount = maxCount;
            return this;
        }

        /// <summary>
        /// Register a IInterfaceMocker which will Mock your interfaces which are not registered with the IFluentFillerApi{TTargetObject}.RegisterInterface{TInterface,TImplementation} method.
        /// To use a Mocker like MOQ or RhinoMocks, it is necessary to implement the <see cref="IInterfaceMocker"/> for this mocking framework.
        /// </summary>
        /// <param name="mocker">Mocker which will be used to mock interfaces</param>
        /// <returns>The <see cref="FluentFillerApi{TTargetObject}"/></returns>
        public FluentFillerApi<TTargetObject> SetInterfaceMocker(IInterfaceMocker mocker)
        {
            if (this.setupManager.GetFor<TTargetObject>().InterfaceMocker != null)
            {
                const string Message = "You can not set a interface mocker more than once!";
                throw new ArgumentException(Message);
            }

            this.setupManager.GetFor<TTargetObject>().InterfaceMocker = mocker;
            return this;
        }

        /// <summary>
        /// Create a setup for another type
        /// </summary>
        /// <param name="useDefaultSettings">
        /// The use Default Settings.
        /// </param>
        /// <typeparam name="TNewType">
        /// Type for which the setup will be created
        /// </typeparam>
        /// <returns>
        /// <see cref="FluentFillerApi{TTargetObject}"/>
        /// </returns>
        public FluentFillerApi<TNewType> SetupFor<TNewType>(bool useDefaultSettings = false) where TNewType : class
        {
            this.setupManager.SetNewFor<TNewType>(useDefaultSettings);

            return new FluentFillerApi<TNewType>(this.setupManager);
        }
    }
}
