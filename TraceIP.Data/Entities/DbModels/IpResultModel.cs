namespace TraceIP.Data.Entities.DbModels
{
    public class IpResultModel
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public int DistanceKms { get; set; }
        public string Ip { get; set; }
    }
}
