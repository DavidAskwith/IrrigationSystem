using System;

namespace Irrigation.Entities 
{
    public class HumidityStat
    {
        public int HumidityStatId { get; set; }

        public DateTime  LastRead { get; set; }

        public double  Humidity  { get; set; }

        public int PlantId { get; set; }

        public Plant Plant { get; set; }
        
    }
}
