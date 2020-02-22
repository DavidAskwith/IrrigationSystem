using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IrigationSystem.Entities
{
    public class IrigationSystemContext: DbContext
    {
        public DbSet<Plant> Plants { get; set; }

        public DbSet<PlantSensorMapping> PlantSensorMappings { get; set; }

        public DbSet<PlantSettings> PlantSettings { get; set; }

        public DbSet<Settings> Settings { get; set; }

        public DbSet<HumidityStat> HumidityStats { get; set; }

        public DbSet<SunStat> SunStats { get; set; }

        public DbSet<TempatureStat> TempatureStats { get; set; }

        public DbSet<WaterLog> WarterLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=irigation-system.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder){

            #if DEBUG
                modelBuilder.Entity<Plant>().HasData(
                        new Plant() {
                            PlantId = 1,
                            Name = "Gary",
                            Species = "Juniper",
                            SubSpecies = "Really nice!"
                        });
            #endif
        }
    }
}
