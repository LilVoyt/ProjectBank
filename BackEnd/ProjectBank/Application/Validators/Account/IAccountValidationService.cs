namespace ProjectBank.Application.Validators.Account
{
    public interface IAccountValidationService
    {
        Task<bool> IsCustomerExists(Guid customerID, CancellationToken cancellationToken);
        Task<bool> IsEmployeeExistsOrNull(Guid? employeeID, CancellationToken cancellationToken);
        Task<bool> IsNameUnique(string name, CancellationToken cancellationToken);
        Task<bool> IsNotAlreadyRegisteredCustomer(Guid customerID, CancellationToken cancellationToken);
    }
}