using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoMapperHelper.AutoMapper
{
    public class AutoInjectFactory
    {
        public List<(Type, Type)> ConvertList { get; } = new List<(Type, Type)>();

        public void AddAssemblys(params Assembly[] assemblys)
        {
            foreach (var assembly in assemblys)
            {
                var types = assembly.GetTypes().Where(_type => _type.GetCustomAttribute<MapToAttribute>() != null);

                foreach (var type in types)
                {
                    var targetType = type.GetCustomAttribute<MapToAttribute>().TargetType;

                    ConvertList.Add((type, targetType));
                }
            }
        }
    }
}
