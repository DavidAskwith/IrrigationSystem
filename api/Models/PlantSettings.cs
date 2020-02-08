namespace IrigationSystem.Models {

    public class PlantSettings
    {
        public int Id { get; set; }

        public Plant Plant { get; set; }

        public float HumidityThresholdLow { get; set; }

        public float HumidityThresholdHigh { get; set; }
    }
}

