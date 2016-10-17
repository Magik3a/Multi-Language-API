namespace Multi_Language.DataApi
{
    using AutoMapper;
    using Multi_language.Common.Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class AutoMapperConfig
    {
        public static void RegisterMappings(params Assembly[] assemblies)
        {
            var types = new List<Type>();
            foreach (var assembly in assemblies)
            {
                types.AddRange(assembly.GetExportedTypes());
            }
            Mapper.Initialize(s =>
          {
              LoadStandardMappings(s, types);
              LoadCustomMappings(s, types);
          });
        }


        private static void LoadStandardMappings(IMapperConfigurationExpression conf, IEnumerable<Type> types)
        {
            var maps = types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
                .Where(
                    type =>
                        type.i.IsGenericType && type.i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                        !type.t.IsAbstract
                        && !type.t.IsInterface)
                .Select(type => new { Source = type.i.GetGenericArguments()[0], Destination = type.t });

            foreach (var map in maps)
            {
                conf.CreateMap(map.Source, map.Destination);
                conf.CreateMap(map.Source, map.Destination).ReverseMap();
                conf.CreateMissingTypeMaps = true;
            }

        }

        private static void LoadCustomMappings(IMapperConfigurationExpression conf, IEnumerable<Type> types)
        {
            var maps =
                types.SelectMany(t => t.GetInterfaces(), (t, i) => new { t, i })
                    .Where(
                        type =>
                            typeof(ICustomMapping).IsAssignableFrom(type.t) && !type.t.IsAbstract &&
                            !type.t.IsInterface)
                    .Select(type => (ICustomMapping)Activator.CreateInstance(type.t));

            foreach (var map in maps)
            {
                map.CreateMappings(conf);
            }
        }
    }
}