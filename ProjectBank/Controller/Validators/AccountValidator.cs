using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Controller.Services;
using ProjectBank.Entities;

namespace ProjectBank.Controller.Validators
{


    public class AccountValidator : AbstractValidator<Account>
    {
        private readonly IValidationService _validationService;
        public AccountValidator(IValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(50)
                .MustAsync(IsNameUnique).WithMessage("Name is used before (it must be unique)!");
            RuleFor(a => a.EmployeeID)
                .MustAsync(IsEmployeeNotExists).WithMessage("Employee with this id not exist!");
            RuleFor(a => a.CustomerID)
                .MustAsync(IsCustomerNotExists).WithMessage("Customer with this id not exist!")
                .MustAsync(IsNotAlredyRegisteredCustomer).WithMessage("Customer is already registered!");
        }
        private async Task<bool> IsCustomerNotExists(Guid customerID, CancellationToken cancellationToken) 
        {
            return await _validationService.IsCustomerNotExists(customerID);
        }
        private async Task<bool> IsEmployeeNotExists(Guid employeeID, CancellationToken cancellationToken)
        {
            return await _validationService.IsEmployeeNotExists(employeeID);
        }
        public async Task<bool> IsNotAlredyRegisteredCustomer(Guid customerId, CancellationToken cancellationToken)
        {
            return await _validationService.IsNotAlreadyRegisteredCustomer(customerId);
        }

        public async Task<bool> IsNameUnique(string name, CancellationToken cancellationToken)
        {
            return await _validationService.IsNameUnique(name);
        }
    }
}
