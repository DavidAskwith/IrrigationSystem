using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IrigationSystem.Entities
{
    public class DataContext: DbContext
    {
        public DbSet<Plant> Plants { get; set; }

        public DbSet<PlantSensorMapping> PlantSensorMappings { get; set; }

        public DbSet<PlantSettings> PlantSettings { get; set; }

        public DbSet<Settings> Settings { get; set; }

        public DbSet<HumidityStat> HumidityStats { get; set; }

        public DbSet<SunStat> SunStats { get; set; }

        public DbSet<TempatureStat> TempatureStats { get; set; }

        public DbSet<WaterLog> WarterLogs { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=irigation-system.db");
    }
}
