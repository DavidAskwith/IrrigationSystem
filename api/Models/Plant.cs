using System.ComponentModel.DataAnnotations;

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

    }
}

