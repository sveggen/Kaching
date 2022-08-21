using Kaching.Enums;
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
            modelBuilder.Entity<BaseExpense>()
                .Property(s => s.Created)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<BaseExpense>()
                .HasOne(f => f.Creator)
                .WithMany(f => f.Expenses)
                .HasForeignKey(g => g.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Expense>()
                .Property(s => s.Updated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Transfer>()
                .HasOne(f => f.Sender)
                .WithMany(f => f.TransfersSent)
                .HasForeignKey(g => g.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfer>()
                .HasOne(f => f.Receiver)
                .WithMany(f => f.TransfersReceived)
                .HasForeignKey(g => g.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<BaseExpense> BaseExpense { get; set; }

        public DbSet<Expense> Expense { get; set; }

        public DbSet<Person> Person { get; set; }

        public DbSet<Transfer> Transfer { get; set; }
        
        public DbSet<Category> Category { get; set; }
        
        public DbSet<Group> Group { get; set; }
        
        public DbSet<Currency> Currency { get; set; }
    }
}