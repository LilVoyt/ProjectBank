namespace ProjectBank.Models
{
    public class TransactionRequestModel
    {
        public DateTime TransactionDate { get; set; }
        public double Sum { get; set; }
        public Guid CardSenderID { get; set; } = Guid.Empty;
        public Guid CardReceiverID { get; set; } = Guid.Empty;
    }
}
