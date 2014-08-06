using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// The ObjectFiller.NET fills the public properties of your .NET object
    /// with random data
    /// </summary>
    /// <typeparam name="T">Targettype of the object to fill</typeparam>
    public class Filler<T> where T : class
    {
        private readonly SetupManager _setupManager;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Filler()
        {
            _setupManager = new SetupManager();
        }

        /// <summary>
        /// Call this to start the setup for the <see cref="Filler{T}"/>
        /// </summary>
        /// <returns>Fluent API setup</returns>
        public FluentFillerApi<T> Setup()
        {
            return Setup(null);
        }

        /// <summary>
        /// Call this to start the setup for the <see cref="Filler{T}"/> and use a setup which you created
        /// before with the FluentApi
        /// </summary>
        /// <param name="fillerSetupToUse">FillerSetup to use</param>
        /// <returns>Fluebt API Setup</returns>
        public FluentFillerApi<T> Setup(FillerSetup fillerSetupToUse)
        {
            if (fillerSetupToUse != null)
            {
                _setupManager.FillerSetup = fillerSetupToUse;
            }
            return new FluentFillerApi<T>(_setupManager);

        }

        /// <summary>
        /// Creates your filled object. Call this after you finished your setup with the FluentAPI and if you want
        /// to create a new object. If you want to use a existing instance use the <see cref="Fill(T)"/> method.
        /// </summary>
        public T Create()
        {
            T objectToFill = (T)CreateInstanceOfType(typeof(T), _setupManager.GetFor<T>());

            Fill(objectToFill);

            return objectToFill;
        }

        /// <summary>
        /// Creates multiple filled objects. Call this after you finished your setup with the FluentAPI and if you want
        /// to create several new objects. If you want to use a existing instance use the <see cref="Fill(T)"/> method.
        /// </summary>
        public IEnumerable<T> Create(int count)
        {
            for (int n = 0; n < count; n++)
            {
                T objectToFill = (T)CreateInstanceOfType(typeof(T), _setupManager.GetFor<T>());
                Fill(objectToFill);
                yield return objectToFill;
            }
        }

        /// <summary>
        /// Fills your object instance. Call this after you finished your setup with the FluentAPI
        /// </summary>
        public T Fill(T instanceToFill)
        {
            FillInternal(instanceToFill);

            return instanceToFill;
        }


        private object CreateInstanceOfType(Type type, FillerSetupItem currentSetupItem)
        {
            List<object> constructorArgs = new List<object>();

            if (type.GetConstructors().All(ctor => ctor.GetParameters().Length != 0))
            {
                IEnumerable<ConstructorInfo> ctorInfos;
                if ((ctorInfos = type.GetConstructors().Where(ctr => ctr.GetParameters().Length != 0)).Count() != 0)
                {
                    foreach (ConstructorInfo ctorInfo in ctorInfos.OrderBy(x => x.GetParameters().Length))
                    {
                        Type[] paramTypes = ctorInfo.GetParameters().Select(p => p.ParameterType).ToArray();

                        if (paramTypes.All(t => TypeIsValidForObjectFiller(t, currentSetupItem)))
                        {
                            foreach (Type paramType in paramTypes)
                            {
                                constructorArgs.Add(GetFilledObject(paramType, currentSetupItem));
                            }

                            break;
                        }
                    }

                    if (constructorArgs.Count == 0)
                    {
                        var message = "Could not found a constructor for type [" + type.Name + "] where the parameters can be filled with the current objectfiller setup";
                        Debug.WriteLine("ObjectFiller: " + message);
                        throw new InvalidOperationException(message);
                    }
                }
            }

            object result = Activator.CreateInstance(type, constructorArgs.ToArray());
            return result;
        }


        private void FillInternal(object objectToFill, HashStack<Type> typeTracker = null)
        {
            var currentSetup = _setupManager.GetFor(objectToFill.GetType());
            var targetType = objectToFill.GetType();

            typeTracker = typeTracker ?? new HashStack<Type>();

            if (currentSetup.TypeToRandomFunc.ContainsKey(targetType))
            {
                objectToFill = currentSetup.TypeToRandomFunc[targetType]();
                return;
            }

            var properties = targetType.GetProperties()
                             .Where(x => GetSetMethodOnDeclaringType(x) != null).ToArray();

            if (properties.Length == 0) return;

            Queue<PropertyInfo> orderedProperties = OrderPropertiers(currentSetup, properties);
            while (orderedProperties.Count != 0)
            {
                PropertyInfo property = orderedProperties.Dequeue();

                if (currentSetup.TypesToIgnore.Contains(property.PropertyType))
                {
                    continue;
                }

                if (IgnoreProperty(property, currentSetup))
                {
                    continue;
                }
                if (ContainsProperty(currentSetup.PropertyToRandomFunc.Keys, property))
                {
                    PropertyInfo p = GetPropertyFromProperties(currentSetup.PropertyToRandomFunc.Keys, property).Single();
                    SetPropertyValue(property, objectToFill, currentSetup.PropertyToRandomFunc[p]());
                    continue;
                }

                object filledObject = GetFilledObject(property.PropertyType, currentSetup, typeTracker);

                SetPropertyValue(property, objectToFill, filledObject);
            }
        }

        private void SetPropertyValue(PropertyInfo property, object objectToFill, object value)
        {
            if (property.CanWrite)
            {
                property.SetValue(objectToFill, value, null);
            }
            else
            {
                MethodInfo m = GetSetMethodOnDeclaringType(property);
                m.Invoke(objectToFill, new object[] { value });
            }
        }

        private Queue<PropertyInfo> OrderPropertiers(FillerSetupItem currentSetupItem, PropertyInfo[] properties)
        {
            Queue<PropertyInfo> propertyQueue = new Queue<PropertyInfo>();
            var firstProperties = currentSetupItem.PropertyOrder
                                              .Where(x => x.Value == At.TheBegin && ContainsProperty(properties, x.Key))
                                              .Select(x => x.Key).ToList();

            var lastProperties = currentSetupItem.PropertyOrder
                                              .Where(x => x.Value == At.TheEnd && ContainsProperty(properties, x.Key))
                                              .Select(x => x.Key).ToList();

            var propertiesWithoutOrder = properties.Where(x => !ContainsProperty(currentSetupItem.PropertyOrder.Keys, x)).ToList();


            firstProperties.ForEach(propertyQueue.Enqueue);
            propertiesWithoutOrder.ForEach(propertyQueue.Enqueue);
            lastProperties.ForEach(propertyQueue.Enqueue);

            return propertyQueue;
        }

        private bool IgnoreProperty(PropertyInfo property, FillerSetupItem currentSetupItem)
        {
            return ContainsProperty(currentSetupItem.PropertiesToIgnore, property);
        }

        private bool ContainsProperty(IEnumerable<PropertyInfo> properties, PropertyInfo property)
        {
            return GetPropertyFromProperties(properties, property).Any();
        }

        private MethodInfo GetSetMethodOnDeclaringType(PropertyInfo propInfo)
        {
            var methodInfo = propInfo.GetSetMethod(true);


            if (propInfo.DeclaringType != null)
                return methodInfo ?? propInfo
                    .DeclaringType
                    .GetProperty(propInfo.Name)
                    .GetSetMethod(true);

            return null;
        }

        private IEnumerable<PropertyInfo> GetPropertyFromProperties(IEnumerable<PropertyInfo> properties, PropertyInfo property)
        {
            return properties.Where(x => x.MetadataToken == property.MetadataToken && x.Module.Equals(property.Module));
        }

        private object GetFilledObject(Type type, FillerSetupItem currentSetupItem, HashStack<Type> typeTracker = null)
        {
            if (HasTypeARandomFunc(type, currentSetupItem))
            {
                return GetRandomValue(type, currentSetupItem);
            }

            if (TypeIsDictionary(type))
            {
                IDictionary dictionary = GetFilledDictionary(type, currentSetupItem, typeTracker);

                return dictionary;
            }

            if (TypeIsList(type))
            {
                IList list = GetFilledList(type, currentSetupItem, typeTracker);
                return list;
            }

            if (type.IsInterface)
            {
                return GetInterfaceInstance(type, currentSetupItem, typeTracker);
            }

            if (TypeIsPoco(type))
            {
                return GetFilledPoco(type, currentSetupItem, typeTracker);
            }

            if (TypeIsEnum(type))
            {
                return GetRandomEnumValue(type);
            }

            object newValue = GetRandomValue(type, currentSetupItem);
            return newValue;
        }

        private object GetRandomEnumValue(Type type)
        {
            // performance: Enum.GetValues() is slow due to reflection, should cache it
            Array values = Enum.GetValues(type);
            if (values.Length > 0)
            {
                int index = Random.Next() % values.Length;
                return values.GetValue(index);
            }
            return 0;
        }

        private bool CheckForCircularReference(Type targetType, HashStack<Type> typeTracker, FillerSetupItem currentSetupItem)
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

        private object GetFilledPoco(Type type, FillerSetupItem currentSetupItem, HashStack<Type> typeTracker)
        {
            if (CheckForCircularReference(type, typeTracker, currentSetupItem))
            {
                return GetDefaultValueOfType(type);
            }
            typeTracker.Push(type);

            object result = CreateInstanceOfType(type, currentSetupItem);

            FillInternal(result, typeTracker);

            if (typeTracker != null)
            {
                // once we fully filled the object, we can pop so other properties in the hierarchy can use the same types
                typeTracker.Pop();
            }

            return result;
        }

        private IDictionary GetFilledDictionary(Type propertyType, FillerSetupItem currentSetupItem, HashStack<Type> typeTracker)
        {
            IDictionary dictionary = (IDictionary)Activator.CreateInstance(propertyType);
            Type keyType = propertyType.GetGenericArguments()[0];
            Type valueType = propertyType.GetGenericArguments()[1];

            int maxDictionaryItems = Random.Next(currentSetupItem.DictionaryKeyMinCount,
                currentSetupItem.DictionaryKeyMaxCount);
            for (int i = 0; i < maxDictionaryItems; i++)
            {
                object keyObject = GetFilledObject(keyType, currentSetupItem, typeTracker);

                if (dictionary.Contains(keyObject))
                {
                    string message = string.Format("Generating Keyvalue failed because it generates always the same data for type [{0}]. Please check your setup.", keyType);
                    Debug.WriteLine("ObjectFiller: " + message);
                    throw new ArgumentException(message);
                }

                object valueObject = GetFilledObject(valueType, currentSetupItem, typeTracker);
                dictionary.Add(keyObject, valueObject);
            }
            return dictionary;
        }

        private static bool HasTypeARandomFunc(Type type, FillerSetupItem currentSetupItem)
        {
            return currentSetupItem.TypeToRandomFunc.ContainsKey(type);
        }


        private IList GetFilledList(Type propertyType, FillerSetupItem currentSetupItem, HashStack<Type> typeTracker)
        {
            Type genType = propertyType.GetGenericArguments()[0];

            if (CheckForCircularReference(genType, typeTracker, currentSetupItem))
            {
                return null;
            }

            IList list;
            if (!propertyType.IsInterface && propertyType.GetInterfaces().Any(x => x.GetGenericTypeDefinition() == typeof(ICollection<>)))
            {
                list = (IList)Activator.CreateInstance(propertyType);
            }
            else if (propertyType.IsGenericType
                && propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                   || propertyType.GetInterfaces().Any(x => x.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
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
                object listObject = GetFilledObject(genType, currentSetupItem, typeTracker);
                list.Add(listObject);
            }
            return list;
        }

        private object GetInterfaceInstance(Type interfaceType, FillerSetupItem setupItem, HashStack<Type> typeTracker)
        {
            object result;
            if (setupItem.TypeToRandomFunc.ContainsKey(interfaceType))
            {
                return setupItem.TypeToRandomFunc[interfaceType]();
            }
            if (setupItem.InterfaceToImplementation.ContainsKey(interfaceType))
            {
                Type implType = setupItem.InterfaceToImplementation[interfaceType];
                result = CreateInstanceOfType(implType, setupItem);
            }
            else
            {
                if (setupItem.InterfaceMocker == null)
                {
                    string message = string.Format("ObjectFiller Interface mocker missing and type [{0}] not registered", interfaceType.Name);
                    Debug.WriteLine("ObjectFiller: " + message);
                    throw new InvalidOperationException(message);
                }

                MethodInfo method = setupItem.InterfaceMocker.GetType().GetMethod("Create");
                MethodInfo genericMethod = method.MakeGenericMethod(new[] { interfaceType });
                result = genericMethod.Invoke(setupItem.InterfaceMocker, null);
            }
            FillInternal(result, typeTracker);
            return result;
        }

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
            Debug.WriteLine("ObjectFiller: " + message);
            throw new TypeInitializationException(propertyType.FullName, new Exception(message));
        }

        private static object GetDefaultValueOfType(Type propertyType)
        {
            if (propertyType.IsValueType)
            {
                return Activator.CreateInstance(propertyType);
            }
            return null;
        }

        private static bool TypeIsValidForObjectFiller(Type type, FillerSetupItem currentSetupItem)
        {
            return HasTypeARandomFunc(type, currentSetupItem)
                   || (TypeIsList(type) && ListParamTypeIsValid(type, currentSetupItem))
                   || (TypeIsDictionary(type) && DictionaryParamTypesAreValid(type, currentSetupItem))
                   || TypeIsPoco(type)
                   || (type.IsInterface
                        && currentSetupItem.InterfaceToImplementation.ContainsKey(type)
                        || currentSetupItem.InterfaceMocker != null);

        }

        private static bool DictionaryParamTypesAreValid(Type type, FillerSetupItem currentSetupItem)
        {
            if (!TypeIsDictionary(type))
            {
                return false;
            }

            Type keyType = type.GetGenericArguments()[0];
            Type valueType = type.GetGenericArguments()[1];

            return TypeIsValidForObjectFiller(keyType, currentSetupItem) &&
                   TypeIsValidForObjectFiller(valueType, currentSetupItem);
        }

        private static bool ListParamTypeIsValid(Type type, FillerSetupItem setupItem)
        {
            if (!TypeIsList(type))
            {
                return false;
            }
            Type genType = type.GetGenericArguments()[0];

            return TypeIsValidForObjectFiller(genType, setupItem);
        }

        private static bool TypeIsPoco(Type type)
        {
            return !type.IsValueType
                   && !type.IsArray
                   && type.IsClass
                   && type.GetProperties().Length > 0
                   && (type.Namespace == null
                       || (!type.Namespace.StartsWith("System")
                           && !type.Namespace.StartsWith("Microsoft")));
        }

        private static bool TypeIsDictionary(Type type)
        {
            return type.GetInterfaces().Any(x => x == typeof(IDictionary));
        }

        private static bool TypeIsList(Type type)
        {
            return !type.IsArray
                      && type.IsGenericType
                      && type.GetGenericArguments().Length != 0
                      && (type.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                        || type.GetInterfaces().Any(x => x == typeof(IEnumerable)));
        }

        private static bool TypeIsEnum(Type type)
        {
            return type.IsEnum;
        }
    }
}
