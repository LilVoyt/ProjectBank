namespace ProjectBank.Application.Validators.Account
{
    public interface IAccountValidationService
    {
        Task<bool> IsCustomerNotExists(Guid customerID);
        Task<bool> IsEmployeeExistsOrNull(Guid? employeeID);
        Task<bool> IsNameUnique(string name);
        Task<bool> IsNotAlreadyRegisteredCustomer(Guid customerID);
    }
}