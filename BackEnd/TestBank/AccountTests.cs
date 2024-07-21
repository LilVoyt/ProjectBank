using FluentValidation.TestHelper;
using Moq;
using ProjectBank.Application.Validators.Account;
using ProjectBank.Entities;
using Xunit;

public class AccountValidatorTests
{
    private readonly Mock<IAccountValidationService> _validationServiceMock;
    private readonly AccountValidator _validator;

    public AccountValidatorTests()
    {
        _validationServiceMock = new Mock<IAccountValidationService>();
        _validator = new AccountValidator(_validationServiceMock.Object);
    }

    [Fact]
    public async Task Should_Have_Error_When_Name_Is_Empty()
    {
        var account = new Account { Name = "" };

        var result = await _validator.TestValidateAsync(account);

        result.ShouldHaveValidationErrorFor(a => a.Name).WithErrorMessage("Name cannot be empty.");
    }

    [Fact]
    public async Task Should_Have_Error_When_Name_Is_Not_Unique()
    {
        _validationServiceMock.Setup(x => x.IsNameUnique(It.IsAny<string>())).ReturnsAsync(false);

        var account = new Account { Name = "Test Name" };

        var result = await _validator.TestValidateAsync(account);

        result.ShouldHaveValidationErrorFor(a => a.Name).WithErrorMessage("Name is used before (it must be unique)!");
    }

    [Fact]
    public async Task Should_Have_Error_When_EmployeeID_Is_Not_Valid()
    {
        _validationServiceMock.Setup(x => x.IsEmployeeExistsOrNull(It.IsAny<Guid>())).ReturnsAsync(false);

        var account = new Account { EmployeeID = Guid.NewGuid() };

        var result = await _validator.TestValidateAsync(account);

        result.ShouldHaveValidationErrorFor(a => a.EmployeeID).WithErrorMessage("Employee with this id not exist!");
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_EmployeeID_Is_Null()
    {
        _validationServiceMock.Setup(x => x.IsEmployeeExistsOrNull(null)).ReturnsAsync(true);

        var account = new Account { EmployeeID = null };

        var result = await _validator.TestValidateAsync(account);

        result.ShouldNotHaveValidationErrorFor(a => a.EmployeeID);
    }


    [Fact]
    public async Task Should_Have_Error_When_CustomerID_Is_Not_Valid()
    {
        _validationServiceMock.Setup(x => x.IsCustomerNotExists(It.IsAny<Guid>())).ReturnsAsync(false);

        var account = new Account { CustomerID = Guid.NewGuid() };

        var result = await _validator.TestValidateAsync(account);

        result.ShouldHaveValidationErrorFor(a => a.CustomerID).WithErrorMessage("Customer with this id not exist!");
    }

    [Fact]
    public async Task Should_Have_Error_When_Customer_Is_Already_Registered()
    {
        _validationServiceMock.Setup(x => x.IsNotAlreadyRegisteredCustomer(It.IsAny<Guid>())).ReturnsAsync(false);

        var account = new Account { CustomerID = Guid.NewGuid() };

        var result = await _validator.TestValidateAsync(account);

        result.ShouldHaveValidationErrorFor(a => a.CustomerID).WithErrorMessage("Customer is already registered!");
    }
}