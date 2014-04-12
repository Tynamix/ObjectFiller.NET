using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using ObjectFiller.FillerPlugins;

namespace ObjectFiller
{
    public class ObjectFillerApi<TTargetObject> : IFluentFillerApi<TTargetObject>
        where TTargetObject : class
    {
        /// <summary>
        /// Sets the randomizer for the given type with a function delegate.
        /// This will then be the default way to generate data for the given <see cref="TTargetType"/>.
        /// When you want to change the randomizer of a specific propery look at <seealso cref="IFluentFillerApi{TTargetObject}.RandomizerForProperty{TTargetObject,TTargetType}(System.Func{TTargetType},System.Linq.Expressions.Expression{System.Func{TTargetObject,TTargetType}}[])"/>
        /// </summary>
        /// <typeparam name="TTargetType">Type for which the randomizer will be set. For example string, int, etc...</typeparam>
        /// <param name="randomizer">The randomizer delegate has the task to generate the random data for the given <see cref="TTargetType"/></param>
        public IFluentFillerApi<TTargetObject> RandomizerForType<TTargetType>(Func<TTargetType> randomizer)
        {
            SetupManager.GetFor<TTargetObject>().TypeToRandomFunc[typeof(TTargetType)] = () => randomizer();
            return this;
        }

        /// <summary>
        /// Sets the randomizer for the given type with a implementation of the <see cref="IRandomizerPlugin{T}"/>.
        /// This will then be the default way to generate data for the given <see cref="TTargetType"/>.
        /// When you want to change the randomizer of a specific propery look at <seealso cref="IFluentFillerApi{TTargetObject}.RandomizerForProperty{TTargetObject,TTargetType}(System.Func{TTargetType},System.Linq.Expressions.Expression{System.Func{TTargetObject,TTargetType}}[])"/>
        /// </summary>
        /// <typeparam name="TTargetType">Type for which the randomizer plugin will be set. For example string, int, etc...</typeparam>
        /// <param name="randomizerPlugin">The randomizer plugin has the task to generate random data for the given <see cref="TTargetType"/></param>
        public IFluentFillerApi<TTargetObject> RandomizerForType<TTargetType>(IRandomizerPlugin<TTargetType> randomizerPlugin)
        {
            SetupManager.GetFor<TTargetObject>().TypeToRandomFunc[typeof(TTargetType)] = () => randomizerPlugin.GetValue();
            return this;
        }

        /// <summary>
        /// Sets a custom randomizer for one or more properties within a class. 
        /// So you can set a custom randomizer to a specific property within a class.
        /// </summary>
        /// <typeparam name="TTargetObject">The type of object where the target properties are located</typeparam>
        /// <typeparam name="TTargetType">The type of the target properties</typeparam>
        /// <param name="randomizer">Randomizer as function delegate which will be used to generate sample data for the given <see cref="TTargetType"/></param>
        /// <param name="property">The target property which will be filled by the customm <see cref="randomizer"/>.</param>
        /// <param name="additionalProperties">Some more properties which should be also filled by the custom <see cref="randomizer"/></param>
        /// <example>
        ///   objectFiller.Setup()..RandomizerForProperty<Person, string>(() => "SOME NAME", person => person.FirstName, person => person.LastName)
        /// </example>
        public IFluentFillerApi<TTargetObject> RandomizerForProperty<TTargetType>(Func<TTargetType> randomizer, Expression<Func<TTargetObject, TTargetType>> property,
    params Expression<Func<TTargetObject, TTargetType>>[] additionalProperties)
        {
            var properties = new List<Expression<Func<TTargetObject, TTargetType>>>() { property };


            if (additionalProperties.Length > 0)
            {
                properties.AddRange(additionalProperties);
            }

            foreach (Expression<Func<TTargetObject, TTargetType>> propertyExp in properties)
            {
                var body = propertyExp.Body as MemberExpression;
                if (body != null)
                {
                    PropertyInfo pInfo = body.Member as PropertyInfo;
                    if (pInfo != null)
                    {
                        SetupManager.GetFor<TTargetObject>().PropertyToRandomFunc.Add(pInfo, () => randomizer());
                    }
                }
            }


            return this;
        }

        /// <summary>
        /// Sets a custom randomizer for one or more properties within a class. 
        /// So you can set a custom randomizer to a specific property within a class.
        /// </summary>
        /// <typeparam name="TTargetObject">The type of object where the target properties are located</typeparam>
        /// <typeparam name="TTargetType">The type of the target properties</typeparam>
        /// <param name="randomizerPlugin">Randomizer as interface implementation of the <see cref="IRandomizerPlugin{T}"/> which will be used to generate sample data for the given <see cref="TTargetObject"/></param>
        /// <param name="property">The target property which will be filled by the customm <see cref="randomizerPlugin"/>.</param>
        /// <param name="additionalProperties">Some more properties which should be also filled by the custom <see cref="randomizerPlugin"/></param>
        /// <example>
        ///   objectFiller.Setup()..RandomizerForProperty<Person, string>(new MnemonicStringPlugin(1), person => person.FirstName, person => person.LastName)
        /// </example>
        public IFluentFillerApi<TTargetObject> RandomizerForProperty<TTargetType>(IRandomizerPlugin<TTargetType> randomizerPlugin,
            Expression<Func<TTargetObject, TTargetType>> property, params Expression<Func<TTargetObject, TTargetType>>[] additionalProperties)
        {
            RandomizerForProperty(randomizerPlugin.GetValue, property, additionalProperties);
            return this;
        }

        /// <summary>
        /// ObjectFiller will ignore the given <see cref="propertyToIgnore"/> and <see cref="additionalProperties"/> when generating random data.
        /// The properties will stay untouched!
        /// </summary>
        /// <typeparam name="TTargetObject">The type of object where the target properties to ignore are located</typeparam>
        /// <param name="propertyToIgnore">Targetproperty to ignore</param>
        /// <param name="additionalProperties">OPTIONAL: Additional Properties which will be also ignored</param>
        public IFluentFillerApi<TTargetObject> IgnoreProperties(Expression<Func<TTargetObject, object>> propertyToIgnore, params  Expression<Func<TTargetObject, object>>[] additionalProperties)
        // where TTargetObject : class
        {
            var propertiesToIgnore = new List<Expression<Func<TTargetObject, object>>>() { propertyToIgnore };
            if (additionalProperties != null && additionalProperties.Length > 0)
            {
                propertiesToIgnore.AddRange(additionalProperties);
            }

            foreach (Expression<Func<TTargetObject, object>> propToIgnore in propertiesToIgnore)
            {
                var body = propToIgnore.Body as MemberExpression;
                if (body != null)
                {
                    PropertyInfo pInfo = body.Member as PropertyInfo;
                    if (pInfo != null)
                    {
                        SetupManager.GetFor<TTargetObject>().ProperiesToIgnore.Add(pInfo);
                    }
                }
            }

            return this;
        }


        /// <summary>
        /// Setup the maximum item count for lists. The ObjectFiller will not generate more listitems then this.
        /// The default value is 25.
        /// </summary>
        /// <param name="maxCount">Max items count in a list. Default: 25</param>
        public IFluentFillerApi<TTargetObject> ListItemCount(int maxCount)
        {
            SetupManager.GetFor<TTargetObject>().ListMaxCount = maxCount;
            return this;
        }

        /// <summary>
        /// Setup the minimum and maximum item count for lists. The ObjectFiller will not generate more or less listitems then this limits.
        /// The default value for <see cref="minCount"/> is 1. The default value for <see cref="maxCount"/> is 25.
        /// </summary>
        /// <param name="minCount">Minimum item in a list. Default: 1</param>
        /// <param name="maxCount">Maximum item in a list. Default: 25</param>
        public IFluentFillerApi<TTargetObject> ListItemCount(int minCount, int maxCount)
        {
            SetupManager.GetFor<TTargetObject>().ListMinCount = minCount;
            SetupManager.GetFor<TTargetObject>().ListMaxCount = maxCount;
            return this;
        }

        /// <summary>
        /// Setup the maximum count of keys in dictionaries. The ObjectFiller will not generate more listitems then this.
        /// The default value is 10.
        /// </summary>
        /// <param name="maxCount">Max items count of keys in a dictionary. Default: 10</param>
        public IFluentFillerApi<TTargetObject> DictionaryItemCount(int maxCount)
        {
            SetupManager.GetFor<TTargetObject>().DictionaryKeyMaxCount = maxCount;
            return this;
        }

        /// <summary>
        /// Setup the minimum and maximum count of keys in dictionaries. The ObjectFiller will not generate more or less listitems then this limits.
        /// The default value for <see cref="minCount"/> is 1. The default value for <see cref="maxCount"/> is 25.
        /// </summary>
        /// <param name="minCount">Minimum items count of keys in a dictionary. Default: 1</param>
        /// <param name="maxCount">Max items count of keys in a dictionary. Default: 10</param>
        public IFluentFillerApi<TTargetObject> DictionaryItemCount(int minCount, int maxCount)
        {
            SetupManager.GetFor<TTargetObject>().DictionaryKeyMinCount = minCount;
            SetupManager.GetFor<TTargetObject>().DictionaryKeyMaxCount = maxCount;
            return this;
        }

        /// <summary>
        /// Register a IInterfaceMocker which will Mock your interfaces which are not registered with the <see cref="IFluentFillerApi{TTargetObject}.RegisterInterface{TInterface,TImplementation}"/> method.
        /// To use a Mocker like Moq or RhinoMocks, it is necessary to implement the <see cref="IInterfaceMocker"/> for this Mockingframework.
        /// </summary>
        /// <param name="mocker">Mocker which will be used to mock interfaces</param>
        /// <returns></returns>
        public IFluentFillerApi<TTargetObject> SetInterfaceMocker(IInterfaceMocker mocker)
        {
            if (SetupManager.GetFor<TTargetObject>().InterfaceMocker != null)
            {
                throw new ArgumentException("You can not set a interface mocker more than once!");
            }
            SetupManager.GetFor<TTargetObject>().InterfaceMocker = mocker;
            return this;
        }

        /// <summary>
        /// Register here the implementation of a interface which may be located in your object.
        /// The objectfiller needs the real implementation of the interface to be able to generate testdata.
        /// </summary>
        /// <typeparam name="TInterface">Type of the interface which will be registered</typeparam>
        /// <typeparam name="TImplementation">Type of the interface implementation class</typeparam>
        public IFluentFillerApi<TTargetObject> RegisterInterface<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            SetupManager.GetFor<TTargetObject>().InterfaceToImplementation.Add(typeof(TInterface), typeof(TImplementation));
            return this;
        }


        /// <summary>
        /// Create a setup for another type
        /// </summary>
        /// <typeparam name="TNewType">Type for which the setup will be created</typeparam>
        /// <returns></returns>
        public IFluentFillerApi<TNewType> SetupFor<TNewType>(bool useDefaultSettings = false) where TNewType : class
        {
            SetupManager.SetNewFor<TNewType>(useDefaultSettings);

            return new ObjectFillerApi<TNewType>();
        }
    }
}
