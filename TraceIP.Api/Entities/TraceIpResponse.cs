using System.Text.Json.Serialization;

namespace TraceIP.Api.Entities
{
    public class TraceIpResponse
    {
        [JsonPropertyName("message_code")]
        public int MessageCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        [JsonPropertyName("current_date")]
        public string CurrentDate { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("iso_code")]
        public string IsoCode { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("country_date")]
        public string CountryTime { get; set; }

        [JsonPropertyName("distance")]
        public string DistanceKms { get; set; }
    }
}
