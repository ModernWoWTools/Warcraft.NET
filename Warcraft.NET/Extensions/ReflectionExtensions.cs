using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Warcraft.NET.Extensions
{
    public static class ReflectionExtensions
    {
        private static ConcurrentDictionary<string, MethodInfo> extensionMethodCache = new();

        /// <summary>
        /// Get all extension methods by Assembly
        /// </summary>
        /// <param name="type"></param>
        /// <param name="extensionsAssembly"></param>
        /// <returns></returns>
        public static IEnumerable<MethodInfo> GetExtensionMethods(this Type type, Assembly extensionsAssembly)
        {
            var query = from t in extensionsAssembly.GetTypes()
                        where !t.IsGenericType && !t.IsNested
                        from m in t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                        where m.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false)
                        where m.GetParameters()[0].ParameterType == type
                        select m;

            return query;
        }

        public static MethodInfo GetExtensionMethod(this Type type, Assembly extensionsAssembly, string name)
        {
            // This is a bit dirty, but it allows us to cache the MethodInfo for future use as reflecting is very expensive.
            var uniqueKey = type.ToString() + "-" + extensionsAssembly.ToString() + "-" + name;

            if (extensionMethodCache.TryGetValue(uniqueKey, out var cachedMethodInfo))
            {
                return cachedMethodInfo;
            }
            else
            {
                var methodInfo = type.GetExtensionMethods(extensionsAssembly).FirstOrDefault(m => m.Name == name);
                extensionMethodCache.TryAdd(uniqueKey, methodInfo);
                return methodInfo;
            }
        }

        /// <summary>
        /// Get extension method by Assembly and method name
        /// </summary>
        /// <param name="type"></param>
        /// <param name="extensionsAssembly"></param>
        /// <param name="name"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static MethodInfo GetExtensionMethod(this Type type, Assembly extensionsAssembly, string name, Type[] types)
        {
            var methods = (from m in type.GetExtensionMethods(extensionsAssembly)
                           where m.Name == name
                           && m.GetParameters().Count() == types.Length + 1 // + 1 because extension method parameter (this)
                           select m).ToList();

            if (!methods.Any())
            {
                return default(MethodInfo);
            }

            if (methods.Count() == 1)
            {
                return methods.First();
            }

            foreach (var methodInfo in methods)
            {
                var parameters = methodInfo.GetParameters();

                bool found = true;
                for (byte b = 0; b < types.Length; b++)
                {
                    found = true;
                    if (parameters[b].GetType() != types[b])
                    {
                        found = false;
                    }
                }

                if (found)
                {
                    return methodInfo;
                }
            }

            return default(MethodInfo);
        }
    }
}
