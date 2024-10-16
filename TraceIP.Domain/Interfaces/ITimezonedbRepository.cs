using TraceIP.Domain.Entities.Timezonedb;

namespace TraceIP.Domain.Interfaces
{
    public interface ITimezonedbRepository
    {
        Task<Response> FindAsync(string countryCode);
    }
}
