using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Controller.Controllers;
using ProjectBank.Controller.Services;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;
using System.Threading.Tasks;
using Xunit;

namespace TestBank
{
    public class AccountTests
    {
        private readonly DataContext _context;
        private readonly AccountService _accountService;

        public AccountTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(options);
            _accountService = new AccountService(_context);
        }

        [Fact]
        public async Task AddAccount_Should_ReturnNull_WhenValueIsNull()
        {
            //Arrange
            AccountRequestModel accountRequestModel = null;

            //Act
            var createdAccount = await _accountService.AddAccount(accountRequestModel);

            //Assert
            Assert.Null(createdAccount);
        }

        [Fact]
        public async Task AddAccount_Should_ThrowArgumentException_WhenNameIsEmpty()
        {
            //Arrange
            var accountRequestModel = new AccountRequestModel
            {

            };

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _accountService.AddAccount(accountRequestModel));
        }

        [Fact]
        public async Task AddAccount_Should_ThrowInvalidOperationException_WhenAccountNameIsNotUnique()
        {
            //Arrange
            var existingAccount = new Account
            {
                Name = "Test Account"
            };
            _context.Accounts.Add(existingAccount);
            _context.SaveChanges();

            var accountRequestModel = new AccountRequestModel
            {
                Name = "Test Account"
            };

            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _accountService.AddAccount(accountRequestModel));
        }

        [Fact]
        public async Task AddAccount_Should_CreateAccount_WhenAllConditionsMet()
        {
            //Arrange
            var accountRequestModel = new AccountRequestModel
            {
                Name = "New Account",
            };

            //Act
            var createdAccount = await _accountService.AddAccount(accountRequestModel);

            //Assert
            Assert.NotNull(createdAccount);
            Assert.Equal("New Account", createdAccount.Name);
        }
    }
}
