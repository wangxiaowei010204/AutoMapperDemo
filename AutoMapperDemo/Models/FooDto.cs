
using AutoMapperHelper.AutoMapper;
using System;

namespace Mapper.Models
{
    public class FooDto
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public Guid Id { get; set; }

        public decimal Money { get; set; }

    }
}
