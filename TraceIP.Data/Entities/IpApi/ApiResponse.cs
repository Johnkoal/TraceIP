using System.Text.Json.Serialization;

namespace TraceIP.Data.Entities.IpApi
{
    public class ApiResponse
    {
        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        [JsonPropertyName("country_code")]
        public string Country_code { get; set; }

        [JsonPropertyName("country_name")]
        public string Country_name { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("latitude")]
        public Double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public Double Longitude { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }
    }
}
