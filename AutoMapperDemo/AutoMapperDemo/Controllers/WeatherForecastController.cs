using AutoMapper;
using AutoMapperHelper.AutoMapper;
using Mapper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoMapperDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private IMapper Mapper { get; }
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMapper mapper)
        {
            _logger = logger;
            Mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // GET api/values
        [HttpGet]
        public FooDto GetFooDto()
        {
            var model = new Foo()
            {
                Id = Guid.NewGuid(),
                Name = "test",
                Money = 15.0m
            };

            var dto= model.MapTo<FooDto>();

            Console.WriteLine("Total memory: {0:###,###,###,##0} bytes", GC.GetTotalMemory(true));

            return dto;
        }
    }
}
