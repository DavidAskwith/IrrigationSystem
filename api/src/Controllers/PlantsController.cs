using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Irrigation.Entities;
using Irrigation.Services;
using Irrigation.Helpers;
using Irrigation.Models.Plants;

namespace Irrigation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PlantsController : ControllerBase
    {
        // TODO: add cref for returns

        private readonly ILogger<PlantsController> _logger;
        private readonly IPlantService _plantService;
        private readonly IMapper _mapper;

        public PlantsController(
                ILogger<PlantsController> logger,
                IMapper mapper,
                IPlantService plantService)
        {
            _logger = logger;
            _mapper = mapper;
            _plantService = plantService;
        }

        /// <summary>
        /// Creates a plant.
        /// </summary>
        /// <param name="plant">Plant that will created.</param>
        /// <returns>Plant that was created.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody]CreateUpdateModel model)
        {
            var plant = _mapper.Map<Plant>(model);

            await _plantService.CreateAsync(plant);

            return Ok(plant);
        }


        /// <summary>
        /// Get a plant by ID.
        /// </summary>
        /// <param name="id">ID of a specific plant.</param>
        /// <returns>A single plant.</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Plant>> GetById(int id)
        {
            var plant = await _plantService.GetByIdAsync(id);
            var model = _mapper.Map<PlantModel>(plant);
            return Ok(model);
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
            // TODO: Page size limit
            if (page == null && pageSize != null)
                return BadRequest("page number must be set specified when specifying pageSize");

            IEnumerable<Plant> plants;

            if (page == null && pageSize == null)
            {
                plants = await _plantService.GetAllAsync();
            }
            else
            {
                plants = await _plantService.GetPagedAsync((int)page, pageSize ?? 10);
            }
            var model = _mapper.Map<IList<PlantModel>>(plants);
            return Ok(model);
        }


        /// <summary>
        /// Updates a plant that already exists.
        /// </summary>
        /// <param name="plant">Plant that will update a pre-existing plant.</param>
        /// <returns>Plant that was updated.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Plant>> Update([FromBody]PlantModel model)
        {
            // TODO: breakout
            var plantDoesNotExist = await _plantService.GetByIdAsync(model.PlantId) == null;
            if (plantDoesNotExist)
                return NotFound("Plant with the specified ID does not exist.");

            var plant = _mapper.Map<Plant>(model);
            await _plantService.UpdateAsync(plant);

            return new OkObjectResult(plant);
        }

        /// <summary>
        /// Deletes a plant.
        /// </summary>
        /// <param name="plant">Plant that will deleted.</param>
        /// <returns>Plant that was deleted.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            // TODO: breakout
            var plantDoesNotExist = await _plantService.GetByIdAsync(id) == null;
            if (plantDoesNotExist)
                return NotFound("Plant with the specified ID does not exist.");

            await _plantService.DeleteAsync(id);

            return NoContent();
        }

    }
}
