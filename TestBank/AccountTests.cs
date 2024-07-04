using Microsoft.EntityFrameworkCore;
using ProjectBank.Controller.Services;
using ProjectBank.Data;
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
            // Arrange
            AccountRequestModel accountRequestModel = null;

            // Act
            var createdAccount = await _accountService.AddAccount(accountRequestModel);

            // Assert
            Assert.Null(createdAccount);
        }
    }
}
