using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace ProjectBank.Entities
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }
        public string NumberCard { get; set; }
        public string CardName { get; set; }
        public int Pincode { get; set; }
        public DateTime Data { get; set; }
        public int CVV { get; set; }
        public double Balance { get; set; }
        public Guid AccountID { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Transaction> SentTransactions { get; set; }
        public virtual ICollection<Transaction> ReceivedTransactions { get; set; }
    }
}
