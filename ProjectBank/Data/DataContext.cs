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
            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.Id); 

                entity.Property(e => e.AccID)
                      .IsRequired();

                entity.HasOne(e => e.Account)
                      .WithMany(a => a.Transactions)
                      .HasForeignKey(e => e.AccID)
                      .HasPrincipalKey(x => x.Id)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.AccID)
                  .IsRequired();

                entity.HasOne(b => b.Account)
                  .WithMany(a => a.Cards)
                  .HasForeignKey(b => b.AccID);
            });
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.CustomerID)
                  .IsRequired();

                entity.HasOne(b => b.Customer)
                .WithOne(a => a.Account)
                .HasForeignKey<Account>(b => b.CustomerID);

            });
        }
    }
}
