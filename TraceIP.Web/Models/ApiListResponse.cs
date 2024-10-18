using System.Text.Json.Serialization;

namespace TraceIP.Web.Models
{
    public class ApiListResponse
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("distanceKms")]
        public int DistanceKms { get; set; }

        [JsonPropertyName("ip")]
        public string Ip { get; set; }
    }
}
