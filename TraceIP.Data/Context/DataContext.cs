using Microsoft.EntityFrameworkCore;
using TraceIP.Data.Entities.DbModels;

namespace TraceIP.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<IpResultModel> IpResults { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Data Source=Database.db");
        //}
    }
}
