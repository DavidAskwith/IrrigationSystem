using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Get all plants.
        /// </summary>
        /// <returns>A collection of plants.</returns>
        [HttpGet]
        public async Task<IEnumerable<Plant>> GetAll()
        {
            using (var ctx = new IrigationSystemContext()){
                return await ctx.Plants.ToListAsync();
            }
        }

        /// <summary>
        /// Get a plant by ID.
        /// </summary>
        /// <param name="id">ID of a specific plant.</param>
        /// <returns>A single plant.</returns>
        [HttpGet("{id:int}")]
        public async Task<Plant> GetById(int id)
        {
            using (var ctx = new IrigationSystemContext()){
                return await ctx.Plants.Where(p => p.PlantId == id).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Get all plants in a range.
        /// </summary>
        /// <param name="start">Startiung ID of a range of plants.</param>
        /// <param name="end">Ending ID of a range of plants.</param>
        /// <returns>A collection of plants.</returns>
        [HttpGet("{start:int}/{end:int}")]
        public async Task<IEnumerable<Plant>> GetRange(int start, int end)
        {
            using (var ctx = new IrigationSystemContext()){
                return await ctx.Plants.Skip(start).Take(end).ToListAsync();
            }
        }
    }
}
