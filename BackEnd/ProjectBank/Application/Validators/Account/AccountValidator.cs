using FluentValidation;
using ProjectBank.Application.Validators.Account;
using ProjectBank.Controller.Services;
using ProjectBank.Entities;

public class AccountValidator : AbstractValidator<Account>
{
    private readonly IAccountValidationService _validationService;

    public AccountValidator(IAccountValidationService validationService)
    {
        _validationService = validationService;

        RuleFor(a => a.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.")
            .MaximumLength(20)
            .WithMessage($"Name is too long!")
            .MustAsync(IsNameUnique)
            .WithMessage("Name is used before (it must be unique)!");

        RuleFor(a => a.EmployeeID)
            .MustAsync(IsEmployeeExistsOrNull).WithMessage("Employee with this id not exist!");

        RuleFor(a => a.CustomerID)
            .MustAsync(IsCustomerNotExists).WithMessage("Customer with this id not exist!")
            .MustAsync(IsNotAlreadyRegisteredCustomer).WithMessage("Customer is already registered!");
    }

    private async Task<bool> IsCustomerNotExists(Guid customerID, CancellationToken cancellationToken)
    {
        return await _validationService.IsCustomerNotExists(customerID);
    }

    private async Task<bool> IsEmployeeExistsOrNull(Guid? employeeID, CancellationToken cancellationToken)
    {
        return await _validationService.IsEmployeeExistsOrNull(employeeID);
    }

    private async Task<bool> IsNotAlreadyRegisteredCustomer(Guid customerId, CancellationToken cancellationToken)
    {
        return await _validationService.IsNotAlreadyRegisteredCustomer(customerId);
    }

    private async Task<bool> IsNameUnique(string name, CancellationToken cancellationToken)
    {
        return await _validationService.IsNameUnique(name);
    }
}
