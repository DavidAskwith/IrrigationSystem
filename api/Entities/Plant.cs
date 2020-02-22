using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace IrigationSystem.Entities
{
    public class Plant
    {
        public int PlantId  { get; set; }

        public byte[] Image { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Species { get; set; }

        [MaxLength(100)]
        public string SubSpecies { get; set; }

        public PlantSettings PlantSettings { get; set; }

        public PlantSensorMapping PlantSensorMapping { get; set; }

        public ICollection<WaterLog> WaterLogs { get; set; }

        public ICollection<SunStat> SunStats { get; set; }

        public ICollection<TempatureStat> TempatureStats { get; set; }

        public ICollection<HumidityStat> HumidityStats { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

    }
}

