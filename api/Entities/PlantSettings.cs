namespace IrigationSystem.Entities {

    public class PlantSettings
    {
        public int PlantSettingsId { get; set; }

        public float HumidityThresholdLow { get; set; }

        public float HumidityThresholdHigh { get; set; }

        public int PlantId { get; set; }

        public Plant Plant { get; set; }
    }
}

