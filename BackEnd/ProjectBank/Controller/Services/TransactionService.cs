using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Controller.Services
{
    public interface ITransactionService
    {
        Task<ActionResult<List<Transaction>>> GetAllTransaction();
        Task<TransactionRequestModel> GetTransactions(Guid id);
        Task<Transaction> AddTransactions(TransactionRequestModel transaction);
        Task<Guid> UpdateTransactions(Guid id, TransactionRequestModel transaction);
        Task<Guid> DeleteTransactions(Guid id);
    }
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;

        public TransactionService(DataContext context)
        {
            _context = context;
        }

        public async Task<Transaction> AddTransactions(TransactionRequestModel transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }
            var res = MapRequestToTransaction(transaction);

            await _context.Transaction.AddAsync(res);
            await _context.SaveChangesAsync();

            return res;
        }

        public async Task<Guid> DeleteTransactions(Guid id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return Guid.Empty;
            }

            transaction.CardSenderID = Guid.Empty;
            transaction.CardReceiverID = Guid.Empty;
            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<ActionResult<List<Transaction>>> GetAllTransaction()
        {
            var transactions = await _context.Transaction.ToListAsync();

            return transactions;
        }

        public async Task<TransactionRequestModel> GetTransactions(Guid id)
        {
            var transaction = await _context.Transaction.FindAsync(id);

            if (transaction == null)
            {
                return null;
            }

            var res = MapRequestToDB(transaction);

            return res;
        }

        public async Task<Guid> UpdateTransactions(Guid id, TransactionRequestModel requestModel)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return Guid.Empty;
            }
            transaction = MapRequestToSet(transaction, requestModel);
            _context.Transaction.Update(transaction);
            await _context.SaveChangesAsync();

            return id;
        }


        private Transaction MapRequestToTransaction(TransactionRequestModel requestModel)
        {
            var transaction = new Transaction();
            transaction.Id = Guid.NewGuid();
            transaction.TransactionDate = requestModel.TransactionDate;
            transaction.Sum = requestModel.Sum;
            transaction.CardSenderID = requestModel.CardSenderID;
            transaction.CardReceiverID = requestModel.CardReceiverID;



            return transaction;
        }

        private TransactionRequestModel MapRequestToDB(Transaction transaction)
        {
            var requestModel = new TransactionRequestModel();
            requestModel.TransactionDate = transaction.TransactionDate;
            requestModel.Sum = transaction.Sum;
            requestModel.CardSenderID = transaction.CardSenderID;
            requestModel.CardReceiverID = transaction.CardReceiverID;
            return requestModel;
        }


        private Transaction MapRequestToSet(Transaction res, TransactionRequestModel transaction)
        {
            res.TransactionDate = transaction.TransactionDate;
            res.Sum = transaction.Sum;
            res.CardSenderID = transaction.CardSenderID;
            res.CardReceiverID = transaction.CardReceiverID;

            return res;
        }
    }
}
