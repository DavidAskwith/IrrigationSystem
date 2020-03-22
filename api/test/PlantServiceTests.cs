using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Irrigation.Services;
using Irrigation.Entities;
using Irrigation.Helpers;

namespace Test
{
    public class PlantServiceTests
    {
        private void SeedPlants(DataContext context)
        {
            var plants = new List<Plant>() {
                new Plant(){
                    PlantId = 1,
                    Image = new byte[] { 0x20, 0x20 },
                    Name = "Vi",
                    Species = "Violet",
                    SubSpecies = "Flower"
                },
                new Plant(){
                    PlantId = 2,
                    Image = new byte[] { 0x20, 0x20 },
                    Name = "Gary",
                    Species = "Juniper",
                    SubSpecies = "Tree"
                },
            };

            context.Plants.AddRange(plants);
            context.SaveChanges();
        }

        [Fact]
        public async void GetByIdAsync_InvalidId_Plant()
        {
            using (var ctx = new TestDataContext())
            {
                SeedPlants(ctx);
                var service = new PlantService(ctx);

                var plant = await service.GetByIdAsync(0);

                Assert.IsType<Plant>(plant);
                Assert.Equal(1, plant.PlantId);
            }
        }

        [Fact]
        public async void GetByIdAsync_ValidId_Plant()
        {
            using (var ctx = new TestDataContext())
            {
                SeedPlants(ctx);
                var service = new PlantService(ctx);

                var plant = await service.GetByIdAsync(0);

                Assert.Null(plant);

            }
        }
    }
}
