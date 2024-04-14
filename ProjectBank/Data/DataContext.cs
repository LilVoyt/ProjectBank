using Microsoft.EntityFrameworkCore;
using ProjectBank.Entities;

namespace ProjectBank.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions <DataContext> options) : base(options)
        {
                
        }
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
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
