using System.Collections.Generic;
using System.Threading.Tasks;
using IrigationSystem.Entities;

namespace IrigationSystem.Services
{
    /// <summary>
    /// Defines the attributes a plant service must impliment.
    /// </summary>
    public interface IPlantService
    {
        /// <summary>
        /// Get plants paginated.
        /// </summary>
        /// <param name="page">What page to retrieve.</param>
        /// <param name="pageSize">The size of the pages being retrieved.</param>
        /// <returns>Collection of plants.</returns>
        Task<IEnumerable<Plant>> GetPagedAsync(int page, int pageSize);
    }
}
