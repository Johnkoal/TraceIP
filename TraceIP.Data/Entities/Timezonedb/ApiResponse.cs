using System.Text.Json.Serialization;

namespace TraceIP.Data.Entities.Timezonedb
{
    public class ApiResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("zones")]
        public List<Zone> Zones { get; set; }
    }
}
