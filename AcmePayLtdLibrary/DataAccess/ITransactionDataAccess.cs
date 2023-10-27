using AcmePayLtdLibrary.Models;

namespace AcmePayLtdLibrary.DataAccess
{
    public interface ITransactionDataAccess
    {
        Task<TransactionModel?> AuthorizeTransactionAsync(TransactionModel transaction);
        Task<PaginatedItemsViewModel<TransactionModel>?> GetTransactionsAync(int pageIndex, int pageSize);
        Task<TransactionModel?> GetTransactionByIdAync(Guid Id);
        Task<TransactionModel?> VoidTransaction(string orderReference, Guid Uuid);
        Task<TransactionModel?> CaptureTransaction(string orderReference, Guid Uuid);
    }
}