using System;
using System.Linq.Expressions;
using ObjectFiller.FillerPlugins;

namespace ObjectFiller
{
    /// <summary>
    /// Interface for the fluent API of the ObjectFiller.NET.
    /// </summary>
    /// <typeparam name="TTargetObject">Type of the object which will be configured</typeparam>
    public interface IFluentFillerApi<TTargetObject>
        where TTargetObject : class
    {
        /// <summary>
        /// Sets the randomizer for the given type with a function delegate.
        /// This will then be the default way to generate data for the given <see cref="TTargetType"/>.
        /// When you want to change the randomizer of a specific propery look at <seealso cref="RandomizerForProperty{TTargetType}(System.Func{TTargetType},System.Linq.Expressions.Expression{System.Func{TTargetObject,TTargetType}},System.Linq.Expressions.Expression{System.Func{TTargetObject,TTargetType}}[])"/>
        /// </summary>
        /// <typeparam name="TTargetType">Type for which the randomizer will be set. For example string, int, etc...</typeparam>
        /// <param name="randomizer">The randomizer delegate has the task to generate the random data for the given <see cref="TTargetType"/></param>
        IFluentFillerApi<TTargetObject> RandomizerForType<TTargetType>(Func<TTargetType> randomizer);

        /// <summary>
        /// Sets the randomizer for the given type with a implementation of the <see cref="IRandomizerPlugin{T}"/>.
        /// This will then be the default way to generate data for the given <see cref="TTargetType"/>.
        /// When you want to change the randomizer of a specific propery look at <seealso cref="RandomizerForProperty{TTargetType}(System.Func{TTargetType},System.Linq.Expressions.Expression{System.Func{TTargetObject,TTargetType}},System.Linq.Expressions.Expression{System.Func{TTargetObject,TTargetType}}[])"/>
        /// </summary>
        /// <typeparam name="TTargetType">Type for which the randomizer plugin will be set. For example string, int, etc...</typeparam>
        /// <param name="randomizerPlugin">The randomizer plugin has the task to generate random data for the given <see cref="TTargetType"/></param>
        IFluentFillerApi<TTargetObject> RandomizerForType<TTargetType>(IRandomizerPlugin<TTargetType> randomizerPlugin);

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
        IFluentFillerApi<TTargetObject> RandomizerForProperty<TTargetType>(Func<TTargetType> randomizer, Expression<Func<TTargetObject, TTargetType>> property, params Expression<Func<TTargetObject, TTargetType>>[] additionalProperties);

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
        ///   objectFiller.Setup().RandomizerForProperty<Person, string>(new MnemonicStringPlugin(1), person => person.FirstName, person => person.LastName)
        /// </example>
        IFluentFillerApi<TTargetObject> RandomizerForProperty<TTargetType>(IRandomizerPlugin<TTargetType> randomizerPlugin, Expression<Func<TTargetObject, TTargetType>> property, params Expression<Func<TTargetObject, TTargetType>>[] additionalProperties);

        /// <summary>
        /// ObjectFiller will ignore the given <see cref="propertyToIgnore"/> and <see cref="additionalProperties"/> when generating random data.
        /// The properties will stay untouched!
        /// </summary>
        /// <typeparam name="TTargetObject">The type of object where the target properties to ignore are located</typeparam>
        /// <param name="propertyToIgnore">Targetproperty to ignore</param>
        /// <param name="additionalProperties">OPTIONAL: Additional Properties which will be also ignored</param>
        IFluentFillerApi<TTargetObject> IgnoreProperties(Expression<Func<TTargetObject, object>> propertyToIgnore, params Expression<Func<TTargetObject, object>>[] additionalProperties);

        /// <summary>
        /// Ignore all properties of type <see cref="TTargetType"/> when generating Testdata.
        /// </summary>
        /// <typeparam name="TTargetType">Type which will be ignored</typeparam>
        /// <example>
        /// objectFiller.IgnoreAllOfType<string>();
        /// </example>
        IFluentFillerApi<TTargetObject> IgnoreAllOfType<TTargetType>();
            
        /// <summary>
        /// Setup the maximum item count for lists. The ObjectFiller will not generate more listitems then this.
        /// The default value is 25.
        /// </summary>
        /// <param name="maxCount">Max items count in a list. Default: 25</param>
        IFluentFillerApi<TTargetObject> ListItemCount(int maxCount);

        /// <summary>
        /// Setup the minimum and maximum item count for lists. The ObjectFiller will not generate more or less listitems then this limits.
        /// The default value for <see cref="minCount"/> is 1. The default value for <see cref="maxCount"/> is 25.
        /// </summary>
        /// <param name="minCount">Minimum item in a list. Default: 1</param>
        /// <param name="maxCount">Maximum item in a list. Default: 25</param>
        IFluentFillerApi<TTargetObject> ListItemCount(int minCount, int maxCount);

        /// <summary>
        /// Setup the maximum count of keys in dictionaries. The ObjectFiller will not generate more listitems then this.
        /// The default value is 10.
        /// </summary>
        /// <param name="maxCount">Max items count of keys in a dictionary. Default: 10</param>
        IFluentFillerApi<TTargetObject> DictionaryItemCount(int maxCount);

        /// <summary>
        /// Setup the minimum and maximum count of keys in dictionaries. The ObjectFiller will not generate more or less listitems then this limits.
        /// The default value for <see cref="minCount"/> is 1. The default value for <see cref="maxCount"/> is 25.
        /// </summary>
        /// <param name="minCount">Minimum items count of keys in a dictionary. Default: 1</param>
        /// <param name="maxCount">Max items count of keys in a dictionary. Default: 10</param>
        IFluentFillerApi<TTargetObject> DictionaryItemCount(int minCount, int maxCount);

        /// <summary>
        /// Register a IInterfaceMocker which will Mock your interfaces which are not registered with the <see cref="RegisterInterface{TInterface,TImplementation}"/> method.
        /// To use a Mocker like Moq or RhinoMocks, it is necessary to implement the <see cref="IInterfaceMocker"/> for this Mockingframework.
        /// </summary>
        /// <param name="mocker">Mocker which will be used to mock interfaces</param>
        /// <returns></returns>
        IFluentFillerApi<TTargetObject> SetInterfaceMocker(IInterfaceMocker mocker);

        /// <summary>
        /// Register here the implementation of a interface which may be located in your object.
        /// The objectfiller needs the real implementation of the interface to be able to generate testdata.
        /// </summary>
        /// <typeparam name="TInterface">Type of the interface which will be registered</typeparam>
        /// <typeparam name="TImplementation">Type of the interface implementation class</typeparam>
        IFluentFillerApi<TTargetObject> RegisterInterface<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;

        /// <summary>
        /// Create a setup for another type
        /// </summary>
        /// <typeparam name="TNewType">Type for which the setup will be created</typeparam>
        /// <returns></returns>
        IFluentFillerApi<TNewType> SetupFor<TNewType>(bool useDefaultSettings = false) where TNewType : class;

    }
}