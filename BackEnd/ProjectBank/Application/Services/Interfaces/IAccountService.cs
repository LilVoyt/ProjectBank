using Microsoft.AspNetCore.Mvc;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ActionResult<List<AccountRequestModel>>> Get(string? Search, string? SortItem, string? SortOrder);
        Task<Account> Post(AccountRequestModel account);
        Task<Account> Update(Guid id, AccountRequestModel account);
        Task<Account> Delete(Guid id);
    }
}
