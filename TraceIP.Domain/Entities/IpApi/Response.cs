namespace TraceIP.Domain.Entities.IpApi
{
    public class Response
    {
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public List<string> Languages { get; set; }

    }
}
