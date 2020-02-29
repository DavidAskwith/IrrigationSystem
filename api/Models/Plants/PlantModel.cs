using System.ComponentModel.DataAnnotations;

namespace IrigationSystem.Models.Plants
{
    /// <summary>
    /// DTO for returning plants.
    /// </summary>
    public class PlantModel
    {
        public int PlantId  { get; set; }

        public byte[] Image { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Species { get; set; }

        [MaxLength(100)]
        public string SubSpecies { get; set; }
    }
}
