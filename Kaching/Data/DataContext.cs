using Kaching.Models;
using Microsoft.EntityFrameworkCore;

namespace Kaching.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                 .Property(s => s.Created)
                 .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Expense>()
                .Property(s => s.Updated)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");
        }

        public DbSet<Expense> Expense { get; set; }

        public DbSet<Person> Person { get; set; }

        public DbSet<Category> Category { get; set; }

    }
}