using Microsoft.AspNetCore.Mvc;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Controller.Services.MathodsServise
{
    public interface IMethodsSevice
    {
        Task<ActionResult<Transactions>> MakeTransaction(Guid cardID, double sum);
    }
    public class MethodsService : IMethodsSevice
    {
        private readonly DataContext _context;

        public MethodsService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Transactions>> MakeTransaction(Guid cardID, double sum)
        {
            var card = await _context.Cards.FindAsync(cardID);
            Transactions transactions = new Transactions();
            transactions.Id = Guid.NewGuid();
            transactions.CardID = cardID;
            transactions.TransactionDate = DateTime.Now;
            transactions.Sum = sum;
            if (card.Balance >= transactions.Sum)
            {
                card.Balance -= transactions.Sum;
                _context.Cards.Update(card);
                //await _context.SaveChangesAsync();
                await _context.Transactions.AddAsync(transactions);
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
