using TraceIP.Domain.Entities.Fixer;

namespace TraceIP.Domain.Interfaces
{
    public interface IFixerRepository
    {
        Task<Response> FindAsync(string currencyCode);
    }
}
