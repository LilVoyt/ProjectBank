using FluentValidation;
using ProjectBank.Application.Validators.Account;

namespace ProjectBank.Application.Validators.Card
{
    public class CardValidator : AbstractValidator<ProjectBank.Entities.Card>
    {
        private readonly ICardValidationService _validationService;

        public CardValidator(ICardValidationService validationService)
        {
            _validationService = validationService;

            RuleFor(a => a.CardName)
            .NotEmpty()
            .WithMessage("Name cannot be empty.");
        }
    }
}
