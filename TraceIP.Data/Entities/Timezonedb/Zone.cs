using System.Text.Json.Serialization;

namespace TraceIP.Data.Entities.Timezonedb
{
    public class Zone
    {
        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }

        [JsonPropertyName("countryName")]
        public string CountryName { get; set; }

        [JsonPropertyName("zoneName")]
        public string ZoneName { get; set; }

        [JsonPropertyName("gmtOffset")]
        public int GmtOffset { get; set; }

        [JsonPropertyName("timestamp")]
        public int Timestamp { get; set; }
    }
}
