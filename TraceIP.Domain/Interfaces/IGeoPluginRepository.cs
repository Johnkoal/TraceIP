using TraceIP.Domain.Entities.GeoPlugin;

namespace TraceIP.Domain.Interfaces
{
    public interface IGeoPluginRepository
    {
        Task<Response> FindAsync(string ip);
    }
}
