using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IrigationSystem.Models;

namespace IrigationSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantsController : ControllerBase
    {
        private readonly ILogger<PlantsController> _logger;

        public PlantsController(ILogger<PlantsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Plant> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Plant
            {
                PlantId = index,
                Name = "Test Name",
                Species = "Fur",
                SubSpecies = "NIce As Fuck"

            })
            .ToArray();
        }
    }
}
