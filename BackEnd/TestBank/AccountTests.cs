//using Microsoft.EntityFrameworkCore;
//using Xunit;
//using ProjectBank.Controller.Services;
//using ProjectBank.Entities;
//using ProjectBank.Controller.Validators;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using ProjectBank.Data;
//using ProjectBank.Models;
//using ProjectBank.Controller.Controllers.Mappers;

//public class AccountServiceTests
//{
//    private readonly AccountService _accountService;
//    private readonly DataContext _context;
//    private readonly AccountValidator _validator;

//    public AccountServiceTests()
//    {
//        var options = new DbContextOptionsBuilder<DataContext>()
//            .UseInMemoryDatabase(databaseName: "TestDatabase")
//            .Options;
//        _context = new DataContext(options);

//        var validationService = new ValidationService(_context);
//        _validator = new AccountValidator(validationService);
//        var _accountMapper = new AccountMapper();
//        _accountService = new AccountService(_context, _validator, _accountMapper);
//    }

//    [Fact]
//    public async Task AddAccount_Should_ThrowArgumentException_WhenNameIsEmpty()
//    {
//        // Arrange
//        var model = new AccountRequestModel { Name = "" };

//        // Act & Assert
//        var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.Post(model));
//        Assert.Contains("must not be empty", exception.Message);
//    }

//    [Fact]
//    public async Task AddAccount_Should_ThrowArgumentException_WhenNameIsTooLong()
//    {
//        // Arrange
//        var model = new AccountRequestModel { Name = new string('a', 51) };

//        // Act & Assert
//        var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.Post(model));
//        Assert.Contains("The length of 'Name' must be 50 characters or fewer", exception.Message);
//    }

//    [Fact]
//    public async Task AddAccount_Should_ThrowArgumentException_WhenNameIsNotUnique()
//    {
//        // Arrange
//        var existingAccount = new Account { Name = "ExistingName" };
//        await _context.Account.AddAsync(existingAccount);
//        await _context.SaveChangesAsync();

//        var model = new AccountRequestModel { Name = "ExistingName" };

//        // Act & Assert
//        var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.Post(model));
//        Assert.Contains("Name is used before (it must be unique)!", exception.Message);
//    }

//    [Fact]
//    public async Task AddAccount_Should_ThrowArgumentException_WhenCustomerNotExists()
//    {
//        // Arrange

//        var model = new AccountRequestModel { Name = "NewAccount", CustomerID = Guid.NewGuid() };

//        // Act & Assert
//        var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.Post(model));
//        Assert.Contains("Customer with this id not exist!", exception.Message);
//    }

//    //[Fact]
//    //public async Task AddAccount_Should_ThrowArgumentException_WhenEmployeeNotExists()
//    //{
//    //    // Arrange
//    //    var model = new AccountRequestModel { Name = "NewAccount", EmployeeID = Guid.NewGuid() };

//    //    // Act & Assert
//    //    var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.AddAccount(model));
//    //    Assert.Contains("Employee with this id not exist!", exception.Message);
//    //}

//    [Fact]
//    public async Task AddAccount_Should_ThrowArgumentException_WhenCustomerAlreadyRegistered()
//    {
//        // Arrange
//        Customer customer = new Customer 
//        { Id = Guid.NewGuid(), Name = "Name", LastName = "LastName", Country = "Ua", Email = "asdflkj@gmail.com", Phone = "312132" };
//        var existingAccount = new Account { Name = "ExistingAccount", CustomerID = customer.Id };
//        await _context.Customer.AddAsync(customer);
//        await _context.Account.AddAsync(existingAccount);
//        await _context.SaveChangesAsync();

//        var model = new AccountRequestModel { Name = "NewAccount", CustomerID = customer.Id };

//        // Act & Assert
        
//        var exception = await Assert.ThrowsAsync<Exception>(() => _accountService.Post(model));
//        Assert.Contains("Customer is already registered!", exception.Message);
//    }

//    [Fact]
//    public async Task AddAccount_Should_AddAccount_WhenValidModel()
//    {
//        // Arrange
//        Customer customer = new Customer
//        { Id = Guid.NewGuid(), Name = "Name", LastName = "LastName", Country = "Ua", Email = "asdflkj@gmail.com", Phone = "312132" };
        
//        var model = new AccountRequestModel { Name = "ValidAccount", CustomerID = customer.Id, EmployeeID = Guid.NewGuid() };
//        await _context.Customer.AddAsync(customer);
//        await _context.SaveChangesAsync();

//        // Act
//        var account = await _accountService.Post(model);

//        // Assert
//        Assert.NotNull(account);
//        Assert.Equal(model.Name, account.Name);
//        Assert.Equal(model.CustomerID, account.CustomerID);
//        Assert.Equal(model.EmployeeID, account.EmployeeID);
//        Assert.NotEqual(Guid.Empty, account.Id);
//    }

//    [Fact]
//    public async Task AddAccount_ShouldNT_ThrowArgumentException_WhenEmployeeIdIsEmpty()
//    {
//        Customer customer = new Customer
//        { Id = Guid.NewGuid(), Name = "Name", LastName = "LastName", Country = "Ua", Email = "asdflkj@gmail.com", Phone = "312132" };
//        await _context.Customer.AddAsync(customer);
//        await _context.SaveChangesAsync();

//        var model = new AccountRequestModel { Name = "ValidAccount", CustomerID = customer.Id, EmployeeID = Guid.Empty };
//        //await Assert.ThrowsAsync<Exception>(() => _accountService.AddAccount(model));

//    }
//}