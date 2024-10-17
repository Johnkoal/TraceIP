using TraceIP.Domain.Entities;

namespace TraceIP.Domain.Interfaces
{
    public interface ITraceIpService
    {
        Task<IpResponse> GetIp(string ip);

        void Delete();

        IEnumerable<IpResult> GetAll();
    }
}
