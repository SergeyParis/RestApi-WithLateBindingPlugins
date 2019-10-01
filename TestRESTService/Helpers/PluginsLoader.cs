using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TestCommon.Contracts;

namespace TestRESTService.Helpers
{
    public static class PluginsLoader
    {
        private const string PathToArtifactsDebug = @"../artifacts/netcoreapp3.0/_debug/";
        private const string PathToArtifactsRelease = @"../artifacts/netcoreapp3.0/";

        private static string _currentPathToArtifacts = PathToArtifactsRelease;
        
        public static void AddPlugins(this IServiceCollection serviceCollection, IEnumerable<string> plugins)
        {
            foreach (var pluginPath in plugins)
            {
                RegisterOneGetAssembly(serviceCollection, GetFullPath(pluginPath));
            }
        }

        public static void UseDebug() => _currentPathToArtifacts = PathToArtifactsDebug;
        
        private static void RegisterOneGetAssembly(IServiceCollection serviceCollection, string path)
        {
            var assembly = Assembly.LoadFrom(path);

            var plugins = assembly.GetTypes().Where(x =>
                x.GetInterfaces().Select(contract => contract.Name.GenericName())
                    .Contains(typeof(IPlugin<>).Name.GenericName()));

            foreach (var plugin in plugins)
            {   
                var contract = plugin.GetInterfaces().First(x => x.Name.GenericName() == typeof(IPlugin<>).Name.GenericName());
                serviceCollection.Add(ServiceDescriptor.Singleton(contract, plugin));
            }
        }

        private static string GenericName(this string name) => name.Split("`").First();

        private static string GetFullPath(string projectName) =>
            $"{_currentPathToArtifacts}{projectName}.dll";
    }
}