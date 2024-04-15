﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace ProjectBank.Entities
{
    public class Transactions
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AccID { get; set; }
        public DateTime TransactionDate { get; set; }
        public double Sum { get; set; }
        public virtual Account Account { get; set; }
        [ForeignKey("Account")]
        public Guid AccountId { get; set; }
    }
}