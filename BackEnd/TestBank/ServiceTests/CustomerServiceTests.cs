using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProjectBank.Application.Services.Mappers;
using ProjectBank.Controller.Services;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestBank.ServiceTests
{
    public class CustomerServiceTests
    {
        private readonly Mock<DbSet<Customer>> _mockCustomerDbSet;
        private readonly Mock<IDataContext> _mockDataContext;
        private readonly Mock<IValidator<Customer>> _mockCustomerValidator;
        private readonly Mock<ICustomerMapper> _mockCustomerMapper;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _mockCustomerDbSet = new Mock<DbSet<Customer>>();
            _mockDataContext = new Mock<IDataContext>();
            _mockCustomerValidator = new Mock<IValidator<Customer>>();
            _mockCustomerMapper = new Mock<ICustomerMapper>();

            _mockDataContext.Setup(m => m.Customer).Returns(_mockCustomerDbSet.Object);

            _customerService = new CustomerService(
                _mockDataContext.Object,
                _mockCustomerValidator.Object,
                _mockCustomerMapper.Object);
        }


        [Fact]
        public async Task Post_ValidCustomer_ReturnsCustomer()
        {
            // Arrange
            var customerRequest = new CustomerRequestModel { Name = "Test Customer" };
            var customer = new Customer { Id = Guid.NewGuid(), Name = "Test Customer" };

            _mockCustomerMapper.Setup(m => m.GetCustomer(customerRequest)).Returns(customer);
            _mockCustomerValidator.Setup(v => v.ValidateAsync(customer, default))
                .ReturnsAsync(new ValidationResult());

            // Act
            var result = await _customerService.Post(customerRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customer.Name, result.Name);
            _mockCustomerDbSet.Verify(m => m.AddAsync(It.IsAny<Customer>(), default), Times.Once);
            _mockDataContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task Post_InvalidCustomer_ThrowsValidationException()
        {
            // Arrange
            var customerRequest = new CustomerRequestModel { Name = "Invalid Customer" };
            var customer = new Customer { Id = Guid.NewGuid(), Name = "Invalid Customer" };
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Name", "Name is required")
            });

            _mockCustomerMapper.Setup(m => m.GetCustomer(customerRequest)).Returns(customer);
            _mockCustomerValidator.Setup(v => v.ValidateAsync(customer, default))
                .ReturnsAsync(validationResult);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _customerService.Post(customerRequest));
            Assert.Contains("Name is required", exception.Message);
        }

        [Fact]
        public async Task Update_ValidCustomer_ReturnsUpdatedCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerRequest = new CustomerRequestModel { Name = "Updated Customer" };
            var customer = new Customer { Id = customerId, Name = "Original Customer" };
            var changedCustomer = new Customer { Id = customerId, Name = customerRequest.Name };

            _mockCustomerDbSet.Setup(m => m.FindAsync(customerId)).ReturnsAsync(customer);
            _mockCustomerMapper.Setup(m => m.PutRequestModelInCustomer(customer, customerRequest)).Returns(changedCustomer);
            _mockCustomerValidator.Setup(v => v.ValidateAsync(changedCustomer, default))
                .ReturnsAsync(new ValidationResult());

            // Act
            var result = await _customerService.Update(customerId, customerRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerRequest.Name, result.Name);
            _mockCustomerDbSet.Verify(m => m.Update(It.Is<Customer>(c => c == changedCustomer)), Times.Once);
            _mockDataContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task Update_InvalidCustomer_ThrowsValidationException()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerRequest = new CustomerRequestModel { Name = "Invalid Customer" };
            var customer = new Customer { Id = customerId, Name = "Invalid Customer" };
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Name", "Name is required")
            });

            _mockCustomerDbSet.Setup(m => m.FindAsync(customerId)).ReturnsAsync(customer);
            _mockCustomerMapper.Setup(m => m.PutRequestModelInCustomer(customer, customerRequest)).Returns(customer);
            _mockCustomerValidator.Setup(v => v.ValidateAsync(customer, default))
                .ReturnsAsync(validationResult);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _customerService.Update(customerId, customerRequest));
            Assert.Contains("Name is required", exception.Message);
        }

        [Fact]
        public async Task Delete_ExistingCustomer_ReturnsDeletedCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new Customer { Id = customerId, Name = "Test Customer" };

            _mockCustomerDbSet.Setup(m => m.FindAsync(customerId)).ReturnsAsync(customer);

            // Act
            var result = await _customerService.Delete(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.Id);
            _mockCustomerDbSet.Verify(m => m.Remove(It.IsAny<Customer>()), Times.Once);
            _mockDataContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task Delete_NonExistingCustomer_ThrowsKeyNotFoundException()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            _mockCustomerDbSet.Setup(m => m.FindAsync(customerId)).ReturnsAsync((Customer)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _customerService.Delete(customerId));
            Assert.Equal($"Account with ID {customerId} not found.", exception.Message);
        }
    }
}
