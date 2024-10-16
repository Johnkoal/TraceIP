namespace TraceIP.Domain.Entities.Timezonedb
{
    public class Zone
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string ZoneName { get; set; }
        public int GmtOffset { get; set; }
        public int Timestamp { get; set; }
    }
}
