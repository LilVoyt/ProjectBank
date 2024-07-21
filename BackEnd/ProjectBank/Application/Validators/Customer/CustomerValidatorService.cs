using ProjectBank.Data;
using System.Text.RegularExpressions;

namespace ProjectBank.Application.Validators.Customer
{

    public class CustomerValidatorService : ICustomerValidationService
    {
        private readonly DataContext _context;

        public CustomerValidatorService(DataContext context)
        {
            _context = context;
        }

        public Task<bool> Is_PhoneNumber_Valid(string phoneNumber)
        {
            string phonePattern = @"^\+?3?8?(0\d{9}|8\d{9})$";
            bool isValid = Regex.IsMatch(phoneNumber, phonePattern);
            return Task.FromResult(isValid);
        }

    }
}
