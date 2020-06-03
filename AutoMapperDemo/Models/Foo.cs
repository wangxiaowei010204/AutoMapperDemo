using AutoMapperHelper.AutoMapper;
using System;

namespace Mapper.Models
{
    [MapTo(typeof(FooDto))]
    public class Foo
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public Guid Id { get; set; }

        public decimal Money { get; set; }
    }
}
