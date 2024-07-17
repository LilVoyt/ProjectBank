using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Controller.Services.Mappers
{
    public class AccountMapper
    {
        public Account MapRequestToAccount(AccountRequestModel requestModel)
        {
            return new Account
            {
                Id = Guid.NewGuid(),
                Name = requestModel.Name,
                EmployeeID = requestModel.EmployeeID,
                CustomerID = requestModel.CustomerID
            };
        }

        public AccountRequestModel MapRequestToDB(Account account)
        {
            return new AccountRequestModel 
            {
                Name = account.Name,
                EmployeeID = account.EmployeeID,
                CustomerID = account.CustomerID
            };
        }

        public Account MapRequestToSet(Account account, AccountRequestModel requestModel)
        {
            account.Name = requestModel.Name;
            account.EmployeeID = requestModel.EmployeeID;
            account.CustomerID = requestModel.CustomerID;

            return account;
        }
    }
}
