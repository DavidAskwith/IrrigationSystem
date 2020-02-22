using System;

namespace IrigationSystem.Entities 
{
    public class TempatureStat
    {
        public int TempatureStatId { get; set; }

        public DateTime LastRead { get; set; }

        public double  Tempature  { get; set; }

        public int PlantId { get; set; }

        public Plant Plant { get; set; }
        
    }
}
