using FluentValidation;
using ProjectBank.Application.Validators.Customer;

namespace ProjectBank.Application.Validators.Transaction
{
    public class TransactionValidator : AbstractValidator<ProjectBank.Entities.Transaction>
    {
        private readonly ITransactionValidationService _validationService;

        public TransactionValidator(ITransactionValidationService validationService)
        {
            _validationService = validationService;
        }

    }
}
