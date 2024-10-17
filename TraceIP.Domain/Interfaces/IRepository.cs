namespace TraceIP.Domain.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void DeleteAll();
        IEnumerable<T> GetAll();
    }
}
