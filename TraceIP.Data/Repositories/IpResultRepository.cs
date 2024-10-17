using Microsoft.EntityFrameworkCore;
using TraceIP.Data.Context;
using TraceIP.Data.Entities.DbModels;
using TraceIP.Domain.Entities;
using TraceIP.Domain.Interfaces;
using TraceIP.Infraestructure.Exceptions;

namespace TraceIP.Data.Repositories
{
    public class IpResultRepository : IIpResultRepository
    {
        private readonly DataContext _context;

        public IpResultRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(DataContext));
            _context.Database.EnsureCreated();
        }

        public void Add(IpResult entity)
        {
            try
            {
                var ipResultModel = new IpResultModel()
                {
                    Id = Guid.NewGuid(),
                    Country = entity.Country,
                    DistanceKms = entity.DistanceKms,
                    Ip = entity.Ip
                };
                _context.IpResults.Add(ipResultModel);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ExceptionDatabase("Error al almacenar el registro en la base de datos", ex);
            }
        }

        public void DeleteAll()
        {
            try
            {
                //var ipResults = _context.IpResults.ToList();
                //_context.RemoveRange(ipResults);
                //_context.SaveChanges();
                _context.Database.ExecuteSqlRaw("DELETE FROM IpResults");
                _context.Database.ExecuteSqlRaw("VACUUM");
            }
            catch (Exception ex)
            {
                throw new ExceptionDatabase("Error al eliminar todos registros en la base de datos", ex);
            }
        }

        public IEnumerable<IpResult> GetAll()
        {
            try
            {
                var ipResults = new List<IpResult>();
                var ipResultModels = _context.IpResults.ToList();
                foreach (var ipResultModel in ipResultModels)
                {
                    ipResults.Add(new IpResult()
                    {
                        Country = ipResultModel.Country,
                        DistanceKms = ipResultModel.DistanceKms,
                        Ip = ipResultModel.Ip
                    });
                }

                return ipResults;
            }
            catch (Exception ex)
            {
                throw new ExceptionDatabase("Error al consultar en la base de datos", ex);
            }
        }
    }
}
