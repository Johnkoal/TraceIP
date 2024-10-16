using System.Text.Json.Serialization;

namespace TraceIP.Api.Entities
{
    public class TraceIpResponse
    {
        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        [JsonPropertyName("current_date")]
        public string CurrentDate { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("isocode")]
        public string IsoCode { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("country_date")]
        public string CountryDate { get; set; }

        [JsonPropertyName("distance")]
        public string Distance { get; set; }
    }
}
