using System;
using System.Collections.Generic;
using System.Linq;

using Autofac.Core;

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

        public static string GetName(this Service service)
        {
            string serviceName = null;

            var serviceWithType = service as IServiceWithType;
            if (serviceWithType != null)
            {
                serviceName = serviceWithType.ServiceType.GetFriendlyName();
            }

            var keyedService = service as KeyedService;
            if (keyedService != null)
            {
                serviceName += string.Format(" ({0})", keyedService.ServiceKey);
            }

            return serviceName;
        }

        private static string Join<T>(this IEnumerable<T> values, string separator)
        {
            return string.Join(separator, values);
        }
    }
}