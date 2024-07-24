using Microsoft.EntityFrameworkCore;
using ProjectBank.Entities;

namespace ProjectBank.Data
{
    public interface IDataContext
    {
        DbSet<Account> Account { get; set; }
        DbSet<Card> Card { get; set; }
        DbSet<Customer> Customer { get; set; }
        DbSet<Employee> Employee { get; set; }
        DbSet<Transaction> Transaction { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
