using Microsoft.AspNetCore.Mvc;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Application.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<ActionResult<List<TransactionRequestModel>>> Get(Guid? search, string? sortItem, string? sortOrder);
        Task<Transaction> Post(TransactionRequestModel transaction);
        Task<Transaction> Update(Guid id, TransactionRequestModel transaction);
        Task<Transaction> Delete(Guid id);
    }
}
