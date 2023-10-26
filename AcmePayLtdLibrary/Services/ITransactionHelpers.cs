using AcmePayLtdLibrary.Models;
using AcmePayLtdLibrary.Models.Response;

namespace AcmePayLtdLibrary.Services
{
    public interface ITransactionHelpers
    {
        string AnonymizeCardNumber(string cardNumber);
        GetTransactionModel MapSqlTransactionToResponse(TransactionModel transactionModel);
    }
}