 using System;

namespace Irrigation.Entities
{
    public class Settings
    {
        public int SettingsId { get; set; }

        public DateTime WaterTime { get; set; }

        public int UserId { get; set; }
    }
}
