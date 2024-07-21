using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Application.Services.Mappers
{
    public class AccountMapper
    {
        public Account GetAccount(AccountRequestModel requestModel)
        {
            return new Account
            {
                Id = Guid.NewGuid(),
                Name = requestModel.Name,
                EmployeeID = requestModel.EmployeeID,
                CustomerID = requestModel.CustomerID
            };
        }

        public AccountRequestModel GetRequestModel(Account account)
        {
            return new AccountRequestModel
            {
                Name = account.Name,
                EmployeeID = account.EmployeeID,
                CustomerID = account.CustomerID
            };
        }

        public Account PutAccountRequestModelInAccount(Account account, AccountRequestModel requestModel)
        {
            account.Name = requestModel.Name;
            account.EmployeeID = requestModel.EmployeeID;
            account.CustomerID = requestModel.CustomerID;

            return account;
        }

        public List<AccountRequestModel> GetRequestModels(List<Account> accounts)
        {
            return accounts.Select(account => GetRequestModel(account)).ToList();
        }

    }
}
