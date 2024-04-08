using Microsoft.EntityFrameworkCore;
using ProjectBank.Entities;

namespace ProjectBank.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions <DataContext> options) : base(options)
        {
                
        }
        public DbSet <Customer> Customers {  get; set; }
    }
}
