using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace AutoMapperHelper.AutoMapper
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection service)
        {
            service.TryAddSingleton<MapperConfigurationExpression>();
            service.TryAddSingleton<AutoInjectFactory>();
            service.TryAddSingleton(serviceProvider =>
            {
                var mapperConfigurationExpression = serviceProvider.GetRequiredService<MapperConfigurationExpression>();
                var factory = serviceProvider.GetRequiredService<AutoInjectFactory>();

                foreach (var (sourceType, targetType) in factory.ConvertList)
                {
                    mapperConfigurationExpression.CreateMap(sourceType, targetType);
                }

                var instance = new MapperConfiguration(mapperConfigurationExpression);

                instance.AssertConfigurationIsValid();

                return instance;
            });

            service.TryAddSingleton(serviceProvider =>
            {
                var mapperConfiguration = serviceProvider.GetRequiredService<MapperConfiguration>();

                return mapperConfiguration.CreateMapper();
            });

            return service;
        }

        public static IMapperConfigurationExpression UseAutoMapper(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.ApplicationServices.GetRequiredService<MapperConfigurationExpression>();
        }

        public static void UseAutoInject(this IApplicationBuilder applicationBuilder, params Assembly[] assemblys)
        {
            var factory = applicationBuilder.ApplicationServices.GetRequiredService<AutoInjectFactory>();

            factory.AddAssemblys(AppDomain.CurrentDomain.GetAssemblies());

            var deps = DependencyContext.Default;
            var libs = deps.CompileLibraries;
         
            IEnumerable<Assembly> allAssemblies = Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load);

            foreach (var lib in libs)
            {
                try
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));

                    factory.AddAssemblys(assembly);
                }
                catch (Exception ex)
                { 
                }
            }

        }
    }
}
