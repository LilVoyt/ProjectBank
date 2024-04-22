using System.ComponentModel.DataAnnotations;

namespace ProjectBank.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        //Foreign
        public Guid EmployeeID { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Card> Cards { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
