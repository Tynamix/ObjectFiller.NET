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
        /// <summary>
        /// Default constructor
        /// </summary>
        public Filler()
        {
            SetupManager.Clear();
        }

        /// <summary>
        /// Call this to start the setup for the <see cref="Filler{T}"/>
        /// </summary>
        /// <returns>Fluent API setup</returns>
        public FluentFillerApi<T> Setup()
        {
            return new FluentFillerApi<T>();
        }


        /// <summary>
        /// This will create your object of type <see cref="T"/> and overrides the setup for the generation.
        /// Use this method if you don't wan't to use the FluentAPI
        /// </summary>
        /// <param name="setup">Setup for the objectfiller</param>
        /// <returns>Object which is filled with random data</returns>
        public T Create(ObjectFillerSetup setup)
        {
            SetupManager.SetMain(setup);
            return Create();
        }

        /// <summary>
        /// Creates your filled object. Call this after you finished your setup with the FluentAPI and if you want
        /// to create a new object. If you want to use a existing instance use the <see cref="Fill(T)"/> method.
        /// </summary>
        public T Create()
        {
            T objectToFill = (T)CreateInstanceOfType(typeof(T), SetupManager.GetFor<T>());

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
                T objectToFill = (T)CreateInstanceOfType(typeof(T), SetupManager.GetFor<T>());
                Fill(objectToFill);
                yield return objectToFill;
            }
        }

        /// <summary>
        /// This will fill your instance of an object of type <see cref="T"/> and overrides the setup for the generation.
        /// Use this method if you don't wan't to use the FluentAPI
        /// </summary>
        /// <param name="instanceToFill">The instance which will get filled with random data.</param>
        /// <param name="setup">Setup for the objectfiller</param>
        /// <returns>Instance which is filled with random data</returns>
        public T Fill(T instanceToFill, ObjectFillerSetup setup)
        {
            SetupManager.SetMain(setup);
            return Create();
        }


        /// <summary>
        /// Fills your object instance. Call this after you finished your setup with the FluentAPI
        /// </summary>
        public T Fill(T instanceToFill)
        {
            FillInternal(instanceToFill);

            return instanceToFill;
        }


        private object CreateInstanceOfType(Type type, ObjectFillerSetup currentSetup)
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

                        if (paramTypes.All(t => TypeIsValidForObjectFiller(t, currentSetup)))
                        {
                            foreach (Type paramType in paramTypes)
                            {
                                constructorArgs.Add(GetFilledObject(paramType, currentSetup));
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
            var targetType = objectToFill.GetType();

            if (typeTracker == null)
            {
                // we only really use this in GetFilledPoco but we have to start somewhere...
                typeTracker = new HashStack<Type>();
                typeTracker.Push(targetType);
            }

            var currentSetup = SetupManager.GetFor(targetType);

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

        private Queue<PropertyInfo> OrderPropertiers(ObjectFillerSetup currentSetup, PropertyInfo[] properties)
        {
            Queue<PropertyInfo> propertyQueue = new Queue<PropertyInfo>();
            var firstProperties = currentSetup.PropertyOrder
                                              .Where(x => x.Value == At.TheBegin && ContainsProperty(properties, x.Key))
                                              .Select(x => x.Key).ToList();

            var lastProperties = currentSetup.PropertyOrder
                                              .Where(x => x.Value == At.TheEnd && ContainsProperty(properties, x.Key))
                                              .Select(x => x.Key).ToList();

            var propertiesWithoutOrder = properties.Where(x => !ContainsProperty(currentSetup.PropertyOrder.Keys, x)).ToList();


            firstProperties.ForEach(propertyQueue.Enqueue);
            propertiesWithoutOrder.ForEach(propertyQueue.Enqueue);
            lastProperties.ForEach(propertyQueue.Enqueue);

            return propertyQueue;
        }

        private bool IgnoreProperty(PropertyInfo property, ObjectFillerSetup currentSetup)
        {
            return ContainsProperty(currentSetup.PropertiesToIgnore, property);
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

        private object GetFilledObject(Type type, ObjectFillerSetup currentSetup, HashStack<Type> typeTracker = null)
        {
            if (HasTypeARandomFunc(type, currentSetup))
            {
                return GetRandomValue(type, currentSetup);
            }

            if (TypeIsDictionary(type))
            {
                IDictionary dictionary = GetFilledDictionary(type, currentSetup);

                return dictionary;
            }

            if (TypeIsList(type))
            {
                IList list = GetFilledList(type, currentSetup);
                return list;
            }

            if (type.IsInterface)
            {
                return GetInterfaceInstance(type, currentSetup);
            }

            if (TypeIsPoco(type))
            {
                return GetFilledPoco(type, currentSetup, typeTracker);
            }

            if (TypeIsEnum(type))
            {
                return GetRandomEnumValue(type);
            }

            object newValue = GetRandomValue(type, currentSetup);
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

        private object GetFilledPoco(Type type, ObjectFillerSetup currentSetup, HashStack<Type> typeTracker)
        {
            if (typeTracker != null)
            {
                if (typeTracker.Contains(type))
                {
                    throw new InvalidOperationException(
                        string.Format(
                            "The type {0} was already encountered before, which probably means you have a circular reference in your model. Either ignore the properties which cause this or specify explicit creation rules for them which do not rely on types.",
                            type.Name));
                }

                typeTracker.Push(type);
            }

            object result = CreateInstanceOfType(type, currentSetup);

            FillInternal(result, typeTracker);

            if (typeTracker != null)
            {
                // once we fully filled the object, we can pop so other properties in the hierarchy can use the same types
                typeTracker.Pop();
            }

            return result;
        }

        private IDictionary GetFilledDictionary(Type propertyType, ObjectFillerSetup currentSetup)
        {
            IDictionary dictionary = (IDictionary)Activator.CreateInstance(propertyType);
            Type keyType = propertyType.GetGenericArguments()[0];
            Type valueType = propertyType.GetGenericArguments()[1];

            int maxDictionaryItems = Random.Next(currentSetup.DictionaryKeyMinCount,
                currentSetup.DictionaryKeyMaxCount);
            for (int i = 0; i < maxDictionaryItems; i++)
            {
                object keyObject = GetFilledObject(keyType, currentSetup);

                if (dictionary.Contains(keyObject))
                {
                    string message = string.Format("Generating Keyvalue failed because it generates always the same data for type [{0}]. Please check your setup.", keyType);
                    Debug.WriteLine("ObjectFiller: " + message);
                    throw new ArgumentException(message);
                }

                object valueObject = GetFilledObject(valueType, currentSetup);
                dictionary.Add(keyObject, valueObject);
            }
            return dictionary;
        }

        private static bool HasTypeARandomFunc(Type type, ObjectFillerSetup currentSetup)
        {
            return currentSetup.TypeToRandomFunc.ContainsKey(type);
        }


        private IList GetFilledList(Type propertyType, ObjectFillerSetup currentSetup)
        {
            Type genType = propertyType.GetGenericArguments()[0];

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


            int maxListItems = Random.Next(currentSetup.ListMinCount, currentSetup.ListMaxCount);
            for (int i = 0; i < maxListItems; i++)
            {
                object listObject = GetFilledObject(genType, currentSetup);
                list.Add(listObject);
            }
            return list;
        }

        private object GetInterfaceInstance(Type interfaceType, ObjectFillerSetup setup)
        {
            object result;
            if (setup.TypeToRandomFunc.ContainsKey(interfaceType))
            {
                return setup.TypeToRandomFunc[interfaceType]();
            }
            if (setup.InterfaceToImplementation.ContainsKey(interfaceType))
            {
                Type implType = setup.InterfaceToImplementation[interfaceType];
                result = CreateInstanceOfType(implType, setup);
            }
            else
            {
                if (setup.InterfaceMocker == null)
                {
                    string message = string.Format("ObjectFiller Interface mocker missing and type [{0}] not registered", interfaceType.Name);
                    Debug.WriteLine("ObjectFiller: " + message);
                    throw new InvalidOperationException(message);
                }

                MethodInfo method = setup.InterfaceMocker.GetType().GetMethod("Create");
                MethodInfo genericMethod = method.MakeGenericMethod(new[] { interfaceType });
                result = genericMethod.Invoke(setup.InterfaceMocker, null);
            }
            FillInternal(result);
            return result;
        }

        private object GetRandomValue(Type propertyType, ObjectFillerSetup setup)
        {
            if (setup.TypeToRandomFunc.ContainsKey(propertyType))
            {
                return setup.TypeToRandomFunc[propertyType]();
            }

            if (setup.IgnoreAllUnknownTypes)
            {
                if (propertyType.IsValueType)
                {
                    return Activator.CreateInstance(propertyType);
                }
                return null;
            }

            string message = "The type [" + propertyType.Name + "] was not registered in the randomizer.";
            Debug.WriteLine("ObjectFiller: " + message);
            throw new TypeInitializationException(propertyType.FullName, new Exception(message));
        }

        private static bool TypeIsValidForObjectFiller(Type type, ObjectFillerSetup currentSetup)
        {
            return HasTypeARandomFunc(type, currentSetup)
                   || (TypeIsList(type) && ListParamTypeIsValid(type, currentSetup))
                   || (TypeIsDictionary(type) && DictionaryParamTypesAreValid(type, currentSetup))
                   || TypeIsPoco(type)
                   || (type.IsInterface
                        && currentSetup.InterfaceToImplementation.ContainsKey(type)
                        || currentSetup.InterfaceMocker != null);

        }

        private static bool DictionaryParamTypesAreValid(Type type, ObjectFillerSetup currentSetup)
        {
            if (!TypeIsDictionary(type))
            {
                return false;
            }

            Type keyType = type.GetGenericArguments()[0];
            Type valueType = type.GetGenericArguments()[1];

            return TypeIsValidForObjectFiller(keyType, currentSetup) &&
                   TypeIsValidForObjectFiller(valueType, currentSetup);
        }

        private static bool ListParamTypeIsValid(Type type, ObjectFillerSetup setup)
        {
            if (!TypeIsList(type))
            {
                return false;
            }
            Type genType = type.GetGenericArguments()[0];

            return TypeIsValidForObjectFiller(genType, setup);
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
