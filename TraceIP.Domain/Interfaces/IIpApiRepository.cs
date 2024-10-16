using TraceIP.Domain.Entities.IpApi;

namespace TraceIP.Domain.Interfaces
{
    public interface IIpApiRepository
    {
        Task<Response> FindAsync(string ip);
    }
}
