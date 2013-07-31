using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;
using Autofac.Core;

using Glimpse.Core.Extensibility;

namespace Glimpse.Autofac
{
    public class AutofacGlimpsePlugin : TabBase
    {
        internal static IContainer ContainerInstance { private get; set; }

        public override string Name { get { return "Autofac"; } }

        public override object GetData(ITabContext context)
        {
            if (ContainerInstance == null) return null;

            var registrations = ContainerInstance.ComponentRegistry.Registrations;

            var data = new List<object[]> { new object[] { "Services", "Implementation", "Ownership", "Sharing" } };

            data.AddRange(registrations.Select(registration => new object[]
            {
                registration.Services.Select(service => service.GetName()),
                registration.Activator.LimitType.GetFriendlyName(),
                Enum.GetName(typeof(InstanceOwnership), registration.Ownership), 
                Enum.GetName(typeof(InstanceSharing), registration.Sharing)
            }));

            return data;
        }
    }
}