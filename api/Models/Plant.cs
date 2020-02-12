using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace IrigationSystem.Models
{
    public class Plant
    {
        public int Id  { get; set; }

        public byte[] Image { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Species { get; set; }

        [MaxLength(100)]
        public string SubSpecies { get; set; }

        public ICollection<WaterLog> WaterLogs { get; set; }

        public PlantSettings PlantSettings { get; set; }

        public PlantSensorMapping PlantSensorMapping { get; set; }

    }
}

