using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IrigationSystem.Models;

namespace IrigationSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantsController : ControllerBase
    {
        // TODO: add cref for returns

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
                return await ctx.Plants.FirstOrDefaultAsync(p => p.PlantId == id);
            }
        }

        /// <summary>
        /// Gets all plants in a range.
        /// </summary>
        /// <param name="start">Starting ID of a range of plants.</param>
        /// <param name="end">Ending ID of a range of plants.</param>
        /// <returns>Collection of plants.</returns>
        [HttpGet("{start:int}/{end:int}")]
        public async Task<IEnumerable<Plant>> GetRange(int start, int end)
        {
            using (var ctx = new IrigationSystemContext()){
                return await ctx.Plants.Skip(start - 1).Take(end).ToListAsync();
            }
        }

        /// <summary>
        /// Updates a plant that already exists.
        /// </summary>
        /// <param name="plant">Plant that will update a pre-existing plant.</param>
        /// <returns>Plant that was updated.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Plant>> Update([FromBody]Plant plant)
        {
            using (var ctx = new IrigationSystemContext()){

                if(ctx.Plants.All(p => p.PlantId != plant.PlantId))
                {
                    return NotFound("Plant with the specified ID does not exist.");
                }

                ctx.Plants.Update(plant);


                await ctx.SaveChangesAsync();

                return new OkObjectResult(plant);
            }
        }

        /// <summary>
        /// Adds a plant.
        /// </summary>
        /// <param name="plant">Plant that will added.</param>
        /// <returns>Plant that was added.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Plant>> Add([FromBody]Plant plant)
        {
            using (var ctx = new IrigationSystemContext()){

                // TODO: Test it
                if(ctx.Plants.Any(p => p.PlantId == plant.PlantId))
                {
                    return BadRequest("Plant must be specified without an ID when adding.");
                }

                ctx.Plants.Add(plant);

                await ctx.SaveChangesAsync();

                return Created("TODO: Set to URL dynamically", plant);
            }
        }

        /// <summary>
        /// Adds a plant.
        /// </summary>
        /// <param name="plant">Plant that will added.</param>
        /// <returns>Plant that was added.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            using (var ctx = new IrigationSystemContext()){

                if(ctx.Plants.All(p => p.PlantId != id))
                {
                    return NotFound("Plant with the specified ID does not exist.");
                }

                ctx.Plants.Remove(ctx.Plants.Find(id));

                await ctx.SaveChangesAsync();

                return NoContent();
            }
        }

    }
}
