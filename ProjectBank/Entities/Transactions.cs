using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace ProjectBank.Entities
{
    public class Transactions
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AccID { get; set; }
        public DateTime TransactionDate { get; set; }
        public SqlMoney Sum { get; set; }

        public virtual Account Account { get; set; }
    }
}
