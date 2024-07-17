using Microsoft.AspNetCore.Mvc;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Controller.Services.MathodsServise
{
    public interface IMethodsSevice
    {
        Task<ActionResult<Transaction>> MakeTransaction(Guid cardID, double sum);
        Task<ActionResult<Transaction>> MakeTransaction(Guid senderCardID, Guid receiverCardID, double sum);
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
            var card = await _context.Card.FindAsync(cardID);
            Transaction transactions = new Transaction();
            transactions.Id = Guid.NewGuid();
            transactions.CardSenderID = cardID;
            transactions.CardReceiverID = cardID;
            transactions.TransactionDate = DateTime.Now;
            transactions.Sum = sum;
            if (card.Balance >= transactions.Sum)
            {
                card.Balance -= transactions.Sum;
                _context.Card.Update(card);
                await _context.Transaction.AddAsync(transactions);
                await _context.SaveChangesAsync();
                return transactions;
            }
            else
            {
                return null;
            }
        }
        public async Task<ActionResult<Transaction>> MakeTransaction(Guid senderCardID, Guid receiverCardID, double sum)
        {
            var senderCard = await _context.Card.FindAsync(senderCardID);
            var receiverCard = await _context.Card.FindAsync(receiverCardID);
            Transaction transaction = new Transaction();
            transaction.Id = senderCardID;
            transaction.CardSenderID = senderCardID;
            transaction.CardReceiverID = receiverCardID;
            transaction.TransactionDate = DateTime.Now;
            transaction.Sum = sum;
            if (senderCard.Balance >= transaction.Sum)
            {
                senderCard.Balance -= transaction.Sum;
                _context.Card.Update(senderCard);
                receiverCard.Balance += transaction.Sum;
                _context.Card.Update(receiverCard);
                await _context.Transaction.AddAsync(transaction);
                await _context.SaveChangesAsync();
                return transaction;
            }
            else
            {
                throw new NotImplementedException("Not enough money!!!");
            }

        }
    }
}
