using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Application.Services.Mappers
{
    public class TransactionMapper
    {
        public Transaction GetTransaction(TransactionRequestModel requestModel)
        {
            var transaction = new Transaction();
            transaction.Id = Guid.NewGuid();
            transaction.TransactionDate = requestModel.TransactionDate;
            transaction.Sum = requestModel.Sum;
            transaction.CardSenderID = requestModel.CardSenderID;
            transaction.CardReceiverID = requestModel.CardReceiverID;

            return transaction;
        }

        public TransactionRequestModel GetRequestModel(Transaction transaction)
        {
            var requestModel = new TransactionRequestModel();
            requestModel.TransactionDate = transaction.TransactionDate;
            requestModel.Sum = transaction.Sum;
            requestModel.CardSenderID = transaction.CardSenderID;
            requestModel.CardReceiverID = transaction.CardReceiverID;
            return requestModel;
        }


        public Transaction PutRequestModelInTransaction(Transaction res, TransactionRequestModel transaction)
        {
            res.TransactionDate = transaction.TransactionDate;
            res.Sum = transaction.Sum;
            res.CardSenderID = transaction.CardSenderID;
            res.CardReceiverID = transaction.CardReceiverID;

            return res;
        }
        public List<TransactionRequestModel> GetRequestModels(List<Transaction> transactions)
        {
            return transactions.Select(transaction => GetRequestModel(transaction)).ToList();
        }
    }
}
