﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IrigationSystem.Models;
using IrigationSystem.Services;

namespace IrigationSystem.Controllers
{
    [ApiController]
    [Route("plants")]
    public class PlantController : ControllerBase
    {
        // TODO: add cref for returns

        private readonly ILogger<PlantController> logger;
        private readonly IPlantService plantService;

        public PlantController(
                ILogger<PlantController> logger,
                IPlantService plantService)
        {
            this.logger = logger;
            this.plantService = plantService;
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
        /// Gets all plants can be filtered by page page size defaults to 10.
        /// </summary>
        /// <param name="page">Starting ID of a range of plants.</param>
        /// <param name="pageSize">Ending ID of a range of plants.</param>
        /// <returns>Collection of plants.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Plant>>> GetAll([FromQuery]int? page, [FromQuery]int? pageSize)
        {
            if (page == null && pageSize != null)
                return BadRequest("page number must be set specified when specifying pageSize");

            using (var ctx = new IrigationSystemContext())
            {

                if (page == null && pageSize == null)
                {
                    return await ctx.Plants.ToListAsync();
                } 

            }

            var plants = await plantService.GetPagedAsync((int)page, pageSize ?? 10);
            return Ok(plants);
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
