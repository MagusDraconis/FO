using FO.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FO.DataAccess
{
    public class FoDbContext : DbContext
    {
        public FoDbContext() : base()
        {

        }
        public FoDbContext(DbContextOptions<FoDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=FoDb");
        }
        public DbSet<Friend> Friends => Set<Friend>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Friend>().ToTable("Friend");
            modelBuilder.Entity<Friend>().HasKey(e => e.Id);
            modelBuilder.Entity<Friend>().Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Friend>().Property(e => e.LastName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Friend>().Property(e => e.Email).HasAnnotation("Email",null).HasMaxLength(150);


            // only First time
            SetInitializedData(modelBuilder);
        }

        private static void SetInitializedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>().HasData(new { Id = 1, FirstName = "Thomas", LastName = "Huber" });
            modelBuilder.Entity<Friend>().HasData(new { Id = 2, FirstName = "Fabrice", LastName = "Wieser" });
            modelBuilder.Entity<Friend>().HasData(new { Id = 3, FirstName = "Julia", LastName = "Huber" });
            modelBuilder.Entity<Friend>().HasData(new { Id = 4, FirstName = "Chrissi", LastName = "Egin" });



        }
    }
}
