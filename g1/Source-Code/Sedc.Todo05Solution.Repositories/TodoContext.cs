using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sedc.Todo03Solution.Entities;

namespace Sedc.Todo03Solution.Repositories
{
    public class TodoContext : IdentityDbContext<TodoUser>
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Todo>()
            //    .ToTable("Todos")
            //    .HasKey(x => x.Id);
            //builder.Entity<Todo>()
            //    .Property(x => x.IsCompleted)
            //    .HasColumnType("date");
            //builder.Entity<Todo>()
            //    .HasOne(x => x.User)
            //    .WithMany(u => u.Todos)
            //    .HasForeignKey(x => x.UserId);
            //both are valid
            //builder.Entity<TodoUser>()
            //    .HasMany(x => x.Todos)
            //    .WithOne(u => u.User)
            //    .HasForeignKey(x => x.UserId);
            base.OnModelCreating(builder);
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
