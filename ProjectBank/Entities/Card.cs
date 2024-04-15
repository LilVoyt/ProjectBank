using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace ProjectBank.Entities
{
    public class Card
    {
        [Key]
        public Guid id { get; set; }
        public int NumberCard { get; set; }
        public string CardName { get; set; }
        public int Pincode { get; set; }
        public DateTime Data { get; set; }
        public int CVV { get; set; }
        public double Balance { get; set; }
        public Guid AccLink { get; set; }
    }
}
