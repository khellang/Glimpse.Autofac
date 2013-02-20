using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Autofac;
using Autofac.Core;

using Glimpse.Core.Extensibility;

namespace Glimpse.Autofac
{
    [GlimpsePlugin]
    public class AutofacGlimpsePlugin : IGlimpsePlugin
    {
        public static IContainer ContainerInstance { private get; set; }

        public string Name { get { return "Autofac"; } }

        public object GetData(HttpContextBase context)
        {
            if (ContainerInstance == null) return null;

            var registrations = ContainerInstance.ComponentRegistry.Registrations;

            var data = new List<object[]> { new object[] { "Services", "Implementation", "Ownership", "Sharing" } };

            data.AddRange(registrations.Select(registration => new object[]
            {
                registration.Services.Select(GetServiceName),
                registration.Activator.LimitType.GetFriendlyName(),
                Enum.GetName(typeof(InstanceOwnership), registration.Ownership), 
                Enum.GetName(typeof(InstanceSharing), registration.Sharing)
            }));

            return data;
        }

        public void SetupInit() { /* Not needed */ }

        private static string GetServiceName(Service service)
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
    }

    internal static class HelperExtensions
    {
        public static string GetFriendlyName(this Type type)
        {
            if (!type.IsGenericType) return type.Name;

            var argumentList = type.GetGenericArguments()
                .Select(argument => argument.GetFriendlyName()).Join(", ");

            var cleanName = type.Name.Remove(type.Name.IndexOf('`'));

            return String.Format("{0}<{1}>", cleanName, argumentList);
        }

        private static string Join<T>(this IEnumerable<T> values, string separator)
        {
            return String.Join(separator, values);
        }
    }
}