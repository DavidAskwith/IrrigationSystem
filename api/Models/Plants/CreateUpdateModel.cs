using System.ComponentModel.DataAnnotations;

namespace IrigationSystem.Models.Plants
{
    public class CreateUpdateModel
    {
        public byte[] Image { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Species { get; set; }

        [MaxLength(100)]
        public string SubSpecies { get; set; }
    }
}

