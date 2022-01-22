// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Filler.cs" company="Tynamix">
//   © 2015 by Roman Köhler
// </copyright>
// <summary>
//   The ObjectFiller.NET fills the public properties of your .NET object
//   with random data
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The ObjectFiller.NET fills the public properties of your .NET object
    /// with random data
    /// </summary>
    /// <typeparam name="T">
    /// Target dictionaryType of the object to fill
    /// </typeparam>
    public class Filler<T>
        where T : class
    {
        #region Fields

        /// <summary>
        /// The setup manager contains the setup per dictionaryType
        /// </summary>
        private readonly SetupManager setupManager;

        /// <summary>
        /// True when the filler will just fill properties which are set up explicit
        /// </summary>
        private bool justConfiguredProperties;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Filler{T}"/> class.
        /// </summary>
        public Filler()
        {
            this.setupManager = new SetupManager();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Creates your filled object. Call this after you finished your setup with the FluentAPI and if you want
        /// to create a new object. If you want to use a existing instance use the <see cref="Fill(T)"/> method.
        /// </summary>
        /// <returns>
        /// A created and filled instance of dictionaryType <typeparamref name="T"/>
        /// </returns>
        public T Create()
        {
            T objectToFill;
            var hashStack = new HashStack<Type>();

            if (!TypeIsClrType(typeof(T)))
            {
                objectToFill = (T)this.CreateInstanceOfType(typeof(T), this.setupManager.GetFor<T>(), hashStack);
                this.Fill(objectToFill);
            }
            else
            {
                objectToFill = (T)this.CreateAndFillObject(typeof(T), this.setupManager.GetFor<T>(), hashStack);
            }

            return objectToFill;
        }

        /// <summary>
        /// Creates multiple filled objects. Call this after you finished your setup with the FluentAPI and if you want
        /// to create several new objects. If you want to use a existing instance use the <see cref="Fill(T)"/> method.
        /// </summary>
        /// <param name="count">
        /// Count of instances to create
        /// </param>
        /// <returns>
        /// <see cref="IEnumerable{T}"/> with created and filled instances of dictionaryType <typeparamref name="T"/>
        /// </returns>
        public IEnumerable<T> Create(int count)
        {
            IList<T> items = new List<T>();
            for (int n = 0; n < count; n++)
            {
                items.Add(this.Create());
            }

            return items;
        }

        /// <summary>
        /// Fills your object instance. Call this after you finished your setup with the FluentAPI
        /// </summary>
        /// <param name="instanceToFill">
        /// The instance To fill.
        /// </param>
        /// <returns>
        /// The filled instance of dictionaryType <typeparamref name="T"/>.
        /// </returns>
        public T Fill(T instanceToFill)
        {
            this.FillInternal(instanceToFill);

            return instanceToFill;
        }

        /// <summary>
        /// Call this to start the setup for the <see cref="Filler{T}"/>
        /// </summary>
        /// <returns>Fluent API setup</returns>
        public FluentFillerApi<T> Setup()
        {
            return this.Setup(false);
        }

        /// <summary>
        /// Call this to start the setup for the <see cref="Filler{T}"/>
        /// </summary>
        /// <param name="explicitSetup">True if just properties shall get filled which configured in filler setup.</param>
        /// <returns>Fluent API setup</returns>
        public FluentFillerApi<T> Setup(bool explicitSetup)
        {
            return this.Setup(null, explicitSetup);
        }

        /// <summary>
        /// Call this to start the setup for the <see cref="Filler{T}"/> and use a setup which you created
        /// before with the <see cref="IFluentApi{TTargetObject,TTargetType}"/>
        /// </summary>
        /// <param name="fillerSetupToUse">
        /// FillerSetup to use
        /// </param>
        /// <returns>
        /// Fluent API Setup
        /// </returns>
        public FluentFillerApi<T> Setup(FillerSetup fillerSetupToUse)
        {
            return this.Setup(fillerSetupToUse, false);
        }

        /// <summary>
        /// Call this to start the setup for the <see cref="Filler{T}"/> and use a setup which you created
        /// before with the <see cref="IFluentApi{TTargetObject,TTargetType}"/>
        /// </summary>
        /// <param name="fillerSetupToUse">
        /// FillerSetup to use
        /// </param>
        /// <param name="explicitSetup">True if just properties shall get filled which configured in filler setup.</param>
        /// <returns>
        /// Fluent API Setup
        /// </returns>
        public FluentFillerApi<T> Setup(FillerSetup fillerSetupToUse, bool explicitSetup)
        {
            if (fillerSetupToUse != null)
            {
                this.setupManager.FillerSetup = fillerSetupToUse;
            }

            this.justConfiguredProperties = explicitSetup;
            return new FluentFillerApi<T>(this.setupManager);
        }

        /// <summary>
        /// Set a random seed to generate always the same data for the same seed.
        /// </summary>
        /// <param name="seed">Number for the data generation</param>
        /// <returns>This ObjectFiller instance</returns>
        public Filler<T> SetRandomSeed(int seed)
        {
            Random.SetSeed(seed);
            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the dictionary parameter types are valid to use with object filler
        /// </summary>
        /// <param name="dictionaryType">
        /// The type of the dictionary.
        /// </param>
        /// <param name="currentSetupItem">
        /// The current setup item.
        /// </param>
        /// <returns>
        /// True if the dictionary parameter types are valid for use with object filler
        /// </returns>
        private static bool DictionaryParamTypesAreValid(Type dictionaryType, FillerSetupItem currentSetupItem)
        {
            if (!TypeIsDictionary(dictionaryType))
            {
                return false;
            }

            Type keyType = dictionaryType.GetGenericTypeArguments()[0];
            Type valueType = dictionaryType.GetGenericTypeArguments()[1];

            return TypeIsValidForObjectFiller(keyType, currentSetupItem)
                   && TypeIsValidForObjectFiller(valueType, currentSetupItem);
        }

        /// <summary>
        /// Creates a default value for the given <paramref name="propertyType"/>
        /// </summary>
        /// <param name="propertyType">
        /// The property dictionaryType.
        /// </param>
        /// <returns>
        /// Default value for the given <paramref name="propertyType"/>
        /// </returns>
        private static object GetDefaultValueOfType(Type propertyType)
        {
            if (propertyType.IsValueType())
            {
                return Activator.CreateInstance(propertyType);
            }

            return null;
        }

        /// <summary>
        /// Checks if there is a random function for the given <see cref="Type"/>
        /// </summary>
        /// <param name="type">
        /// The dictionaryType.
        /// </param>
        /// <param name="currentSetupItem">
        /// The current setup item.
        /// </param>
        /// <returns>
        /// True if there is a random function in the <paramref name="currentSetupItem"/> for the given <see cref="Type"/>
        /// </returns>
        private static bool HasTypeARandomFunc(Type type, FillerSetupItem currentSetupItem)
        {
            return currentSetupItem.TypeToRandomFunc.ContainsKey(type);
        }

        /// <summary>
        /// Checks if the list parameter type are valid to use with object filler
        /// </summary>
        /// <param name="listType">
        /// The type of the list.
        /// </param>
        /// <param name="currentSetupItem">
        /// The current setup item.
        /// </param>
        /// <returns>
        /// True if the list parameter types are valid for use with object filler
        /// </returns>
        private static bool ListParamTypeIsValid(Type listType, FillerSetupItem currentSetupItem)
        {
            if (!TypeIsList(listType))
            {
                return false;
            }

            Type genType = listType.GetGenericTypeArguments()[0];

            return TypeIsValidForObjectFiller(genType, currentSetupItem);
        }

        /// <summary>
        /// Checks if the given <see cref="Type"/> is a dictionary
        /// </summary>
        /// <param name="type">
        /// The type to check
        /// </param>
        /// <returns>
        /// True if the target <see cref="Type"/>  is a dictionary
        /// </returns>
        private static bool TypeIsDictionary(Type type)
        {
            Type interfaceType = typeof(IDictionary);
            Type genericType = typeof(IDictionary<,>);

            if (type.IsInterface())
            {
                return type.IsGenericType() && 
                    (
                        type.GetGenericTypeDefinition() == genericType || 
                        type.GetImplementedInterfaces().Any(x => x.IsGenericType() &&  x.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                    );
            }
         
            return type.GetImplementedInterfaces().Any(x => x == interfaceType);
        }

        /// <summary>
        /// Checks if the given <see cref="Type"/> is a list
        /// </summary>
        /// <param name="type">
        /// The type to check
        /// </param>
        /// <returns>
        /// True if the target <see cref="Type"/>  is a list
        /// </returns>
        private static bool TypeIsList(Type type)
        {
            Predicate<Type> typeIsList = (typeToCheck) => !typeToCheck.IsArray
                   && typeToCheck.IsGenericType()
                   && typeToCheck.GetGenericTypeArguments().Length != 0
                   && (typeToCheck.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                       || typeToCheck.GetImplementedInterfaces().Any(x => x == typeof(IList)));

            return typeIsList(type) || (type.GetTypeInfo().BaseType != null && typeIsList(type.GetTypeInfo().BaseType));
        }

        /// <summary>
        /// Checks if the given <see cref="Type"/> is a list
        /// </summary>
        /// <param name="type">
        /// The type to check
        /// </param>
        /// <returns>
        /// True if the target <see cref="Type"/>  is a list
        /// </returns>
        private static bool TypeIsCollection(Type type)
        {
            return !type.IsArray
                   && type.IsGenericType()
                   && type.GetGenericTypeArguments().Length != 0
                   && (type.GetGenericTypeDefinition() == typeof(ICollection<>)
#if (!NET35 && !NET40)
                       || type.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>)
#endif
                       || type.GetImplementedInterfaces().Any(x => x.IsGenericType() && (
#if (!NET35 && !NET40)
                      x.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>) ||
#endif
                       x.GetGenericTypeDefinition() == typeof(ICollection<>))));
        }

        /// <summary>
        /// Checks if the given <see cref="Type"/> is a plain old class object
        /// </summary>
        /// <param name="type">
        /// The type to check
        /// </param>
        /// <returns>
        /// True if the target <see cref="Type"/> is a plain old class object
        /// </returns>
        private static bool TypeIsPoco(Type type)
        {
            return !type.IsValueType() && !type.IsArray && type.IsClass() && type.GetProperties(false).Any()
                   && (type.Namespace == null
                       || (!type.Namespace.StartsWith("System") && !type.Namespace.StartsWith("Microsoft")));
        }

        /// <summary>
        /// Check if the given type is a type from the common language library
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns>True if the given type is a type from the common language library</returns>
        private static bool TypeIsClrType(Type type)
        {
            return (type.Namespace != null && (type.Namespace.StartsWith("System") || type.Namespace.StartsWith("Microsoft")))
                    || type.GetModuleName() == "CommonLanguageRuntimeLibrary";
        }

        /// <summary>
        /// Checks if the given <see cref="Type"/> can be used by the object filler
        /// </summary>
        /// <param name="type">
        /// The dictionaryType which will be checked
        /// </param>
        /// <param name="currentSetupItem">
        /// The current setup item.
        /// </param>
        /// <returns>
        /// True when the <see cref="Type"/> can be used with object filler
        /// </returns>
        private static bool TypeIsValidForObjectFiller(Type type, FillerSetupItem currentSetupItem)
        {
            var result = HasTypeARandomFunc(type, currentSetupItem)
                   || (TypeIsList(type) && ListParamTypeIsValid(type, currentSetupItem))
                   || (TypeIsDictionary(type) && DictionaryParamTypesAreValid(type, currentSetupItem))
                   || TypeIsPoco(type)
                   || TypeIsEnum(type)
                   || (type.IsInterface() && currentSetupItem.InterfaceToImplementation.ContainsKey(type)
                       || currentSetupItem.InterfaceMocker != null);

            return result;
        }

        /// <summary>
        /// Checks if the given dictionaryType was already been used in the object hierarchy
        /// </summary>
        /// <param name="targetType">
        /// The target dictionaryType.
        /// </param>
        /// <param name="typeTracker">
        /// The dictionaryType tracker to find circular dependencies
        /// </param>
        /// <param name="currentSetupItem">
        /// The current setup item.
        /// </param>
        /// <returns>
        /// True if there is a circular dependency
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Throws exception if a circular dependency exists and the setup is set to throw exception on circular dependency
        /// </exception>
        private bool CheckForCircularReference(
            Type targetType,
            HashStack<Type> typeTracker,
            FillerSetupItem currentSetupItem)
        {
            if (typeTracker != null)
            {
                if (typeTracker.Contains(targetType))
                {
                    if (currentSetupItem.ThrowExceptionOnCircularReference)
                    {
                        throw new InvalidOperationException(
                            string.Format(
                                "The type {0} was already encountered before, which probably means you have a circular reference in your model. Either ignore the properties which cause this or specify explicit creation rules for them which do not rely on types.",
                                targetType.Name));
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a <paramref name="property"/> exists in the given list of <paramref name="properties"/>
        /// </summary>
        /// <param name="properties">
        /// Source properties where to check if the <paramref name="property"/> is contained
        /// </param>
        /// <param name="property">
        /// The property which will be checked
        /// </param>
        /// <returns>
        /// True if the <paramref name="property"/> is in the list of <paramref name="properties"/>
        /// </returns>
        private bool ContainsProperty(IEnumerable<PropertyInfo> properties, PropertyInfo property)
        {
            return this.GetPropertyFromProperties(properties, property).Any();
        }

        /// <summary>
        /// Creates a object of the target <see cref="Type"/> and fills it up with data according to the given <paramref name="currentSetupItem"/>
        /// </summary>
        /// <param name="type">
        /// The target dictionaryType to create and fill
        /// </param>
        /// <param name="currentSetupItem">
        /// The current setup item.
        /// </param>
        /// <param name="typeTracker">
        /// The dictionaryType tracker to find circular dependencies
        /// </param>
        /// <returns>
        /// The created and filled object of the given <see cref="Type"/>
        /// </returns>
        private object CreateAndFillObject(
            Type type,
            FillerSetupItem currentSetupItem,
            HashStack<Type> typeTracker = null)
        {
            if (HasTypeARandomFunc(type, currentSetupItem))
            {
                return this.GetRandomValue(type, currentSetupItem);
            }

            if (TypeIsDictionary(type))
            {
                IDictionary dictionary = this.GetFilledDictionary(type, currentSetupItem, typeTracker);

                return dictionary;
            }

            if (TypeIsList(type))
            {
                IList list = this.GetFilledList(type, currentSetupItem, typeTracker);

                return list;
            }

            if (TypeIsCollection(type))
            {
                IEnumerable collection = this.GetFilledCollection(type, currentSetupItem, typeTracker);

                return collection;
            }

            if (TypeIsArray(type))
            {
                Array array = this.GetFilledArray(type, currentSetupItem, typeTracker);
                return array;
            }

            if (type.IsInterface() || type.IsAbstract())
            {
                return this.CreateInstanceOfInterfaceOrAbstractClass(type, currentSetupItem, typeTracker);
            }

            if (TypeIsEnum(type) || TypeIsNullableEnum(type))
            {
                return this.GetRandomEnumValue(type);
            }

            if (TypeIsPoco(type))
            {
                return this.GetFilledPoco(type, currentSetupItem, typeTracker);
            }

            object newValue = this.GetRandomValue(type, currentSetupItem);
            return newValue;
        }

        /// <summary>
        /// Creates and filles an array or jagged array
        /// </summary>
        /// <param name="type">Array type to create and fill</param>
        /// <param name="currentSetupItem">Current ObjectFiller.NET Setup item</param>
        /// <param name="typeTracker">
        /// The type tracker to find circular dependencies
        /// </param>
        /// <returns>Created and filled array</returns>
        private Array GetFilledArray(Type type, FillerSetupItem currentSetupItem, HashStack<Type> typeTracker)
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(type.GetElementType());

            var list = this.GetFilledList(constructedListType, currentSetupItem, typeTracker);

            var array = (Array)Activator.CreateInstance(type, new object[] { list.Count });

            list.CopyTo(array, 0);

            return array;
        }

        /// <summary>
        /// Creates a instance of an interface or abstract class. Like an IoC-Framework
        /// </summary>
        /// <param name="interfaceType">
        /// The dictionaryType of interface or abstract class
        /// </param>
        /// <param name="setupItem">
        /// The setup item.
        /// </param>
        /// <param name="typeTracker">
        /// The type tracker to find circular dependencies
        /// </param>
        /// <returns>
        /// The created and filled instance of the <paramref name="interfaceType"/>
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Throws Exception if no dictionaryType was registered for the given <paramref name="interfaceType"/>
        /// </exception>
        private object CreateInstanceOfInterfaceOrAbstractClass(
            Type interfaceType,
            FillerSetupItem setupItem,
            HashStack<Type> typeTracker)
        {
            object result;
            if (setupItem.TypeToRandomFunc.ContainsKey(interfaceType))
            {
                return setupItem.TypeToRandomFunc[interfaceType]();
            }

            if (setupItem.InterfaceToImplementation.ContainsKey(interfaceType))
            {
                Type implType = setupItem.InterfaceToImplementation[interfaceType];
                result = this.CreateInstanceOfType(implType, setupItem, typeTracker);
            }
            else
            {
                if (setupItem.InterfaceMocker == null)
                {
                    string message =
                        string.Format(
                            "ObjectFiller Interface mocker missing and type [{0}] not registered",
                            interfaceType.Name);
                    throw new InvalidOperationException(message);
                }

                MethodInfo method = setupItem.InterfaceMocker.GetType().GetMethod("Create");
                MethodInfo genericMethod = method.MakeGenericMethod(new[] { interfaceType });
                result = genericMethod.Invoke(setupItem.InterfaceMocker, null);
            }

            this.FillInternal(result, typeTracker);
            return result;
        }

        /// <summary>
        /// Creates a instance of the given <see cref="Type"/>
        /// </summary>
        /// <param name="type">
        /// The dictionaryType to create
        /// </param>
        /// <param name="currentSetupItem">
        /// The setup for the current object dictionaryType
        /// </param>
        /// <param name="typeTracker">
        /// The dictionaryType tracker to find circular dependencies
        /// </param>
        /// <returns>
        /// Created instance of the given <see cref="Type"/>
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Throws exception if the constructor could not be created by filler setup
        /// </exception>
        private object CreateInstanceOfType(Type type, FillerSetupItem currentSetupItem, HashStack<Type> typeTracker)
        {
            var constructorArgs = new List<object>();

            if (type.GetConstructors().All(ctor => ctor.GetParameters().Length != 0))
            {
                IEnumerable<ConstructorInfo> ctorInfos;
                if ((ctorInfos = type.GetConstructors().Where(ctr => ctr.GetParameters().Length != 0)).Any())
                {
                    foreach (ConstructorInfo ctorInfo in ctorInfos.OrderBy(x => x.GetParameters().Length))
                    {
                        Type[] paramTypes = ctorInfo.GetParameters().Select(p => p.ParameterType).ToArray();

                        if (paramTypes.All(ctorParamType => TypeIsValidForObjectFiller(ctorParamType, currentSetupItem) && ctorParamType != type))
                        {
                            foreach (Type paramType in paramTypes)
                            {
                                constructorArgs.Add(this.CreateAndFillObject(paramType, currentSetupItem, typeTracker));
                            }

                            break;
                        }
                    }

                    if (constructorArgs.Count == 0)
                    {
                        var message = "Could not found a constructor for type [" + type.Name
                                      + "] where the parameters can be filled with the current objectfiller setup";
                        throw new InvalidOperationException(message);
                    }
                }
            }

            object result = Activator.CreateInstance(type, constructorArgs.ToArray());
            return result;
        }

        /// <summary>
        /// Fills the given <paramref name="objectToFill"/> with random data
        /// </summary>
        /// <param name="objectToFill">
        /// The object to fill.
        /// </param>
        /// <param name="typeTracker">
        /// The dictionaryType tracker to find circular dependencies
        /// </param>
        private void FillInternal(object objectToFill, HashStack<Type> typeTracker = null)
        {
            var currentSetup = this.setupManager.GetFor(objectToFill.GetType());
            var targetType = objectToFill.GetType();

            typeTracker = typeTracker ?? new HashStack<Type>();

            if (currentSetup.TypeToRandomFunc.ContainsKey(targetType))
            {
                objectToFill = currentSetup.TypeToRandomFunc[targetType]();
                return;
            }

            var properties = targetType.GetProperties(currentSetup.IgnoreInheritance)
                                       .Where(prop => this.GetSetMethodOnDeclaringType(prop) != null)
                                       .ToArray();

            this.FillProperties(objectToFill, properties, currentSetup, typeTracker);
        }

        /// <summary>
        /// This method will fill the given <paramref name="properties"/> of the given <paramref name="objectToFill"/>
        /// </summary>
        /// <param name="objectToFill">The object to fill</param>
        /// <param name="properties">The properties of the <paramref name="objectToFill"/> which shall get filled</param>
        /// <param name="currentSetup">
        /// The setup for the current object 
        /// </param>
        /// <param name="typeTracker">
        /// The dictionaryType tracker to find circular dependencies
        /// </param>
        private void FillProperties(object objectToFill, PropertyInfo[] properties, FillerSetupItem currentSetup, HashStack<Type> typeTracker)
        {
            if (properties.Length == 0)
            {
                return;
            }

            Queue<PropertyInfo> orderedProperties = this.OrderPropertiers(currentSetup, properties);
            while (orderedProperties.Count != 0)
            {
                PropertyInfo property = orderedProperties.Dequeue();

                if (currentSetup.TypesToIgnore.Contains(property.PropertyType))
                {
                    continue;
                }

                if (this.IgnoreProperty(property, currentSetup))
                {
                    continue;
                }

                if (this.ContainsProperty(currentSetup.PropertyToRandomFunc.Keys, property))
                {
                    PropertyInfo propertyInfo =
                        this.GetPropertyFromProperties(currentSetup.PropertyToRandomFunc.Keys, property).Single();
                    this.SetPropertyValue(property, objectToFill, currentSetup.PropertyToRandomFunc[propertyInfo]());
                    continue;
                }

                if (this.justConfiguredProperties
                    && !this.setupManager.FillerSetup.TypeToFillerSetup.ContainsKey(property.PropertyType))
                {
                    continue;
                }

                object filledObject = this.CreateAndFillObject(property.PropertyType, currentSetup, typeTracker);

                this.SetPropertyValue(property, objectToFill, filledObject);
            }
        }

        /// <summary>
        /// Creates and fills a dictionary of the target <paramref name="propertyType"/>
        /// </summary>
        /// <param name="propertyType">
        /// The dictionaryType of the dictionary
        /// </param>
        /// <param name="currentSetupItem">
        /// The current setup item.
        /// </param>
        /// <param name="typeTracker">
        /// The dictionaryType tracker to find circular dependencies
        /// </param>
        /// <returns>
        /// A created and filled dictionary
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Throws exception if the setup was made in a way that the keys of the dictionary are always the same
        /// </exception>
        private IDictionary GetFilledDictionary(
            Type propertyType,
            FillerSetupItem currentSetupItem,
            HashStack<Type> typeTracker)
        {

            bool derivedType = !propertyType.GetGenericTypeArguments().Any();

            Type keyType = !derivedType
                            ? propertyType.GetGenericTypeArguments()[0]
                            : propertyType.GetTypeInfo().BaseType.GetGenericTypeArguments()[0];

            Type valueType = !derivedType
                            ? propertyType.GetGenericTypeArguments()[1]
                            : propertyType.GetTypeInfo().BaseType.GetGenericTypeArguments()[1];

            IDictionary dictionary;
            if (!propertyType.IsInterface()
                && propertyType.GetImplementedInterfaces().Any(x => x == typeof(IDictionary)))
            {
                dictionary = (IDictionary)Activator.CreateInstance(propertyType);
            }
            else if (propertyType.IsGenericType() && propertyType.GetGenericTypeDefinition() == typeof(IDictionary<,>)
                     || propertyType.GetImplementedInterfaces().Any(x => x.IsGenericType() && x.GetGenericTypeDefinition() == typeof(IDictionary<,>)))
            {
                Type openDictionaryType = typeof(Dictionary<,>);
                Type genericDictionaryType = openDictionaryType.MakeGenericType(keyType, valueType);
                dictionary = (IDictionary)Activator.CreateInstance(genericDictionaryType);
            }
            else
            {
                dictionary = (IDictionary)Activator.CreateInstance(propertyType);
            }

            int maxDictionaryItems = 0;

            if (keyType.IsEnum())
            {
                maxDictionaryItems = Enum.GetValues(keyType).Length;
            }
            else
            {
                maxDictionaryItems = Random.Next(
                currentSetupItem.DictionaryKeyMinCount,
                currentSetupItem.DictionaryKeyMaxCount);
            }

            for (int i = 0; i < maxDictionaryItems; i++)
            {
                object keyObject = null;
                if (keyType.IsEnum())
                {
                    keyObject = Enum.GetValues(keyType).GetValue(i);
                }
                else
                {
                    keyObject = this.CreateAndFillObject(keyType, currentSetupItem, typeTracker);
                }

                if (dictionary.Contains(keyObject))
                {
                    string message =
                        string.Format(
                            "Generating Keyvalue failed because it generates always the same data for dictionaryType [{0}]. Please check your setup.",
                            keyType);
                    throw new ArgumentException(message);
                }

                object valueObject = this.CreateAndFillObject(valueType, currentSetupItem, typeTracker);
                dictionary.Add(keyObject, valueObject);
            }

            if (derivedType)
            {

                var remainingProperties = propertyType.GetProperties(true)
                                          .Where(prop => this.GetSetMethodOnDeclaringType(prop) != null)
                                          .ToArray();

                this.FillProperties(dictionary, remainingProperties, currentSetupItem, typeTracker);
            }

            return dictionary;
        }

        /// <summary>
        /// Creates and fills a list of the given <paramref name="propertyType"/>
        /// </summary>
        /// <param name="propertyType">
        /// Type of the list
        /// </param>
        /// <param name="currentSetupItem">
        /// The current setup item.
        /// </param>
        /// <param name="typeTracker">
        /// The dictionaryType tracker to find circular dependencies
        /// </param>
        /// <returns>
        /// Created and filled list of the given <paramref name="propertyType"/>
        /// </returns>
        private IList GetFilledList(Type propertyType, FillerSetupItem currentSetupItem, HashStack<Type> typeTracker)
        {
            bool derivedList = !propertyType.GetGenericTypeArguments().Any();
            Type genType = !derivedList ? propertyType.GetGenericTypeArguments()[0]
                                   : propertyType.GetTypeInfo().BaseType.GetGenericTypeArguments()[0];

            if (this.CheckForCircularReference(genType, typeTracker, currentSetupItem))
            {
                return null;
            }

            IList list;
            if (!propertyType.IsInterface()
                && propertyType.GetImplementedInterfaces().Any(x => x == typeof(IList)))
            {
                list = (IList)Activator.CreateInstance(propertyType);
            }
            else if (propertyType.IsGenericType() && propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                     || propertyType.GetImplementedInterfaces().Any(x => x.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                Type openListType = typeof(List<>);
                Type genericListType = openListType.MakeGenericType(genType);
                list = (IList)Activator.CreateInstance(genericListType);
            }
            else
            {
                list = (IList)Activator.CreateInstance(propertyType);
            }

            int maxListItems = Random.Next(currentSetupItem.ListMinCount, currentSetupItem.ListMaxCount);
            for (int i = 0; i < maxListItems; i++)
            {
                object listObject = this.CreateAndFillObject(genType, currentSetupItem, typeTracker);
                list.Add(listObject);
            }

            if (derivedList)
            {

                var remainingProperties = propertyType.GetProperties(true)
                                          .Where(prop => this.GetSetMethodOnDeclaringType(prop) != null)
                                          .ToArray();

                this.FillProperties(list, remainingProperties, currentSetupItem, typeTracker);
            }

            return list;
        }

        /// <summary>
        /// Creates and fills a list of the given <paramref name="propertyType"/>
        /// </summary>
        /// <param name="propertyType">
        /// Type of the list
        /// </param>
        /// <param name="currentSetupItem">
        /// The current setup item.
        /// </param>
        /// <param name="typeTracker">
        /// The dictionaryType tracker to find circular dependencies
        /// </param>
        /// <returns>
        /// Created and filled list of the given <paramref name="propertyType"/>
        /// </returns>
        private IEnumerable GetFilledCollection(Type propertyType, FillerSetupItem currentSetupItem, HashStack<Type> typeTracker)
        {
            Type genType = propertyType.GetGenericTypeArguments()[0];

            if (this.CheckForCircularReference(genType, typeTracker, currentSetupItem))
            {
                return null;
            }

            IEnumerable target;

            if (!propertyType.IsInterface()
                && propertyType.GetImplementedInterfaces().Any(x => x.GetGenericTypeDefinition() == typeof(ICollection<>)
#if (!NET35 && !NET40)
                || x.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>)
#endif
                ))
            {
                target = (IEnumerable)Activator.CreateInstance(propertyType);
            }
            else if (propertyType.IsGenericType() && 
                        propertyType.GetGenericTypeDefinition() == typeof(ICollection<>)
                     || propertyType.GetImplementedInterfaces().Any(x => x.IsGenericType() && x.GetGenericTypeDefinition() == typeof(ICollection<>))
#if (!NET35 && !NET40)
                     || propertyType.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>)
                     || propertyType.GetImplementedInterfaces().Any(x => x.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>))
#endif
                )
            {
                Type openListType = typeof(List<>);
                Type genericListType = openListType.MakeGenericType(genType);
                target = (IEnumerable)Activator.CreateInstance(genericListType);
            }
            else
            {
                target = (IEnumerable)Activator.CreateInstance(propertyType);
            }

            int maxListItems = Random.Next(currentSetupItem.ListMinCount, currentSetupItem.ListMaxCount);
            for (int i = 0; i < maxListItems; i++)
            {
                object listObject = this.CreateAndFillObject(genType, currentSetupItem, typeTracker);
                MethodInfo method = target.GetType().GetMethod("Add");
                method.Invoke(target, new object[] { listObject });
            }

            return target;
        }

        /// <summary>
        /// Creates and fills a POCO class
        /// </summary>
        /// <param name="type">
        /// The target dictionaryType.
        /// </param>
        /// <param name="currentSetupItem">
        /// The current setup item.
        /// </param>
        /// <param name="typeTracker">
        /// The dictionaryType tracker to find circular dependencies
        /// </param>
        /// <returns>
        /// The created and filled POCO class
        /// </returns>
        private object GetFilledPoco(Type type, FillerSetupItem currentSetupItem, HashStack<Type> typeTracker)
        {
            if (this.CheckForCircularReference(type, typeTracker, currentSetupItem))
            {
                return GetDefaultValueOfType(type);
            }

            typeTracker.Push(type);

            object result = this.CreateInstanceOfType(type, currentSetupItem, typeTracker);

            this.FillInternal(result, typeTracker);

            typeTracker.Pop();

            return result;
        }

        /// <summary>
        /// Selects the given <paramref name="property"/> from the given list of <paramref name="properties"/>
        /// </summary>
        /// <param name="properties">
        /// All properties where the target <paramref name="property"/> will be searched in
        /// </param>
        /// <param name="property">
        /// The target property.
        /// </param>
        /// <returns>
        /// All properties from <paramref name="properties"/> which are the same as the target <paramref name="property"/>
        /// </returns>
        private IEnumerable<PropertyInfo> GetPropertyFromProperties(
            IEnumerable<PropertyInfo> properties,
            PropertyInfo property)
        {
            return properties.Where(x => x.Name == property.Name
                                         && x.Module.Equals(property.Module)
                                         && x.DeclaringType?.AssemblyQualifiedName == property.DeclaringType?.AssemblyQualifiedName);
        }

        /// <summary>
        /// Gets a random value for an enumeration
        /// </summary>
        /// <param name="type">
        /// Type of the enumeration
        /// </param>
        /// <returns>
        /// A default value for an enumeration
        /// </returns>
        private object GetRandomEnumValue(Type type)
        {
            // performance: Enum.GetValues() is slow due to reflection, should cache it

            var enumType = type.IsEnum() ? type : Nullable.GetUnderlyingType(type);

            Array values = Enum.GetValues(enumType);
            if (values.Length > 0)
            {
                int index = Random.Next() % values.Length;
                return values.GetValue(index);
            }

            return 0;
        }

        /// <summary>
        /// Gets a random value of the given <paramref name="propertyType"/>
        /// </summary>
        /// <param name="propertyType">
        /// The property dictionaryType.
        /// </param>
        /// <param name="setupItem">
        /// The setup item.
        /// </param>
        /// <returns>
        /// A random value of the given <paramref name="propertyType"/>
        /// </returns>
        /// <exception cref="TypeInitializationException">
        /// Throws exception if object filler was not able to create random data
        /// </exception>
        private object GetRandomValue(Type propertyType, FillerSetupItem setupItem)
        {
            if (setupItem.TypeToRandomFunc.ContainsKey(propertyType))
            {
                return setupItem.TypeToRandomFunc[propertyType]();
            }

            if (setupItem.IgnoreAllUnknownTypes)
            {
                return GetDefaultValueOfType(propertyType);
            }

            string message = "The type [" + propertyType.Name + "] was not registered in the randomizer.";
            throw new TypeInitializationException(propertyType.FullName, new Exception(message));
        }

        /// <summary>
        /// Gets the setter of a <paramref name="propInfo"/>
        /// </summary>
        /// <param name="propInfo">
        /// The <see cref="PropertyInfo"/> for which the setter method will be found
        /// </param>
        /// <returns>
        /// The setter of the property as <see cref="MethodInfo"/>
        /// </returns>
        private MethodInfo GetSetMethodOnDeclaringType(PropertyInfo propInfo)
        {
            var methodInfo = propInfo.GetSetterMethod();

            if (propInfo.DeclaringType != null)
            {
                return methodInfo ?? propInfo.DeclaringType.GetProperty(propInfo.Name).GetSetterMethod();
            }

            return null;
        }

        /// <summary>
        /// Checks if a property is ignored by the <paramref name="currentSetupItem"/>
        /// </summary>
        /// <param name="property">
        /// The property to check for ignorance
        /// </param>
        /// <param name="currentSetupItem">
        /// The current setup item.
        /// </param>
        /// <returns>
        /// True if the <paramref name="property"/> should be ignored
        /// </returns>
        private bool IgnoreProperty(PropertyInfo property, FillerSetupItem currentSetupItem)
        {
            return this.ContainsProperty(currentSetupItem.PropertiesToIgnore, property);
        }

        /// <summary>
        /// Sorts the properties like the <paramref name="currentSetupItem"/> wants to have it
        /// </summary>
        /// <param name="currentSetupItem">
        /// The current setup item.
        /// </param>
        /// <param name="properties">
        /// The properties to sort
        /// </param>
        /// <returns>
        /// Sorted properties as a queue
        /// </returns>
        private Queue<PropertyInfo> OrderPropertiers(FillerSetupItem currentSetupItem, PropertyInfo[] properties)
        {
            var propertyQueue = new Queue<PropertyInfo>();
            var firstProperties =
                currentSetupItem.PropertyOrder.Where(
                    x => x.Value == At.TheBegin && this.ContainsProperty(properties, x.Key)).Select(x => x.Key).ToList();

            var lastProperties =
                currentSetupItem.PropertyOrder.Where(
                    x => x.Value == At.TheEnd && this.ContainsProperty(properties, x.Key)).Select(x => x.Key).ToList();

            var propertiesWithoutOrder =
                properties.Where(x => !this.ContainsProperty(currentSetupItem.PropertyOrder.Keys, x)).ToList();

            firstProperties.ForEach(propertyQueue.Enqueue);
            propertiesWithoutOrder.ForEach(propertyQueue.Enqueue);
            lastProperties.ForEach(propertyQueue.Enqueue);

            return propertyQueue;
        }

        /// <summary>
        /// Sets the given <paramref name="value"/> on the given <paramref name="property"/> for the given <paramref name="objectToFill"/> 
        /// </summary>
        /// <param name="property">
        /// The property to set
        /// </param>
        /// <param name="objectToFill">
        /// The object to fill.
        /// </param>
        /// <param name="value">
        /// The value for the <paramref name="property"/>
        /// </param>
        private void SetPropertyValue(PropertyInfo property, object objectToFill, object value)
        {
            if (property.CanWrite)
            {
                property.SetValue(objectToFill, value, null);
            }
            else
            {
                MethodInfo m = this.GetSetMethodOnDeclaringType(property);
                m.Invoke(objectToFill, new[] { value });
            }
        }

        /// <summary>
        /// Checks if the given <see cref="Type"/> is a enumeration
        /// </summary>
        /// <param name="type">
        /// The type to check
        /// </param>
        /// <returns>
        /// True if the target <see cref="Type"/>  is a enumeration
        /// </returns>
        private static bool TypeIsEnum(Type type)
        {
            return type.IsEnum();
        }

        /// <summary>
        /// Checks if the given <see cref="Type"/> is a nullable enum
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns>True if the type is a nullable enum</returns>
        private static bool TypeIsNullableEnum(Type type)
        {
            Type u = Nullable.GetUnderlyingType(type);
            return (u != null) && u.IsEnum();
        }

        /// <summary>
        /// Checks if the given <see cref="Type"/> is a supported array
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns>True if the type is a array</returns>
        private bool TypeIsArray(Type type)
        {
            return type.IsArray && type.GetArrayRank() == 1;
        }

#endregion
    }
}