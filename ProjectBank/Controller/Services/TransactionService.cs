using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Controller.Services
{
    public interface ITransactionService
    {
        Task<ActionResult<List<Transactions>>> GetAllTransaction();
        Task<TransactionRequestModel> GetTransactions(Guid id);
        Task<Transactions> AddTransactions(TransactionRequestModel transaction);
        Task<Guid> UpdateTransactions(Guid id, TransactionRequestModel transaction);
        Task<Guid> DeleteTransactions(Guid id);
    }
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;

        public async Task<Transactions> AddTransactions(TransactionRequestModel transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }
            var res = MapRequestToTransaction(transaction);

            _context.Transactions.AddAsync(res);
            await _context.SaveChangesAsync();

            return res;
        }

        public async Task<Guid> DeleteTransactions(Guid id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return Guid.Empty;
            }

            transaction.CardID = Guid.Empty;
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<ActionResult<List<Transactions>>> GetAllTransaction()
        {
            var transactions = await _context.Transactions.ToListAsync();

            return transactions;
        }

        public async Task<TransactionRequestModel> GetTransactions(Guid id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return null;
            }

            var res = MapRequestToDB(transaction);

            return res;
        }

        public async Task<Guid> UpdateTransactions(Guid id, TransactionRequestModel requestModel)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return Guid.Empty;
            }
            transaction = MapRequestToSet(transaction, requestModel);
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();

            return id;
        }


        private Transactions MapRequestToTransaction(TransactionRequestModel requestModel)
        {
            var transaction = new Transactions();
            transaction.Id = Guid.NewGuid();
            transaction.TransactionDate = requestModel.TransactionDate;
            transaction.Sum = requestModel.Sum;
            transaction.CardID = requestModel.CardID;

            return transaction;
        }

        private TransactionRequestModel MapRequestToDB(Transactions transaction)
        {
            var requestModel = new TransactionRequestModel();
            requestModel.TransactionDate = transaction.TransactionDate;
            requestModel.Sum = transaction.Sum;
            requestModel.CardID = transaction.CardID;
            return requestModel;
        }


        private Transactions MapRequestToSet(Transactions res, TransactionRequestModel transaction)
        {
            res.TransactionDate = transaction.TransactionDate;
            res.Sum = transaction.Sum;
            res.CardID = transaction.CardID;

            return res;
        }
    }
}
