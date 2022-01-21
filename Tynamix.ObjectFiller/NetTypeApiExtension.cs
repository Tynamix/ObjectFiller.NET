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
            return source.SetMethod;
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
#if NETSTANDARD

            if (ignoreInheritance)
            {
                return source.GetTypeInfo().DeclaredProperties.ToList();
            }

            return GetDeclaredPropertyInfosRecursive(new List<PropertyInfo>(), source.GetTypeInfo());
#else

            if (ignoreInheritance)
            {
                return source.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
            }

            return source.GetProperties();
#endif

        }

#if NETSTANDARD

        internal static List<PropertyInfo> GetDeclaredPropertyInfosRecursive(List<PropertyInfo> propertyInfos, TypeInfo typeInfo)
        {
            foreach (var property in typeInfo.DeclaredProperties)
            {
                if (!propertyInfos.Any(x => x.Name == property.Name))
                {
                    propertyInfos.Add(property);
                }
            }

            if(typeInfo.BaseType != null)
            {
                return GetDeclaredPropertyInfosRecursive(propertyInfos, typeInfo.BaseType.GetTypeInfo());
            }

            return propertyInfos;
        }

#endif



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