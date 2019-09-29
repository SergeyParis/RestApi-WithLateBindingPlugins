using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TestCommon.Contracts;
using TestCommon.Domain;

namespace TestRESTService.Helpers
{
    public static class PluginsLoader
    {
        public static void AddPlugins(this IServiceCollection serviceCollection, IEnumerable<string> pluginsPath)
        {
            foreach (var pluginPath in pluginsPath)
            {
                RegisterOneGetAssembly(serviceCollection, pluginPath);
            }
        }

        private static void RegisterOneGetAssembly(IServiceCollection serviceCollection, string path)
        {
            var assembly = Assembly.LoadFrom(path);

            var plugins = assembly.GetTypes().Where(x =>
                x.GetInterfaces().Select(@interface => @interface.Name.GenericName())
                    .Contains(typeof(IPlugin<>).Name.GenericName()));

            foreach (var plugin in plugins)
            {
                var @interface = plugin.GetInterfaces().First(x => x.Name.GenericName() == typeof(IPlugin<>).Name.GenericName());

                var instance = Activator.CreateInstance(plugin);
                serviceCollection.Add(new ServiceDescriptor(@interface,  instance));
            }
        }

        private static string GenericName(this string name) => name.Split("`").First();
    }
}