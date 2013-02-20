using System;

using Glimpse.Autofac;

// ReSharper disable CheckNamespace
namespace Autofac
// ReSharper restore CheckNamespace
{
    public static class AutofacGlimpseExtensions
    {
        /// <summary>
        /// Activates the glimpse plugin for autofac.
        /// </summary>
        /// <param name="container">The container to use.</param>
        /// <exception cref="System.ArgumentNullException">If container is <c>null</c>.</exception>
        public static void ActivateGlimpse(this IContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            AutofacGlimpsePlugin.ContainerInstance = container;
        }
    }
}