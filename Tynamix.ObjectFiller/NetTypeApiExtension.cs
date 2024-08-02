namespace Tynamix.ObjectFiller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class NetTypeApiExtension
    {
        internal static bool IsEnum(this Type source)
        {
#if NETSTANDARD
            return source.GetTypeInfo().IsEnum;
#else
            return source.IsEnum;
#endif
        }

        internal static PropertyInfo GetProperty(this Type source, string name)
        {
#if NETSTANDARD
            return source.GetTypeInfo().GetDeclaredProperty(name);
#else
            return source.GetProperty(name);
#endif
        }

        internal static IEnumerable<MethodInfo> GetMethods(this Type source)
        {
#if NETSTANDARD
            return source.GetTypeInfo().DeclaredMethods;
#else
            return source.GetMethods();
#endif
        }

        internal static MethodInfo GetSetterMethod(this PropertyInfo source)
        {
#if NETSTANDARD
            return source?.SetMethod;
#else
            return source.GetSetMethod(true);
#endif
        }

        internal static bool IsGenericType(this Type source)
        {
#if NETSTANDARD
            return source.GetTypeInfo().IsGenericType;
#else
            return source.IsGenericType;
#endif
        }

        internal static bool IsValueType(this Type source)
        {

#if NETSTANDARD
            return source.GetTypeInfo().IsValueType;
#else
            return source.IsValueType;
#endif
        }

        internal static bool IsClass(this Type source)
        {
#if NETSTANDARD
            return source.GetTypeInfo().IsClass;
#else
            return source.IsClass;
#endif
        }

        internal static bool IsInterface(this Type source)
        {
#if NETSTANDARD
                        return source.GetTypeInfo().IsInterface;
#else
            return source.IsInterface;
#endif
        }

        internal static bool IsAbstract(this Type source)
        {
#if NETSTANDARD
            return source.GetTypeInfo().IsAbstract;
#else
            return source.IsAbstract;
#endif
        }
        
        internal static bool IsAbstract(this PropertyInfo source)
        {
#if NETSTANDARD
            return source.GetMethod.IsAbstract;
#else
            var methodInfo = source.GetGetMethod() ?? source.GetSetMethod();
            return methodInfo != null && methodInfo.IsAbstract;
#endif
        }
        
        internal static Type GetBaseType(this Type source)
        {
#if NETSTANDARD
            return source.GetTypeInfo().BaseType;
#else
            return source.BaseType;
#endif
        }

        internal static IEnumerable<Type> GetImplementedInterfaces(this Type source)
        {
#if NETSTANDARD
            return source.GetTypeInfo().ImplementedInterfaces;
#else
            return source.GetInterfaces();
#endif
        }

        internal static IEnumerable<PropertyInfo> GetProperties(this Type source, bool ignoreInheritance)
        {
            if (ignoreInheritance)
            {
                return GetOwnProperties(source);
            }

            return GetPropertiesRecursively(source, new List<PropertyInfo>());
        }

        private static PropertyInfo[] GetOwnProperties(Type source)
        {
#if NETSTANDARD
            return source.GetTypeInfo().DeclaredProperties.ToArray();
#else
            return source.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
#endif
        }
        
        private static IEnumerable<PropertyInfo> GetPropertiesRecursively(Type source, List<PropertyInfo> propertyInfos)
        {
            foreach (var property in GetOwnProperties(source))
            {
                var existingProperty = propertyInfos.FirstOrDefault(p => p.Name == property.Name);
                if (existingProperty != null)
                {
                    if (IsAbstract(property))
                    {
                        // abstract properties take precedence over their concrete declaration counterpart
                        propertyInfos.Remove(existingProperty);
                        propertyInfos.Add(property);
                    }
                }
                else
                {
                    propertyInfos.Add(property);
                }
            }

            Type baseType = GetBaseType(source);
            if(baseType != null)
            {
                return GetPropertiesRecursively(baseType, propertyInfos);
            }

            return propertyInfos;
        }

        internal static Type[] GetGenericTypeArguments(this Type source)
        {
#if NETSTANDARD
            return source.GetTypeInfo().GenericTypeArguments;
#else
            return source.GetGenericArguments();
#endif
        }

        internal static IEnumerable<ConstructorInfo> GetConstructors(this Type source)
        {
#if NETSTANDARD
            return source.GetTypeInfo().DeclaredConstructors;
#else
            return source.GetConstructors();
#endif
        }

        internal static MethodInfo GetMethod(this Type source, string name)
        {
#if NETSTANDARD
            return source.GetTypeInfo().GetDeclaredMethod(name);
#else
            return source.GetMethod(name);
#endif
        }

        internal static string GetModuleName(this Type source)
        {
#if NETSTANDARD
            return source.GetTypeInfo().Module.Name;
#else
            return source.Module.ScopeName;
#endif
        }

#if (NET35 || NET40 ||  NET45 || NET451 || NET452)
        internal static Type GetTypeInfo(this Type source)
        {
            return source;
        }
#endif

#if NETSTANDARD
        internal static void ForEach<T>(this IEnumerable<T> source, Action<T> eachItem)
        {
            foreach (T item in source)
            {
                eachItem(item);
            }
        }
#endif
    }
}