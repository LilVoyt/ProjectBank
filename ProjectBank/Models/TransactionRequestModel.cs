namespace ProjectBank.Models
{
    public class TransactionRequestModel
    {
        public DateTime TransactionDate { get; set; }
        public double Sum { get; set; }
        public Guid CardID { get; set; }
    }
}
