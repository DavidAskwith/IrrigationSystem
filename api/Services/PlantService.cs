using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IrigationSystem.Models;

namespace IrigationSystem.Services
{
    public class PlantService : IPlantService 
    {
        private readonly ILogger<PlantService> logger;

        public PlantService(ILogger<PlantService> logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Plant>> GetPagedAsync(int page, int pageSize)
        {
            //TODO: Refine method for paging
            int start = pageSize * page - pageSize;
            int end = pageSize * page;

            using (var ctx = new IrigationSystemContext())
            {
                return await ctx.Plants.Skip(start).Take(end)
                  .Select(p => new Plant() {
                      Name = p.Name,
                      Species = p.Species
                  }).ToListAsync();
            }
        }

    }
}
