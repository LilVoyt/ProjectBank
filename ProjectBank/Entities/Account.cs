using System.ComponentModel.DataAnnotations;

namespace ProjectBank.Entities
{
    public class Account
    {
        [Key]
        public Guid id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double Balance { get; set; }
        public List<Card> Cards { get; set; }

        public List<Transactions> Transactions { get; set; }
    }
}
