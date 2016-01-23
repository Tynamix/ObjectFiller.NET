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
#if (NET3X || NET4X)
            return source.IsEnum;
#endif

#if (NETSTD)
            return source.GetTypeInfo().IsEnum;
#endif
        }

        internal static PropertyInfo GetProperty(this Type source, string name)
        {
#if (NET3X || NET4X)
            return source.GetProperty(name);
#endif

#if (NETSTD)
            return source.GetTypeInfo().GetDeclaredProperty(name);
#endif
        }

        internal static IEnumerable<MethodInfo> GetMethods(this Type source)
        {
#if (NET3X || NET4X)
            return source.GetMethods();
#endif

#if (NETSTD)
            return source.GetTypeInfo().DeclaredMethods;
#endif
        }

        internal static MethodInfo GetSetterMethod(this PropertyInfo source)
        {
#if (NET3X || NET4X)
            return source.GetSetMethod(true);
#else
            return source.SetMethod;
#endif
        }

        internal static bool IsGenericType(this Type source)
        {
#if (NET3X || NET4X)
            return source.IsGenericType;
#endif

#if (NETSTD)
            return source.GetTypeInfo().IsGenericType;
#endif
        }

        internal static bool IsValueType(this Type source)
        {
#if (NET3X || NET4X)
            return source.IsValueType;
#endif

#if (NETSTD)
            return source.GetTypeInfo().IsValueType;
#endif
        }

        internal static bool IsClass(this Type source)
        {
#if (NET3X || NET4X)
            return source.IsClass;
#endif

#if (NETSTD)
            return source.GetTypeInfo().IsClass;
#endif
        }

        internal static bool IsInterface(this Type source)
        {
#if (NET3X || NET4X)
            return source.IsInterface;
#endif

#if (NETSTD)
            return source.GetTypeInfo().IsInterface;
#endif
        }

        internal static bool IsAbstract(this Type source)
        {
#if (NET3X || NET4X)
            return source.IsAbstract;
#endif

#if (NETSTD)
            return source.GetTypeInfo().IsAbstract;
#endif
        }

        internal static IEnumerable<Type> GetImplementedInterfaces(this Type source)
        {
#if (NET3X || NET4X)
            return source.GetInterfaces();
#endif

#if (NETSTD)
            return source.GetTypeInfo().ImplementedInterfaces;
#endif
        }

        internal static IEnumerable<PropertyInfo> GetProperties(this Type source)
        {
#if (NET3X || NET4X)
            return source.GetProperties();
#endif

#if (NETSTD)

            var propertyInfos = source.GetTypeInfo().DeclaredProperties.ToList();
            if (source.GetTypeInfo().BaseType != null)
            {
                foreach (var property in source.GetTypeInfo().BaseType.GetTypeInfo().DeclaredProperties)
                {
                    if (!propertyInfos.Any(x => x.Name == property.Name))
                    {
                        propertyInfos.Add(property);
                    }
                }
            }
            return propertyInfos;
#endif
        }

        internal static Type[] GetGenericTypeArguments(this Type source)
        {
#if (NET3X || NET4X)
            return source.GetGenericArguments();
#endif

#if (NETSTD)
            return source.GetTypeInfo().GenericTypeArguments;
#endif
        }

        internal static IEnumerable<ConstructorInfo> GetConstructors(this Type source)
        {
#if (NET3X || NET4X)
            return source.GetConstructors();
#endif

#if (NETSTD)
            return source.GetTypeInfo().DeclaredConstructors;
#endif
        }

        internal static MethodInfo GetMethod(this Type source, string name)
        {
#if (NET3X || NET4X)
            return source.GetMethod(name);
#endif

#if (NETSTD)
            return source.GetTypeInfo().GetDeclaredMethod(name);
#endif
        }

        internal static string GetModuleName(this Type source)
        {
#if (NET3X || NET4X)
            return source.Module.ScopeName;
#endif

#if (NETSTD)
            return source.GetTypeInfo().Module.Name;
#endif
        }

#if (NET3X || NET4X)
        internal static Type GetTypeInfo(this Type source)
        {
            return source;
        }
#endif

#if (NETSTD)
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