using Airport.Entities;
using Microsoft.EntityFrameworkCore;

namespace Airport.Repositories.Implementations
{
    public class AirportDbContext : DbContext
    {
        public DbSet<BusinessObject> BusinessObjects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Offer> Offers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = 
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AirportDb;Integrated Security=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
