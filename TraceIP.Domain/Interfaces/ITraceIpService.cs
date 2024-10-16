using TraceIP.Domain.Entities;

namespace TraceIP.Domain.Interfaces
{
    public interface ITraceIpService
    {
        Task<IpResponse> GetIp(string ip);
    }
}
