using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Controller.Services;
using ProjectBank.Entities;

namespace ProjectBank.Controller.Validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        private readonly IAccountService _accountService;
        public AccountValidator(IAccountService accountService)
        {
            _accountService = accountService;

            RuleFor(a => a.Name).NotEmpty().MaximumLength(50).MustAsync(IsNameUnique);
            RuleFor(a => a.EmployeeID).MustAsync(IsEmployeeNotExists);
            RuleFor(a => a.CustomerID).MustAsync(IsCustomerNotExists).MustAsync(IsNotAlredyRegisteredCustomer);
        }
        private async Task<bool> IsCustomerNotExists(Guid customerID, CancellationToken cancellationToken) 
        {
            return await _accountService.IsCustomerNotExists(customerID);
        }
        private async Task<bool> IsEmployeeNotExists(Guid employeeID, CancellationToken cancellationToken)
        {
            return await _accountService.IsEmployeeNotExists(employeeID);
        }
        public async Task<bool> IsNotAlredyRegisteredCustomer(Guid customerId, CancellationToken cancellationToken)
        {
            return await _accountService.IsNotAlredyRegisteredCustomer(customerId);
        }

        public async Task<bool> IsNameUnique(string name, CancellationToken cancellationToken)
        {
            return await _accountService.IsNameUnique(name);
        }
    }
}
