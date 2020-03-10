using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Irrigation.Entities
{
    public class DataContext : DbContext
    {
        public DataContext()
        { }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        public DbSet<Plant> Plants { get; set; }

        public DbSet<PlantSensorMapping> PlantSensorMappings { get; set; }

        public DbSet<PlantSettings> PlantSettings { get; set; }

        public DbSet<Settings> Settings { get; set; }

        public DbSet<HumidityStat> HumidityStats { get; set; }

        public DbSet<SunStat> SunStats { get; set; }

        public DbSet<TempatureStat> TempatureStats { get; set; }

        public DbSet<WaterLog> WarterLogs { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
