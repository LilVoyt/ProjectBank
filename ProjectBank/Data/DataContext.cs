using Microsoft.EntityFrameworkCore;
using ProjectBank.Entities;

namespace ProjectBank.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions <DataContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet <Employee> Employees { get; set; }
        public DbSet <Transactions> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.CustomerID)
                  .IsRequired();

                entity.Property(b => b.EmployeeID)
                .IsRequired();
                    
                entity.HasOne(b => b.Customer)
                .WithOne(a => a.Account)
                .HasForeignKey<Account>(b => b.CustomerID);

                entity.HasMany(b => b.Employees)
                .WithMany(a => a.Accounts);
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.AccountID)
                .IsRequired();

                entity.HasOne(b => b.Account)
                .WithMany(a => a.Cards)
                .HasForeignKey(b => b.AccountID);
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.CardID)
                .IsRequired();

                entity.HasOne(b => b.Card)
                .WithMany(a => a.Transactions)
                .HasForeignKey(b => b.CardID)
                .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
