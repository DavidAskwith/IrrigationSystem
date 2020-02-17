namespace IrigationSystem.Models
{
    public class PlantSensorMapping
    {
        public int PlantSensorMappingId { get; set; }

        public int SensorId { get; set; }

        public int  PlantId { get; set; } 

        public Plant Plant { get; set; } 
    }
}
