using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DtoModels
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(c => c.Items)
                .WithOne(e => e.User)
                .HasForeignKey(x => x.UserId);


            #region Seed

            var users = new List<User>
            {
                new User("Risto", "Panchevski", "risto@gmail.com", "risto") {Id = 1},
                new User("Martin", "Panovski", "martin@gmail.com", "martin") {Id = 2}
            };

            modelBuilder.Entity<User>().HasData(users);
            #endregion
        }
    }
}
