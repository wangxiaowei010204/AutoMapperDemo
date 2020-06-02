using AutoMapperHelper.AutoMapper;
using System;

namespace Models
{
    [AutoInject(typeof(Foo), typeof(FooDto))]
    public class Foo
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public Guid Id { get; set; }

        public decimal Money { get; set; }
    }
}
