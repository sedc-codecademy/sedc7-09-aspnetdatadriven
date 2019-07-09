using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SEDC.Entity.Class3.Domain
{
    public partial class BooksDB2019Context : DbContext
    {
        private string _connectionString;
        public BooksDB2019Context(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BooksDB2019Context(DbContextOptions<BooksDB2019Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Awards> Awards { get; set; }
        public virtual DbSet<Nominations> Nominations { get; set; }
        public virtual DbSet<Novels> Novels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Authors>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_Authors")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DateOfDeath).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Awards>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AwardName).IsRequired();
            });

            modelBuilder.Entity<Nominations>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AwardId).HasColumnName("AwardID");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.HasOne(d => d.Award)
                    .WithMany(p => p.Nominations)
                    .HasForeignKey(d => d.AwardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Nominations_Awards");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Nominations)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Nominations_Novels");
            });

            modelBuilder.Entity<Novels>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuthorId).HasColumnName("AuthorID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Novels)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HugoNovels_Authors");
            });
        }
    }
}
