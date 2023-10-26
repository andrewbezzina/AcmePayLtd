using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Models.Response;

namespace AcmePayLtdLibrary.Services
{
    public class TransactionHelpers : ITransactionHelpers
    {
        public GetTransactionModel MapSqlTransactionToResponse(TransactionModel transactionModel) => new GetTransactionModel()
        {
            Amount = transactionModel.Amount,
            Currency = transactionModel.Currency,
            CardholderNumber = AnonymizeCardNumber(transactionModel.CardholderNumber),
            HolderName = transactionModel.HolderName,
            Id = transactionModel.Uuid,
            Status = transactionModel.Status
        };
        public string AnonymizeCardNumber(string cardNumber) => cardNumber.Remove(6, 6).Insert(6, "********");
    }
}
