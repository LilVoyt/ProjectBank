namespace ProjectBank.Models
{
    public class AccountRequestModel
    {
        public string Name { get; set; } = string.Empty;
        public Guid EmployeeID { get; set; } = Guid.Empty;
        public Guid CustomerID { get; set; } = Guid.Empty;
    }
}
