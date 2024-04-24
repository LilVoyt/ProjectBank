namespace ProjectBank.Models
{
    public class AccountRequestModel
    {
        public string Name { get; set; }
        public double Balance { get; set; }
        public Guid EmployeeID { get; set; }
        public Guid CustomerID { get; set; }
    }
}
