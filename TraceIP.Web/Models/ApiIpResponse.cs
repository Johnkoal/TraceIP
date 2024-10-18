using System.Text.Json.Serialization;

namespace TraceIP.Web.Models
{
    public class ApiIpResponse
    {
        [JsonPropertyName("message_code")]
        public int Message_code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        [JsonPropertyName("current_date")]
        public string Current_date { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("iso_code")]
        public string Iso_code { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("currency_code")]
        public string Currency_code { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("country_date")]
        public string Country_date { get; set; }

        [JsonPropertyName("distance")]
        public string Distance { get; set; }
    }
}
