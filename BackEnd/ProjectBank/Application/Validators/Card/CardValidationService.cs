using ProjectBank.Data;

namespace ProjectBank.Application.Validators.Card
{
    public class CardValidationService : ICardValidationService
    {
        private readonly DataContext _context;

        public CardValidationService(DataContext dataContext)
        {
            _context = dataContext;
        }


    }
}
