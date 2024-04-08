﻿namespace ProjectBank.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Birtday { get; set; }
    }
}
