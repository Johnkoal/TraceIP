using System.Text.Json.Serialization;

namespace TraceIP.Data.Entities.IpApi
{
    public class Location
    {
        [JsonPropertyName("capital")]
        public string Capital { get; set; }

        [JsonPropertyName("languages")]
        public List<Language> Languages { get; set; }

        [JsonPropertyName("calling_code")]
        public string Calling_code { get; set; }
    }
}
