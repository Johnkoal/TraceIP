using System.Text.Json.Serialization;

namespace TraceIP.Data.Entities.GeoPlugin
{
    public class ApiResponse
    {
        [JsonPropertyName("geoplugin_countryCode")]
        public string Geoplugin_countryCode { get; set; }

        [JsonPropertyName("geoplugin_timezone")]
        public string Geoplugin_timezone { get; set; }

        [JsonPropertyName("geoplugin_currencyCode")]
        public string Geoplugin_currencyCode { get; set; }

        [JsonPropertyName("geoplugin_currencySymbol")]
        public string Geoplugin_currencySymbol { get; set; }
    }
}
