namespace Irrigation.Helpers
{

    /// <summary>
    /// Used to access AppSettings via dependency injection
    /// </summary>
    public class AppSettings
    {
        public string JWTSigningSecret { get; set; }
        public string AllowedOrigins { get; set; }
    }
}
