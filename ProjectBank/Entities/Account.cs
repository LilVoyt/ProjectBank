using System.ComponentModel.DataAnnotations;

namespace ProjectBank.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public Guid EmployeeID { get; set; }
        public Guid CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
