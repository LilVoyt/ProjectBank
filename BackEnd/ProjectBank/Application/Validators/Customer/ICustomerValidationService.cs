namespace ProjectBank.Application.Validators.Customer
{
    public interface ICustomerValidationService
    {
        Task<bool> Is_PhoneNumber_Valid(string number);
    }
}
