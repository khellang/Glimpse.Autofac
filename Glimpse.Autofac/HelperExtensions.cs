using System;
using System.Collections.Generic;
using System.Linq;

namespace Glimpse.Autofac
{
    internal static class HelperExtensions
    {
        public static string GetFriendlyName(this Type type)
        {
            if (!type.IsGenericType) return type.Name;

            var argumentList = type.GetGenericArguments().Select(GetFriendlyName).Join(", ");

            var cleanName = type.Name.Remove(type.Name.IndexOf('`'));

            return string.Format("{0}<{1}>", cleanName, argumentList);
        }

        private static string Join<T>(this IEnumerable<T> values, string separator)
        {
            return string.Join(separator, values);
        }
    }
}