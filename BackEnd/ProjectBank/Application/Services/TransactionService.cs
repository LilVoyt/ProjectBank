using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Application.Services.Interfaces;
using ProjectBank.Application.Services.Mappers;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ProjectBank.Controller.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;
        private readonly TransactionMapper _mapper;
        private readonly IValidator<Transaction> _validator;

        public TransactionService(DataContext context, TransactionMapper mapper, IValidator<Transaction> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ActionResult<List<TransactionRequestModel>>> Get(Guid? search, string? sortItem, string? sortOrder)
        {
            IQueryable<Transaction> transactions = _context.Transaction;

            if (search.HasValue)
            {
                transactions = transactions.Where(t => t.CardSenderID == search);
            }

            Expression<Func<Transaction, object>> selectorKey = sortItem?.ToLower() switch
            {
                "date" => transactions => transactions.TransactionDate,
                "sum" => transactions => transactions.Sum,
                _ => transactions => transactions.TransactionDate
            };

            transactions = sortOrder?.ToLower() == "desc"
                ? transactions.OrderByDescending(selectorKey)
                : transactions.OrderBy(selectorKey);
            List<Transaction> transactionsList = await transactions.ToListAsync();

            List<TransactionRequestModel> response = _mapper.GetRequestModels(transactionsList);

            return response;
        }

        public async Task<Transaction> Post(TransactionRequestModel transaction)
        {
            var res = _mapper.GetTransaction(transaction);

            var validationResult = await _validator.ValidateAsync(res);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new FluentValidation.ValidationException(errorMessages);
            }

            await _context.Transaction.AddAsync(res);
            await _context.SaveChangesAsync();
            
            return res;
        }

        public async Task<Transaction> Update(Guid id, TransactionRequestModel requestModel)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }
            transaction = _mapper.PutRequestModelInTransaction(transaction, requestModel);
            var validationResult = await _validator.ValidateAsync(transaction);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new FluentValidation.ValidationException(errorMessages);
            }
            _context.Transaction.Update(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction> Delete(Guid id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            transaction.CardSenderID = Guid.Empty;
            transaction.CardReceiverID = Guid.Empty;
            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }
    }
}
