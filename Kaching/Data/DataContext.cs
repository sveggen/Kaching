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
                .HasOne(f => f.Creator)
                .WithMany(f => f.ExpensesCreated)
                .HasForeignKey(g => g.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EEvent>()
                .Property(s => s.Updated)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Transfer>()
                .HasOne(f => f.Sender)
                .WithMany(f => f.PaymentsSent)
                .HasForeignKey(g => g.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfer>()
                .HasOne(f => f.Receiver)
                .WithMany(f => f.PaymentsReceived)
                .HasForeignKey(g => g.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }


        //    modelBuilder.Entity<Expense>()
        //        .HasOne(f => f.Buyer)
        //        .WithMany(f => f.ExpensesPaid)
        //        .HasForeignKey(g => g.BuyerId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //}



        public DbSet<Expense> Expense { get; set; }

        public DbSet<EEvent> ExpenseEvent { get; set; }

        public DbSet<Person> Person { get; set; }

        public DbSet<Transfer> Payment { get; set; }
    }
}