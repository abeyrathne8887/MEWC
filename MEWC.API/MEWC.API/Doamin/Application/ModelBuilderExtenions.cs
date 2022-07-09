using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MEWC.API.Doamin.Application
{
    public static class ModelBuilderExtenions
    {
        private static IEnumerable<Type> GetMappingTypes(this Assembly assembly, Type mappingInterface, string nameSpace = null)
        {
            return assembly
                .GetTypes()
                .Where(x =>
                    !IntrospectionExtensions.GetTypeInfo(x).IsAbstract &&
                    IntrospectionExtensions.GetTypeInfo(x).GetInterfaces().Any(y => IntrospectionExtensions.GetTypeInfo(y).IsGenericType && y.GetGenericTypeDefinition() == mappingInterface) && (nameSpace == null || IntrospectionExtensions.GetTypeInfo(x).Namespace == nameSpace));
        }

        public static void AddEntityConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly, string nameSpace = null)
        {
            var mappingTypes = assembly.GetMappingTypes(typeof(IEntityMappingConfiguration<>), nameSpace);

            foreach (var config in mappingTypes.Select(Activator.CreateInstance).Cast<IEntityMappingConfiguration>())
                config.Map(modelBuilder);
        }
    }
}
