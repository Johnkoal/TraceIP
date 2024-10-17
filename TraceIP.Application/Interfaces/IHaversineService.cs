namespace TraceIP.Application.Interfaces
{
    public interface IHaversineService
    {
        int CalculateDistance(double lat1, double lon1, double lat2, double lon2);
    }
}
