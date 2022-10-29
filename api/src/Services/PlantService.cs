using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Irrigation.Entities;
using Irrigation.Helpers;

namespace Irrigation.Services
{
    public class PlantService : IPlantService
    {
        private readonly DataContext _context;
        private readonly int _userId;


        public PlantService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }


        /// <inheritdoc/>
        public async Task<Plant> GetByIdAsync(int id)
        {
            // TODO: Exception for user mismatch
            return await _context.Plants.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Plant>> GetAllAsync()
        {
            return await _context.Plants.Where(p => p.UserId == _userId).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Plant plant)
        {
            // TODO: Exception for user mismatch
            var plantDoesNotExist = await this.GetByIdAsync(plant.PlantId) == null;

            if(plantDoesNotExist)
                throw new AppException("Plant not found.");

            _context.Plants.Update(plant);
            await _context.SaveChangesAsync();

        }

        /// <inheritdoc/>
        public async Task CreateAsync(Plant plant)
        {
            //TODO: Add for curent user

            plant.UserId = _userId;
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id)
        {
            // TODO: Exception for user mismatch
            var plant = await this.GetByIdAsync(id);
            var plantDoesNotExist = plant == null;

            if(plantDoesNotExist)
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

            return await _context.Plants.Where(p => p.UserId == _userId)
                .Skip(start).Take(end).ToListAsync();
        }

    }
}
