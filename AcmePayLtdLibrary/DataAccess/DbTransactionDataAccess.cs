using AcmePayLtdLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pagination.EntityFrameworkCore.Extensions;

namespace AcmePayLtdLibrary.DataAccess
{
    public class DbTransactionDataAccess : ITransactionDataAccess
    {
        private readonly TransactionDbContext _context;
        private readonly DbSet<TransactionModel> _transactions;

        public DbTransactionDataAccess(TransactionDbContext context)
        {
            _context = context;
            _transactions = _context.Transactions;
        }
        public async Task<TransactionModel?> AuthorizeTransactionAsync(TransactionModel transaction)
        {
            if (!_transactions.IsNullOrEmpty() && _transactions.Any(t => t.Uuid == transaction.Uuid))
            {
                return null;
            }

            _transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<TransactionModel?> CaptureTransaction(string orderReference, Guid Uuid)
        {
            if (_transactions.IsNullOrEmpty())
            {
                return null;
            }
            var transaction = _transactions.FirstOrDefault(t => t.Uuid == Uuid);
            if (transaction == null)
            {
                return null;
            }
            transaction.CaptureOrderReference = orderReference;
            transaction.Status = Status.Captured;

            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<TransactionModel?> GetTransactionByIdAync(Guid Id)
        {
            if(_transactions.IsNullOrEmpty())
            {
                return null;
            }
            return await _transactions.FirstOrDefaultAsync(t => t.Uuid == Id);
        }

        public async Task<PaginatedItemsViewModel<TransactionModel>?> GetTransactionsAync(int pageIndex, int pageSize)
        {
            if (_transactions.IsNullOrEmpty())
            {
                return null;
            }
            var list = await _transactions.AsPaginationAsync(pageIndex, pageSize, "DateUpdated");
            PaginatedItemsViewModel<TransactionModel> paginatedList = new(list.CurrentPage, pageSize, list.TotalPages, list.Results);
            return paginatedList;
        }

        public async Task<TransactionModel?> VoidTransaction(string orderReference, Guid Uuid)
        {
            if (_transactions.IsNullOrEmpty())
            {
                return null;
            }
            var transaction = _transactions.FirstOrDefault(t => t.Uuid == Uuid);
            if (transaction == null)
            {
                return null;
            }
            transaction.VoidOrderReference = orderReference;
            transaction.Status = Status.Voided;

            await _context.SaveChangesAsync();
            return transaction;
        }
    }
}
