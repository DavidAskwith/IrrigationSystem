using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Irrigation.Entities;
using Irrigation.Helpers;

namespace Irrigation.Services
{
    public class PlantService : IPlantService 
    {
        private readonly DataContext _context;


        public PlantService(DataContext context)
        {
            _context = context;
        }


        /// <inheritdoc/>
        public async Task<Plant> GetByIdAsync(int id)
        {
            return await _context.Plants.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Plant>> GetAllAsync()
        {
            return await _context.Plants.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Plant plant)
        {
            var plantExists = await this.GetByIdAsync(plant.PlantId) == null;

            if (!plantExists)
                throw new AppException("Plant not found.");

            _context.Plants.Update(plant);
            await _context.SaveChangesAsync();

        }

        /// <inheritdoc/>
        public async Task CreateAsync(Plant plant)
        {
            //TODO: Add for curent user

            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            var plant = await this.GetByIdAsync(id);

            if(plant == null)
                throw new AppException("Plant not found.");

            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<Plant>> GetPagedAsync(int page, int pageSize)
        {
            //TODO: Refine method for paging
            int start = pageSize * page - pageSize;
            int end = pageSize * page;

            return await _context.Plants.Skip(start).Take(end)
              .Select(p => new Plant() {
                  Name = p.Name,
                  Species = p.Species
              }).ToListAsync();
        }

    }
}
