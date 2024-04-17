using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace ProjectBank.Entities
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }
        public int NumberCard { get; set; }
        public string CardName { get; set; }
        public int Pincode { get; set; }
        public DateTime Data { get; set; }
        public int CVV { get; set; }
        public double Balance { get; set; }
        //Foregin
        public Guid AccountID { get; set; }
        public Account Account { get; set; }
        public ICollection<Transactions> Transactions { get; set; }
    }
}
