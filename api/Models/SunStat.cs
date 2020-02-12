using System;

namespace IrigationSystem.Models 
{
    public class SunStat
    {
        public int SunStatId { get; set; }

        public DateTime  LastRead { get; set; }

        public double Irradiance  { get; set; }

        public Plant Plant { get; set; }
        
    }
}
