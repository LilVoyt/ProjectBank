using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using System.Text.RegularExpressions;

namespace ProjectBank.Application.Validators.Employee
{
    public class EmployeeValidationService : IEmployeeValidationService
    {
        private readonly DataContext _context;

        public EmployeeValidationService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Is_Email_Not_In_DB(string email, CancellationToken cancellationToken)
        {
            return !await _context.Employee.AnyAsync(c => c.Email == email);
        }

        public async Task<bool> Is_PhoneNumber_Not_In_DB(string number, CancellationToken cancellationToken)
        {
            return await _context.Employee.AnyAsync(c => c.Phone == number);
        }

        public Task<bool> Is_PhoneNumber_Valid(string phoneNumber, CancellationToken cancellationToken)
        {
            string phonePattern = @"^\+?3?8?(0\d{9}|8\d{9})$";
            bool isValid = Regex.IsMatch(phoneNumber, phonePattern);
            return Task.FromResult(isValid);
        }
    }
}
