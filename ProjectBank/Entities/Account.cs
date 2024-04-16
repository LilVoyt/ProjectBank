using System.ComponentModel.DataAnnotations;

namespace ProjectBank.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public Guid CustomerID { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Card> Cards { get; set; }
        public ICollection<Transactions> Transactions { get; set; }
    }
}
