using Moq;
using ProjectBank.Application.Validators.Account;
using ProjectBank.Application.Validators.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBank
{
    internal class CustomerValidatorTests
    {
        private readonly Mock<ICustomerValidationService> _validationServiceMock;
        private readonly CustomerValidator _validator;
        CancellationToken _cancellationToken;

        public CustomerValidatorTests()
        {
            _validationServiceMock = new Mock<ICustomerValidationService>();
            _validator = new CustomerValidator(_validationServiceMock.Object);
            _cancellationToken = CancellationToken.None;
        }
    }
}
