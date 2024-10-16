namespace TraceIP.Domain.Entities.Fixer
{
    public class Response
    {
        public string BaseCurrency { get; set; }
        public Dictionary<string, double> Rates { get; set; }
    }
}
