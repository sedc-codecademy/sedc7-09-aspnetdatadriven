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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusinessObject>(businessObject =>
            {
                businessObject.HasOne(b => b.ResponsibleEmployee)
                               .WithOne(e => e.ResponsibleFor)
                               .HasForeignKey<BusinessObject>(b => b.ResponsibleEmployeeId);

                //either this one or in offer
                businessObject.HasMany(b => b.Offers)
                                .WithOne(o => o.BusinessObject)
                                .HasForeignKey(o => o.BusinessObjectId);

                businessObject.Property(b => b.Name)
                                .IsRequired()
                                .HasMaxLength(100);
            });

            modelBuilder.Entity<Offer>(offer =>
            {
                //either this one or in business object
                offer.HasOne(o => o.BusinessObject)
                               .WithMany(b => b.Offers)
                               .HasForeignKey(o => o.BusinessObjectId);

                offer.Property(o => o.Description)
                                .HasMaxLength(500);

                offer.Property(o => o.Title)
                        .IsRequired()
                        .HasMaxLength(100);
            });

            modelBuilder.Entity<Employee>(employee =>
            {
                employee.Property(e => e.FullName)
                        .IsRequired()
                        .HasMaxLength(100);
            });
        }
    }
}
