using System;

namespace IrigationSystem.Models 
{
    public class WaterLog 
    {
        public int WaterLogId { get; set; }

        public DateTime WaterDate { get; set; }

        public int PlantId { get; set; }

        public Plant Plant { get; set; }
    }
}
