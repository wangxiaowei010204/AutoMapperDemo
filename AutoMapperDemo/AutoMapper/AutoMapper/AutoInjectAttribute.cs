using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapperHelper.AutoMapper
{
    public sealed class AutoInjectAttribute : Attribute
    {
        public Type SourceType { get; }
        public Type TargetType { get; }

        public AutoInjectAttribute(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
        }
    }
}
