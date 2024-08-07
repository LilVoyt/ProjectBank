﻿using System.ComponentModel.DataAnnotations;

namespace ProjectBank.Entities
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public virtual Account Account { get; set; }
    }
}
