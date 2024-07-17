using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace ProjectBank.Entities
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Sum { get; set; }
        public Guid CardSenderID { get; set; } = Guid.Empty;
        public Guid CardReceiverID { get; set; } = Guid.Empty;
        public virtual Card CardSender { get; set; }
        public virtual Card CardReceiver { get; set; }
    }
}
