using System;

namespace AutoMapperHelper.AutoMapper
{
    public sealed class MapToAttribute : Attribute
    {
        public Type TargetType { get; }

        public MapToAttribute(Type targetType)
        {
            TargetType = targetType;
        }
    }
}
