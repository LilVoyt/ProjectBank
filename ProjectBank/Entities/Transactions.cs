using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace ProjectBank.Entities
{
    public class Transactions
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Sum { get; set; }
        public Guid CardID { get; set; }
        public Account Account { get; set; }
        public Card Card { get; set; }
    }
}
