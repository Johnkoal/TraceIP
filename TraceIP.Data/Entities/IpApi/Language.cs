using System.Text.Json.Serialization;

namespace TraceIP.Data.Entities.IpApi
{
    public class Language
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("native")]
        public string Native { get; set; }
    }
}
