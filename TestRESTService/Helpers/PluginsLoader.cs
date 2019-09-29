using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TestCommon.Contracts;
using TestCommon.Domain;

namespace TestRESTService.Helpers
{
    public static class PluginsLoader
    {
        public static void AddPlugins(this IServiceCollection serviceCollection, IEnumerable<PluginInfo> plugins)
        {
            foreach (var pluginPath in plugins)
            {
                RegisterOneGetAssembly(serviceCollection, GetFullPath(pluginPath.ProjectName, pluginPath.RelativePath));
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

                //var instance = Activator.CreateInstance(plugin);
                //serviceCollection.Add(new ServiceDescriptor(@interface, instance));
                serviceCollection.Add(ServiceDescriptor.Singleton(@interface, plugin));
            }
        }

        private static string GenericName(this string name) => name.Split("`").First();

        private static string GetFullPath(string projectName, string relativePath) =>
            $".\\{relativePath}..\\{projectName}\\bin\\Debug\\netcoreapp3.0\\{projectName}.dll";
    }
}