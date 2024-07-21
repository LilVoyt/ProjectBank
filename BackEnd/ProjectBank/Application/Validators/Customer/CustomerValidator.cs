using FluentValidation;
using ProjectBank.Application.Validators.Account;
using ProjectBank.Application.Validators.Customer;
using ProjectBank.Entities;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


public class CustomerValidator : AbstractValidator<Customer>
{
    private readonly ICustomerValidationService _validationService;

    public CustomerValidator(ICustomerValidationService validationService)
    {
        _validationService = validationService;

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.")
            .MaximumLength(20)
            .WithMessage($"Name is too long!");

        RuleFor(c => c.LastName)
            .NotEmpty()
            .WithMessage("Last name cannot be empty.")
            .MaximumLength(30)
            .WithMessage($"Last name is too long!");

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .EmailAddress()
            .WithMessage("Email is not valid!")
            .MustAsync(_validationService.Is_Email_Not_In_DB)
            .WithMessage("Email is already registered!");

        RuleFor(c => c.Phone)
            .NotEmpty()
            .WithMessage("Phone number cannot be empty.")
            .MustAsync(_validationService.Is_PhoneNumber_Valid)
            .WithMessage("Phone number is not valid!")
            .MustAsync(_validationService.Is_PhoneNumber_Not_In_DB)
            .WithMessage("Phone number is already registered!");
    }
}
