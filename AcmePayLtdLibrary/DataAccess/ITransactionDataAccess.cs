using AcmePayLtdLibrary.Models;

namespace AcmePayLtdLibrary.DataAccess
{
    public interface ITransactionDataAccess
    {
        Task<TransactionModel> AuthorizeTransactionAsync(TransactionModel transaction);
        Task<List<TransactionModel>?> GetTransactionsAync();
        Task<TransactionModel?> GetTransactionByIdAync(Guid Id);
        Task<TransactionModel?> VoidTransaction(string orderReference, Guid Uuid);
    }
}