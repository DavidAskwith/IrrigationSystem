using System.Collections.Generic;
using System.Threading.Tasks;
using Irrigation.Entities;

namespace Irrigation.Services
{
    /// <summary>
    /// Defines the attributes a plant service must impliment.
    /// </summary>
    public interface IPlantService
    {
        /// <summary>
        /// Create a plant.
        /// </summary>
        /// <param name="plant">Plant that will created.</param>
        /// <returns>Plant that was created.</returns>
        Task CreateAsync(Plant plant);

        /// <summary>
        /// Get plants paginated.
        /// </summary>
        /// <param name="page">What page to retrieve.</param>
        /// <param name="pageSize">The size of the pages being retrieved.</param>
        /// <returns>Collection of plants.</returns>
        Task<IEnumerable<Plant>> GetPagedAsync(int page, int pageSize);

        /// <summary>
        /// Get a plant by ID.
        /// </summary>
        /// <param name="id">ID of a specific plant.</param>
        /// <returns>A single plant.</returns>
        Task<Plant> GetByIdAsync(int id);

        /// <summary>
        /// Gets all plants.
        /// </summary>
        /// <returns>Collection of plants.</returns>
        Task<IEnumerable<Plant>> GetAllAsync();

        /// <summary>
        /// Updates a plant that already exists.
        /// </summary>
        /// <param name="plant">Plant that will update a pre-existing plant.</param>
        /// <returns>Plant that was updated.</returns>
        Task UpdateAsync(Plant plant);

        /// <summary>
        /// Create a deleted.
        /// </summary>
        /// <param name="plant">Plant that will deleted.</param>
        /// <returns>Plant that was deleted.</returns>
        Task DeleteAsync(int id);
    }
}
