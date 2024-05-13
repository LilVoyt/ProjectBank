using Microsoft.AspNetCore.Mvc;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Controller.Services.MathodsServise
{
    public interface IMethodsSevice
    {
        Task<ActionResult<Transaction>> MakeTransaction(Guid cardID, double sum);
    }
    public class MethodsService : IMethodsSevice
    {
        private readonly DataContext _context;

        public MethodsService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Transaction>> MakeTransaction(Guid cardID, double sum)
        {
            var card = await _context.Cards.FindAsync(cardID);
            Transaction transactions = new Transaction();
            transactions.Id = Guid.NewGuid();
            transactions.CardSenderID = cardID;
            transactions.TransactionDate = DateTime.Now;
            transactions.Sum = sum;
            if (card.Balance >= transactions.Sum)
            {
                card.Balance -= transactions.Sum;
                _context.Cards.Update(card);
                //await _context.SaveChangesAsync();
                await _context.Transaction.AddAsync(transactions);
                await _context.SaveChangesAsync();
                return transactions;
            }
            else
            {
                return null;
            }
        }
    }
}
